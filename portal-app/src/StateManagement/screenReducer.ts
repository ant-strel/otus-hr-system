import { CandidateFullResponse } from "../Interfaces/Candidate";
import { JobFullRequest } from "../Interfaces/Job";
import { JobReplyFullResponse} from "../Interfaces/Replie";

interface ScreenElem {
    elementName: string;
    jobsList: JobFullRequest[];
    candidatesList: CandidateFullResponse[];
    repliesList: JobReplyFullResponse[];
}

const initialState: ScreenElem = {
    elementName: "JobMain",
    candidatesList: [],
    jobsList: [],
    repliesList: []
}

export const ScreenState = Object.freeze({
    SET_SCREEN: 'SET_SCREEN',
    GET_JOBS: 'GET_JOBS_VALUES',
    GET_CANDIDATES: 'GET_CANDIDATES_VALUES',
    GET_REPLIES: 'GET_REPLIES_VALUES',
});

type Action = { type: string, payload?: any };

const screenReducer = (state = initialState, action: Action) => {
    switch (action.type) {
        case ScreenState.SET_SCREEN:
            return { ...state, elementName: action.payload};
        case ScreenState.GET_JOBS: 
            return {...state, jobsList: action.payload};
        case ScreenState.GET_CANDIDATES:
            return {...state, candidatesList: action.payload};
        case ScreenState.GET_REPLIES:
            return {...state, repliesList: action.payload};
        default:
            return state;
    }
}


export default screenReducer;