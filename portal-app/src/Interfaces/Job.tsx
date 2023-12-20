import { Guid } from 'guid-typescript';
import { JobReplyFullResponse} from "./Replie";

export interface JobRequest{
    name: string,
    description: string
}

export interface JobFullRequest{
    id: Guid,
    name: string,
    description: string
}

export interface JobFullResponse{
    id: Guid,
    name: string,
    description: string,
    jobReplies: JobReplyFullResponse[],
}
export interface JobShortResponse{
    id: Guid,
    name: string,
    description: string
}