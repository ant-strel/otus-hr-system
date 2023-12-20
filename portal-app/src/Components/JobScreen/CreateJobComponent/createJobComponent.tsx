import "./createJobComponent.scss"
import React from 'react';
import ButtonComponent from '../../Button/buttonComponent';
import { JobApiService } from '../../../Services/JobApiService';
import { JobRequest } from "../../../Interfaces/Job";

function CreateJobComponent(closeFunction: Function) {
  const [name, setName] = React.useState("");
  const [description, setDescription] = React.useState("");

  function clearField():void{    
    setName("");
    setDescription("");
  }


  const [errorMessage, setError] = React.useState("");
  function handleNameChange(event: any): void {
    setName(event.target.value);
  }

  function handleDescriptionChange(event: any): void {
    setDescription(event.target.value);
  }

  function addJob(): void {
    setError("")    
    JobApiService.Create({ name: name, description: description } as JobRequest)
    .then(x=>{
      clearField();
      if (closeFunction) {
        closeFunction();
      }
    })
    .catch(x=>{  
      var mes = x.response.data.replaceAll("--", "")
      var mes = mes.replaceAll("Severity: Error", "")
      setError(mes)    
    })
  }

  function close(): void {
    clearField();
    if (closeFunction) {
      closeFunction();
    }
  }

  return (
    <div className='createJob'>
      <div className='input-block'>
        <input className="text-input" type="text" placeholder="Name" onChange={handleNameChange} />
        <input className="text-input" type="text" placeholder="Description" onChange={handleDescriptionChange} />
      </div>
        <div className="error-text">
        {errorMessage}</div>
      <div className='button-block'>
        {ButtonComponent("Добавить",addJob)}
        {ButtonComponent("Закрыть",close)}
      </div>
    </div>
  );
}

export default CreateJobComponent;
