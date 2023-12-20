import { useDispatch, useSelector } from "react-redux";
import { ScreenState } from "../../../StateManagement/screenReducer";
import "./menuItemComponent.scss";
import { PopupState } from "../../../StateManagement/popupReducer";

export interface menuItemProps{
    name: string, 
    companentName: string, 
    imageName: string
  }
  
function MenuItem(prop: menuItemProps) {

    var logo = `logo ${prop.imageName}`;

    const dispatch = useDispatch();

    function clickMenu(){      
        dispatch({
            type: PopupState.CLOSE_POPUP});   
        dispatch({
            type: ScreenState.SET_SCREEN,
            payload: prop.companentName,
        });
    } 

    const selectedType = useSelector((x: {screen: any}) => {
        return x.screen.elementName == prop.companentName});

    return (
        <div className={ `menu-item ${selectedType? "checked" : "unchecked"}`} onClick={clickMenu}>
                <div className={logo}/>
                <div className="text"> {prop.name}
            </div>
        </div>
    );
}
export default MenuItem; 