import axios from 'axios';
import { Guid } from 'guid-typescript';
import { JobFullRequest, JobFullResponse, JobRequest } from '../Interfaces/Job';
import { ScreenState } from '../StateManagement/screenReducer';
import { Dispatch } from 'react';
import { AnyAction } from '@reduxjs/toolkit';
import ApiService from './ApiService';

export class JobApiService extends ApiService {
  public static Create(obj: JobRequest): Promise<Guid> {
    return axios.put(`${this.portalUrl}/api/Job/Create`, obj)
      .then(x => x.data)
      .catch(x => {
        console.log("Не удалось сохранить вакансию. ", x)
        throw x;
      })
  }

  public static Update(obj: JobFullRequest): Promise<JobFullResponse[]> {
    return axios.post(`${this.portalUrl}/api/Job/Update`, obj)
      .then(x => x? x.data : [])
      .catch(x => 
        {
         console.log("Не удалось сохранить вакансию. ", x)
        throw x;
    })
  }

  public static GetById(id: Guid): Promise<JobFullResponse> {
    return axios.get(`${this.portalUrl}/api/Job/GetById`, { params: { id: id } })
      .then(x => x? x.data : [])
      .catch(x => console.log("Не удалось получить список. ", x))
  }

  public static GetAll(): Promise<JobFullRequest[]> {
    return axios.get(`${this.portalUrl}/api/Job/GetAll`)
      .then(x => x? x.data : [])
      .catch(x => console.log("Не удалось получить список. ", x))
  }

  public static UpdateAll(dispatch: Dispatch<AnyAction>): void {
    axios.get(`${this.portalUrl}/api/Job/GetAll`)
      .then(x => dispatch({
        type: ScreenState.GET_JOBS,
        payload: x? x.data : [],
    }))
    .catch(x => console.log("Не удалось получить список. ", x))
  }

  public static RemoveById(id: string): Promise<boolean> {
    return axios.delete(`${this.portalUrl}/api/Job/RemoveById`, { params: { id: id } })
      .then(x => x.data)
      .catch(x => console.log("Не удалось удалить объект", x))
  }
}