import React from 'react'
import './jobGridComponent.scss'
import { JobApiService } from '../../../Services/JobApiService'
import { DataGrid, GridActionsCellItem, GridColDef, GridRowId } from '@mui/x-data-grid';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/DeleteOutlined';
import { useDispatch, useSelector } from 'react-redux';
import { Guid } from 'guid-typescript';
import { PopupState } from '../../../StateManagement/popupReducer';
import PopupComponent from '../../PopupComponent/popupComponent';
import EditJobComponent from '../EditJobComponent/editJobComponent';

function JobGrid() {

    const columns: GridColDef[] = [
        { field: 'name', headerName: 'Название', width: 250 },
        { field: 'description', headerName: 'Описание', width: 250 },        
        {
            field: 'actions',
            type: 'actions',
            headerName: 'Actions',
            width: 100,
            cellClassName: 'actions',
            getActions: ({ id }) => { 
                return [
                <GridActionsCellItem
                    icon={<EditIcon />}
                    label="Edit"
                    color="inherit"
                    className="textPrimary"
                    onClick={handleEditClick(id)}
                />,
                <GridActionsCellItem
                    icon={<DeleteIcon />}
                    label="Del"
                    onClick={handleDeleteClick(id)}
                    color="inherit"
                />];
            }},
      ];
    const dispatch = useDispatch();

    React.useEffect(() => {JobApiService.UpdateAll(dispatch)},[]);
  
    const handleDeleteClick = (id: GridRowId) => () => {
        JobApiService.RemoveById(id as string).then(x=> JobApiService.UpdateAll(dispatch));
     };

   const handleEditClick = (id: GridRowId) => () => {
    setSelectedID(Guid.parse(id.toString()))
    togglePopup(true);
   };
   
   var jobList = useSelector((x: {screen: any}) => {
       return x.screen.jobsList});

       const [selectedID, setSelectedID] = React.useState<Guid>(Guid.create());
       const [ObjectIsSelect, setObjectIsSelect] = React.useState<boolean>(false);
       const guidPopap: string = "EditJobPopup";
       const togglePopup = (objIsSelect:boolean): void => {
           dispatch({type: PopupState.CLOSE_OPEN_POPUP, payload: guidPopap})
           setObjectIsSelect(objIsSelect);
           JobApiService.UpdateAll(dispatch)
       }  
    return (<div>
      {PopupComponent({ elem: EditJobComponent(togglePopup, selectedID, ObjectIsSelect), name: "Карточка вакансии", popupId: guidPopap })}    
            <DataGrid 
            rows={jobList} 
            columns={columns} 
            initialState={{
                pagination: {
                  paginationModel: {
                    pageSize: 8,
                  },
                },
              }}
            disableRowSelectionOnClick /> 
            </div>
        )
}

export default JobGrid;