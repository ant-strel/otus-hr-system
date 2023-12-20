import React from "react";
import { ReplyApiService } from "../../../Services/ReplyApiService";
import { useDispatch } from "react-redux";
import { PopupState } from "../../../StateManagement/popupReducer";
import ButtonComponent from "../../Button/buttonComponent";
import CreateReplyComponent from "../CreateReplyComponent/createReplyComponent";
import PopupComponent from "../../PopupComponent/popupComponent";
import ReplyGrid from "../ReplyGridComponent/replyGridComponent";

function ReplyMain() {

    const dispatch = useDispatch();

    const guidPopap: string = "CreateReplyForm";
    const togglePopup = (): void => {
        dispatch({type: PopupState.CLOSE_OPEN_POPUP, payload: guidPopap})
        ReplyApiService.UpdateAll(dispatch)
    }  

    React.useEffect(() => {ReplyApiService.UpdateAll(dispatch)},[]);

    return (
        <div>
            <div className='mainTop'>
                <div className='title'>Отклики</div>
                     {ButtonComponent("Добавить", togglePopup)}
            </div>
            <div className='mainBottom'>
                    {PopupComponent({ elem: CreateReplyComponent(togglePopup), name: "Создать отклик", popupId: guidPopap })}
                    <div className='gridContainer'>
                        {ReplyGrid()}
                    </div>
            </div>
        </div>)
}


export default ReplyMain;