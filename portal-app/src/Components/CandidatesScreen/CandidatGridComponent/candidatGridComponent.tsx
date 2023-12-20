import React from 'react'
import './CandidatGridComponent.scss'
import { DataGrid, GridActionsCellItem, GridColDef, GridRowId } from '@mui/x-data-grid';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/DeleteOutlined';
import { useDispatch, useSelector } from 'react-redux';
import { CandidateApiService } from '../../../Services/CandidateApiService';
import { CandidateFullResponse } from '../../../Interfaces/Candidate';
import EditCandidateComponent from '../EditCandidateComponent/editCandidateComponent';
import { Guid } from 'guid-typescript';
import { PopupState } from '../../../StateManagement/popupReducer';
import PopupComponent from '../../PopupComponent/popupComponent';

function CandidateGrid() {

    const columns: GridColDef[] = [
        { field: 'fullName', headerName: 'ФИО', width: 250 },
        { field: 'age', headerName: 'Возраст', width: 250 },        
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

    React.useEffect(() => {CandidateApiService.UpdateAllCandidate(dispatch)},[]);
  
    const handleDeleteClick = (id: GridRowId) => () => {
        CandidateApiService.RemoveById(id as string).then(x=> CandidateApiService.UpdateAllCandidate(dispatch));
     };

     const [selectedID, setSelectedID] = React.useState<Guid>(Guid.create());
     const [ObjectIsSelect, setObjectIsSelect] = React.useState<boolean>(false);

    const handleEditClick = (id: GridRowId) => () => {
        setSelectedID(Guid.parse(id.toString()))
        togglePopup(true);
    };
   
   var candidateList = useSelector((x: {screen: any}) => {
        return x.screen.candidatesList.map((x:CandidateFullResponse) => ({
            id: x.id,
            fullName: `${x.lastName} ${x.firstName} ${x.surname}`,
            age: x.age
        }))
    });
      const guidPopap: string = "EditCandidateForm";
      const togglePopup = (objIsSelect:boolean): void => {
          dispatch({type: PopupState.CLOSE_OPEN_POPUP, payload: guidPopap})
          setObjectIsSelect(objIsSelect);
          CandidateApiService.UpdateAllCandidate(dispatch)
      }  
      
        return (
          <div>
              {PopupComponent({ elem: EditCandidateComponent(togglePopup, selectedID, ObjectIsSelect), name: "Карточка кандидата", popupId: guidPopap })}        
                <DataGrid 
                rows={candidateList} 
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

export default CandidateGrid;