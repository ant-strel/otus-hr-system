import "./createReplyComponent.scss"
import React from 'react';
import ButtonComponent from '../../Button/buttonComponent';
import { SelectOptions } from "../../../Interfaces/Interface";
import Select from "react-dropdown-select";
import { CandidateApiService } from "../../../Services/CandidateApiService";
import { JobApiService } from "../../../Services/JobApiService";
import { ReplyApiService } from "../../../Services/ReplyApiService";
import { JobReplyRequest } from "../../../Interfaces/Replie";

function CreateReplyComponent(closeFunction: Function) {

  const [errorMessage, setError] = React.useState("");

  const [selectedJob, setJob] = React.useState<SelectOptions>();
  const [selectedCandidate, setCandidate] = React.useState<SelectOptions>();
  const [jobList, setJobList] = React.useState<SelectOptions[]>([]);
  const [candidateList, setCandidateList] = React.useState<SelectOptions[]>([]);

  React.useEffect(() => {
    JobApiService.GetAll().then(x=>{
      var selectOptions = x.map(job => ({value: job.id.toString(), label: job.name}) as SelectOptions);

      setJobList(selectOptions);
    });
    CandidateApiService.GetAllCandidate().then(x=>{
      var selectOptions = x.map(candidate => ({value: candidate.id.toString(), label: `${candidate.lastName} ${candidate.firstName} ${candidate.firstName}`}) as SelectOptions);

      setCandidateList(selectOptions);
    });
  },[]);

  function addCandidate(): void {
    if(selectedJob == null || selectedCandidate == null)
    {
      setError("Пожалуйста, заполните все поля!");
      return;
    }

    setError("")
    ReplyApiService.Create({
      JobId: selectedJob?.value,
      CandidateId: selectedCandidate?.value
       } as JobReplyRequest)
    .then(x=>{
      if (closeFunction) {
        closeFunction();
      }
    })    
  }

  function close(): void {
    if (closeFunction) {
      closeFunction();
    }
  }

  return (
    <div className='createReply'>
      <div className='input-block'>
        <Select className="text-input" options={jobList} onChange={(values) => setJob(values[0])} values={[]} placeholder="Вакансии"/>
        <Select className="text-input" options={candidateList} onChange={(values) => setCandidate(values[0])} values={[]} placeholder="Кандидаты" />
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

export default CreateReplyComponent;
