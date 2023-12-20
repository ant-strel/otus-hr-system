import { Guid } from "guid-typescript";

export interface StatusResponse{
    id: Guid,
    name: string,
    isEnded: boolean,
    isActual: boolean
}