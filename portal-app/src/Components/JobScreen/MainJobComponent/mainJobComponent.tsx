import React from 'react';
import ButtonComponent from '../../Button/buttonComponent';
import './mainJobComponent.scss'
import PopupComponent from '../../PopupComponent/popupComponent';
import CreateJobComponent from '../CreateJobComponent/createJobComponent';
import JobGrid from '../JobGridComponent/jobGridComponent';
import { useDispatch } from 'react-redux';
import { JobApiService } from '../../../Services/JobApiService';
import { PopupState } from '../../../StateManagement/popupReducer';

function JobMain() {

    const dispatch = useDispatch();

    const guidPopap: string = "CreateJobForm";
    const togglePopup = (): void => {
        dispatch({type: PopupState.CLOSE_OPEN_POPUP, payload: guidPopap})
        JobApiService.UpdateAll(dispatch)
    }  
    
    React.useEffect(() => {JobApiService.UpdateAll(dispatch)},[]);
    
    return (
        <div>
            <div className='mainTop'>
                <div className='title'>Вакансии</div>
                     {ButtonComponent("Добавить", togglePopup)}
            </div>
            <div className='mainBottom'>
                    {PopupComponent({ elem: CreateJobComponent(togglePopup), name: "Создать вакансию", popupId:guidPopap })}
                    
               <div className='gridContainer'>
                    {JobGrid()}
                </div>
            </div>
        </div>)
}


export default JobMain;
