import { Guid } from "guid-typescript";
import { CandidateSimpleResponse } from "./Candidate";
import { JobShortResponse } from "./Job";
import { StatusResponse } from "./Status";


export interface JobReplyRequest{
    JobId: string,
    CandidateId: string
}

export interface JobReplyFullResponse{
    id: Guid,
    job: JobShortResponse,
    candidate: CandidateSimpleResponse,
    status: StatusResponse
}

export interface JobReplySimpleResponse{
    id: Guid,
    jobId: Guid,
    candidateId: Guid,
    statusId: Guid
}
