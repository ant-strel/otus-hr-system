import React from "react";
import { useDispatch } from "react-redux";
import { CandidateApiService } from "../../../Services/CandidateApiService";
import { PopupState } from "../../../StateManagement/popupReducer";
import ButtonComponent from "../../Button/buttonComponent";
import PopupComponent from "../../PopupComponent/popupComponent";
import CandidateGrid from "../CandidatGridComponent/candidatGridComponent";
import CreateCandidateComponent from "../CreateCandidateComponent/createCandidateComponent";

function CandidateMain() {

    const dispatch = useDispatch();

    const guidPopap: string = "CandidateCreateForm";
    const togglePopup = (): void => {
        dispatch({type: PopupState.CLOSE_OPEN_POPUP, payload: guidPopap})
        CandidateApiService.UpdateAllCandidate(dispatch)
    }  

    React.useEffect(() => {CandidateApiService.UpdateAllCandidate(dispatch)},[]);
    
    return (
        <div>
            <div className='mainTop'>
                <div className='title'>Кандидаты</div>
                     {ButtonComponent("Добавить", togglePopup)}
            </div>
            <div className='mainBottom'>
                    {PopupComponent({ elem: CreateCandidateComponent(togglePopup), name: "Создать кандидата" , popupId: guidPopap})}                    
                    <div className='gridContainer'>
                        {CandidateGrid()}
                    </div>
            </div>
        </div>)
}


export default CandidateMain;