import { Guid } from 'guid-typescript';
import { JobReplyFullResponse, JobReplySimpleResponse} from './Replie';

export interface CandidateSimpleResponse{
    id: Guid,
    fullName: string,
    age: number,
    jobReplies: JobReplySimpleResponse[]
}

export interface CandidateFullResponse{
    id: Guid,
    lastName: string,
    firstName: string,
    surname: string,
    age: number,
    address: string,    
    jobReplies: JobReplyFullResponse[]
}

export interface CandidateRequest{
    lastName: string,
    firstName: string,
    surname: string,
    age: number,
    address: string
}

export interface CandidateFullRequest{
    id: Guid,
    lastName: string,
    firstName: string,
    surname: string,
    age: number,
    address: string
}