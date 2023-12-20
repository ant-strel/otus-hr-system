import axios from 'axios';
import { Guid } from 'guid-typescript';
import { ScreenState } from '../StateManagement/screenReducer';
import { Dispatch } from 'react';
import { AnyAction } from '@reduxjs/toolkit';
import ApiService from './ApiService';
import { CandidateFullRequest, CandidateFullResponse, CandidateRequest } from '../Interfaces/Candidate';

export class CandidateApiService extends ApiService {
  public static Create(obj: CandidateRequest): Promise<Guid> {
    return axios.put(`${this.portalUrl}/api/Candidate/Create`, obj)
      .then(x => x.data)
      .catch(x => {
        console.log("Не удалось сохранить кандидата. ", x)
        throw x;
    })
  }

  public static UpdateCandidate(obj: CandidateFullRequest): Promise<CandidateFullResponse[]> {
    return axios.post(`${this.portalUrl}/api/Candidate/Update`, obj)
      .then(x => x? x.data : [])
      .catch(x => 
        {
         console.log("Не удалось сохранить кандидата. ", x)
        throw x;
    })
  }

  public static GetAllCandidate(): Promise<CandidateFullResponse[]> {
    return axios.get(`${this.portalUrl}/api/Candidate/GetAll`)
      .then(x => x? x.data : [])
      .catch(x => console.log("Не удалось получить список. ", x))
  }
  
  public static GetById(id: Guid): Promise<CandidateFullResponse> {
    return axios.get(`${this.portalUrl}/api/Candidate/GetById`, { params: { id: id } })
      .then(x => x? x.data : [])
      .catch(x => console.log("Не удалось получить список. ", x))
  }

  public static UpdateAllCandidate(dispatch: Dispatch<AnyAction>): void {
    axios.get(`${this.portalUrl}/api/Candidate/GetAll`)
      .then(x => dispatch({
        type: ScreenState.GET_CANDIDATES,
        payload: x? x.data : [],
    }))
    .catch(x => console.log("Не удалось получить список. ", x))
  }

  public static RemoveById(id: string): Promise<boolean> {
    return axios.delete(`${this.portalUrl}/api/Candidate/RemoveById`, { params: { id: id } })
      .then(x => x.data)
      .catch(x => console.log("Не удалось удалить объект", x))
  }
}