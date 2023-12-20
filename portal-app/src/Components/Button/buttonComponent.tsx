import "./buttonComponent.scss"

function  ButtonComponent (text:string, callback: () => void){  
    return (
      <div className="defaultButton" onClick={callback}>{text}</div>
  );
}

export default ButtonComponent;
