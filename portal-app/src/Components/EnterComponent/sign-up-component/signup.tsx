import { useNavigate } from "react-router-dom";
import "./singup.scss"
import React from "react";
import ButtonComponent from "../../Button/buttonComponent";


function SignUp() {

  const [name, setName] = React.useState("")
  const [password, setPassword] = React.useState("")

  const navigate = useNavigate();
  
  function signUp() {

    navigate('/');
    //TODO при добавлени  identityService требуется настроить вход и переадресацию на /enter при отсуствии аутентификации
  }

  return (
    <div className="sign-up-body">
      <div className="title">Служебный вход</div>
      <div className="input-block">
        <input className="text-input" type="text" placeholder="Имя" onChange={evt => setName(evt.target.value)} />
        <input className="text-input" type="text" placeholder="Пароль" onChange={evt => setPassword(evt.target.value)} />
      </div>
      {ButtonComponent("Войти", signUp)}
    </div>
  );
}
export default SignUp;