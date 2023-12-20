import React from 'react'
import './replyGridComponent.scss'
import { DataGrid, GridActionsCellItem, GridColDef, GridRowId } from '@mui/x-data-grid';
import EditIcon from '@mui/icons-material/Edit';
import { useDispatch, useSelector } from 'react-redux';
import { ReplyApiService } from '../../../Services/ReplyApiService';
import { JobReplyFullResponse } from '../../../Interfaces/Replie';
import { Guid } from 'guid-typescript';
import EditReplyComponent from '../EditReplyComponent/editReplyComponent';
import { CandidateApiService } from '../../../Services/CandidateApiService';
import { PopupState } from '../../../StateManagement/popupReducer';
import PopupComponent from '../../PopupComponent/popupComponent';

function ReplyGrid() {

    const columns: GridColDef[] = [
        { field: 'fullName', headerName: 'ФИО', width: 250 },
        { field: 'jobName', headerName: 'Вакансия', width: 250 },
        { field: 'status', headerName: 'Статус', width: 250 },
        { field: 'statusActual', headerName: 'Статус получен', width: 250, renderCell: (status: any) => {
          if(status == null){
            return (
            <div className="actualStatus unactual">
             </div>)
          }
          else{
              var isActual = status.value;
              var statusName = isActual == true ? "actualStatus actual" : "actualStatus unactual";
          return (
              <div className={statusName}>
               </div>
          );
          }
       } },
       { field: 'statusEnded', headerName: 'Конечный статус', width: 250, renderCell: (status: any) => {
         if(status == null){(
          <div className="actualStatus not">
           </div>)
         }
         else{
          var isEnded = status.value;
          var statusName = isEnded == true ? "actualStatus ended" : "actualStatus inwork";
      return (
          <div className={statusName}>
           </div>
      );
         }
      } },
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
                />];
            }},
      ];

    const dispatch = useDispatch();

    React.useEffect(() => {ReplyApiService.UpdateAll(dispatch)},[]);
  
    const [selectedID, setSelectedID] = React.useState<Guid>(Guid.create());
    const [ObjectIsSelect, setObjectIsSelect] = React.useState<boolean>(false);

   const handleEditClick = (id: GridRowId) => () => {
      var idStr = id.toString();
      var idGuid = Guid.parse(idStr)
       setSelectedID(idGuid)
       togglePopup(true);
   };
   
   var replyList = useSelector((x: {screen: any}) => {
        if(x.screen.repliesList.length != 0){
        return x.screen.repliesList.map((j:JobReplyFullResponse) => ({
            id: j.id,
            fullName: j.candidate? j.candidate.fullName: "",
            jobName: j.job? j.job.name : "",
            status: j.status? j.status.name: "",
            statusActual: j.status? j.status.isActual: null,
            statusEnded: j.status? j.status.isEnded: null
        }))}else{
          return [];
        }

    });
    const guidPopap: string = "EditReplyForm";
    const togglePopup = (objIsSelect:boolean): void => {
        dispatch({type: PopupState.CLOSE_OPEN_POPUP, payload: guidPopap})
        setObjectIsSelect(objIsSelect);
        CandidateApiService.UpdateAllCandidate(dispatch)
    }  

    return (
      <div>
          {PopupComponent({ elem: EditReplyComponent(togglePopup, selectedID, ObjectIsSelect), name: "Карточка отклика", popupId: guidPopap })}    
            <DataGrid 
            rows={replyList} 
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

export default ReplyGrid;