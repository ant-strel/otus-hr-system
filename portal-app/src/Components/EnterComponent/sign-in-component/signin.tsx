import "./singin.scss"
import React from "react";
import { useNavigate } from 'react-router-dom';
import ButtonComponent from "../../Button/buttonComponent";
import { iSignIn } from "../../../Interfaces/Auth";


function SignIn() {

  const [name, setName] = React.useState("")
  const [password, setPassword] = React.useState("")

  const navigate = useNavigate();
  function signInFunc() {
    navigate('/');
  }

  return (
    <div className="sign-in-body">
      <div className="title">Вход в HR систему</div>
      <div className="input-block">
        <input className="text-input" type="text" placeholder="Имя" onChange={evt => setName(evt.target.value)} />
        <input className="text-input" type="text" placeholder="Пароль" onChange={evt => setPassword(evt.target.value)} />
      </div>
      {ButtonComponent("Войти", signInFunc)}
    </div>
  );

}
export default SignIn;


