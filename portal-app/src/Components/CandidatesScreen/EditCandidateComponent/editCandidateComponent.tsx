import "./editCandidateComponent.scss"
import React from 'react';
import ButtonComponent from '../../Button/buttonComponent';
import { CandidateApiService } from "../../../Services/CandidateApiService";
import { CandidateFullRequest, CandidateFullResponse, CandidateRequest } from "../../../Interfaces/Candidate";
import { Guid } from "guid-typescript";

function EditCandidateComponent(closeFunction: Function, id: Guid, isSelected: boolean = false) {
  const [errorMessage, setError] = React.useState("");
  const [obj, setObj] = React.useState<CandidateFullResponse>({} as CandidateFullResponse);

  function clearField():void{    
    setObj({} as CandidateFullResponse);
    isSelected = false;
  }

  React.useEffect(() => {
    if(isSelected)
    CandidateApiService.GetById(id)
    .then(x=> {
      setObj(x);
    })    
    .catch(x=>{  
      setError(x.response.data)
    })
  }, [id]);

  function saveCandidate(): void {
    CandidateApiService.UpdateCandidate({
        id: obj.id,
        lastName: obj.lastName,
        firstName: obj.firstName,
        surname: obj.surname,
        age: obj.age,
        address: obj.address } as CandidateFullRequest)
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
    <div className='editCandidate'>
      <div className='input-block'>
        <input className="text-input" type="text" placeholder="Фамилия" onChange={e =>setObj({...obj, lastName: e.target.value} as CandidateFullResponse)} value={obj.lastName}/>
        <input className="text-input" type="text" placeholder="Имя" onChange={e =>setObj({...obj, firstName: e.target.value} as CandidateFullResponse)} value={obj.firstName}/>
        <input className="text-input" type="text" placeholder="Отчество" onChange={e =>setObj({...obj, surname: e.target.value} as CandidateFullResponse)} value={obj.surname}/>
        <input className="text-input" type="number" placeholder="Возраст" onChange={e =>setObj({...obj, age: Number(e.target.value)} as CandidateFullResponse)} min="0" max="100"value={obj.age}/>
        <input className="text-input" type="text" placeholder="Адрес" onChange={e =>setObj({...obj, address: e.target.value} as CandidateFullResponse)} value={obj.address}/>
      </div>
        <div className="error-text">
        {errorMessage}</div>
      <div className='button-block'>
        {ButtonComponent("Сохранить",saveCandidate)}
        {ButtonComponent("Закрыть",close)}
      </div>
    </div>
  );
}

export default EditCandidateComponent;
