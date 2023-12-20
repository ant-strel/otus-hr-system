import "./createCandidateComponent.scss"
import React from 'react';
import ButtonComponent from '../../Button/buttonComponent';
import { CandidateApiService } from "../../../Services/CandidateApiService";
import { CandidateRequest } from "../../../Interfaces/Candidate";

function CreateCandidateComponent(closeFunction: Function) {
  const [lastName, setLastName] = React.useState("");
  const [firstName, setFirstName] = React.useState("");
  const [surname, setSurname] = React.useState("");
  const [age, setAge] = React.useState(0);
  const [address, setAddress] = React.useState("");
  const [errorMessage, setError] = React.useState("");

  function clearField():void{    
    setLastName("");
    setFirstName("");
    setFirstName("");
    setAge(0);
    setAddress("");    
    setAddress("");
  }

  function addCandidate(): void {
    CandidateApiService.Create({
        lastName: lastName,
        firstName: firstName,
        surname: surname,
        age: age,
        address: address } as CandidateRequest)
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
    <div className='createCandidate'>
      <div className='input-block'>
        <input className="text-input" type="text" placeholder="Фамилия" onChange={e =>setLastName(e.target.value)} />
        <input className="text-input" type="text" placeholder="Имя" onChange={e =>setFirstName(e.target.value)} />
        <input className="text-input" type="text" placeholder="Отчество" onChange={e =>setSurname(e.target.value)} />
        <input className="text-input" type="number" placeholder="Возраст" onChange={e =>setAge(Number(e.target.value))} min="0" max="100"/>
        <input className="text-input" type="text" placeholder="Адрес" onChange={e =>setAddress(e.target.value)} />
      </div>
        <div className="error-text">
        {errorMessage}</div>
      <div className='button-block'>
        {ButtonComponent("Добавить",addCandidate)}
        {ButtonComponent("Закрыть",close)}
      </div>
    </div>
  );
}

export default CreateCandidateComponent;
