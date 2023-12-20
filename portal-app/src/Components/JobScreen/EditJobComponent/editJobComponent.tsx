import "./editJobComponent.scss"
import React from 'react';
import ButtonComponent from '../../Button/buttonComponent';
import { Guid } from "guid-typescript";
import { JobFullRequest, JobFullResponse } from "../../../Interfaces/Job";
import { JobApiService } from "../../../Services/JobApiService";

function EditJobComponent(closeFunction: Function, id: Guid, isSelected: boolean = false) {
  const [errorMessage, setError] = React.useState("");
  const [obj, setObj] = React.useState<JobFullResponse>({} as JobFullResponse);

  function clearField():void{    
    setObj({} as JobFullResponse);
    isSelected = false;
  }

  React.useEffect(() => {
    if(isSelected)
    JobApiService.GetById(id)
    .then(x=> setObj(x))    
    .catch(x=>{  
      setError(x.response.data)
    })
  }, [id]);

  function saveJob(): void {
    JobApiService.Update({
      id: obj.id,
      name: obj.name,
      description: obj.description
    } as JobFullRequest)
    .then(x=>{
      clearField();
      if (closeFunction) {
        closeFunction(false);}
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
    <div className='editJob'>
      <div className='input-block'>
        <input className="text-input" type="text" placeholder="Название" onChange={e =>setObj({...obj, name: e.target.value} as JobFullResponse)} value={obj.name}/>
        <input className="text-input" type="text" placeholder="Описание" onChange={e =>setObj({...obj, description: e.target.value} as JobFullResponse)} value={obj.description}/>
      </div>
        <div className="error-text">
        {errorMessage}</div>
      <div className='button-block'>
        {ButtonComponent("Сохранить",saveJob)}
        {ButtonComponent("Закрыть",close)}
      </div>
    </div>
  );
}

export default EditJobComponent;
