import axios from 'axios';
import { Guid } from 'guid-typescript';
import { ScreenState } from '../StateManagement/screenReducer';
import { Dispatch } from 'react';
import { AnyAction } from '@reduxjs/toolkit';
import ApiService from './ApiService';
import { JobReplyFullResponse, JobReplyRequest } from '../Interfaces/Replie';

export class ReplyApiService extends ApiService {
  public static Create(obj: JobReplyRequest): Promise<Guid> {
    return axios.post(`${this.portalUrl}/api/JobReply/Create`, obj)
      .then(x => x.data)
      .catch(x => console.log("Не удалось сохранить вакансию. ", x))
  }

  public static GetAllReply(): Promise<JobReplyFullResponse[]> {
    return axios.get(`${this.portalUrl}/api/JobReply/GetAll`)
      .then(x => x? x.data : [])
      .catch(x => console.log("Не удалось получить список. ", x))
  }
  public static GetById(id: Guid): Promise<JobReplyFullResponse> {
    return axios.get(`${this.portalUrl}/api/JobReply/GetById`, { params: { id: id } })
      .then(x => x? x.data : [])
      .catch(x => console.log("Не удалось получить список. ", x))
  }

  public static UpdateAll(dispatch: Dispatch<AnyAction>): void {
    axios.get(`${this.portalUrl}/api/JobReply/GetAll`)
      .then(x => dispatch({
        type: ScreenState.GET_REPLIES,
        payload: x? x.data : [],
    }))
    .catch(x => console.log("Не удалось получить список. ", x))
  }

  public static ReplyApiService(id: string): Promise<boolean> {
    return axios.delete(`${this.portalUrl}/api/JobReply/RemoveById`, { params: { id: id } })
      .then(x => x.data)
      .catch(x => console.log("Не удалось удалить объект", x))
  }
}