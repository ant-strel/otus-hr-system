import "./editReplyComponent.scss"
import React from 'react';
import ButtonComponent from '../../Button/buttonComponent';
import { Guid } from "guid-typescript";
import { JobReplyFullResponse } from "../../../Interfaces/Replie";
import { ReplyApiService } from "../../../Services/ReplyApiService";

function EditReplyComponent(closeFunction: Function, id: Guid, isSelected: boolean = false) {
  const [errorMessage, setError] = React.useState("");
  const [obj, setObj] = React.useState<JobReplyFullResponse>({} as JobReplyFullResponse);

  React.useEffect(() => {
    if(isSelected)
    ReplyApiService.GetById(id)
    .then(x=> setObj(x))    
    .catch(x=>{  
      setError(x.response.data)
    })
  }, [id]);

  function close(): void {
    if (closeFunction) {
      closeFunction();
    }
  }
if(isSelected == true){
  return (
    <div className='editReply'>
      <div className='input-block'>
        <div className='text-line'>
          <div className='field-name'> Статус: </div>
          <div className='field-value'> {obj.status ? obj.status.name: ""}</div>
      </div>
        <div className='text-line'>
          <div className='field-name'> ФИО: </div>
          <div className='field-value'> {obj.candidate ? obj.candidate.fullName: ""}</div>
      </div>
        <div className='text-line'>
          <div className='field-name'> Вакансия: </div>
          <div className='field-value'> {obj.job? obj.job.name: ""}</div>
      </div>
      </div>
        <div className="error-text">
        {errorMessage}</div>
      <div className='button-block'>
        {ButtonComponent("Закрыть",close)}
      </div>
    </div>
  );
}else{
  return (
    <div className='editReply'>
        <div className="error-text">
        {errorMessage}</div>
      <div className='button-block'>
        {ButtonComponent("Закрыть",close)}
      </div>
    </div>
  );
}
}

export default EditReplyComponent;
