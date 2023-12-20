import  {  ReactNode } from 'react';
import "./popupComponent.scss"
import Draggable from 'react-draggable';
import { useSelector } from 'react-redux';


export interface popupProps{
  name: string
  elem: ReactNode,
  popupId: string
}

function PopupComponent (props: popupProps) {
  var showElement = useSelector((x: {popup: any}) => {
    return x.popup.popups.findIndex((p:string) => p == props.popupId) >= 0});

  if(showElement == false){
    return null;
  }else{
    return (
      <div className='draggable'>
        <Draggable 
        bounds="parent" 
        handle="strong">
        <div className="no-cursor popupBox popupBody">
          <strong className="cursor popup-header">
            <div className="headetText">{props.name}
            </div>
            </strong>
            {props.elem}
        </div>
      </Draggable>
      </div>

    );
  }
}
export default PopupComponent;
