import { Component } from "react";
import "./enterForm.scss"
import SignUp from "../sign-up-component/signup";
import SignIn from "../sign-in-component/signin";

type Props = {};

type State = {
  content: string;
}

export class Enterform extends Component<Props, State> {
  readonly state: State = {
    content: 'container',
  };

  constructor(props: Props) {
    super(props);
  }

  private signIn(): void {
    this.setState({ content: 'container' })
  }

  private signUp(): void {
    this.setState({ content: 'container right-panel-active' })
  }

  render() {
    var signInActive = this.state.content;
    console.log(signInActive);
    return (
      <div className="enterScreen">
        <div className={signInActive}>
          <div className="form-container sign-up-container">
            <SignUp></SignUp>
          </div>
          <div className="form-container sign-in-container">
            <SignIn></SignIn>
          </div>
          <div className="overlay-container">
            <div className="overlay">
              <div className="overlay-panel overlay-left">
                <div className="info-text">Для работы в HR системе, перейдите:</div>
                <div className="change-button" onClick={this.signIn.bind(this)}>Перейти</div>
              </div>
              <div className="overlay-panel overlay-right">
                <div className="info-text">Для входа в админитративную панель, перейдите:</div>
                <div className="change-button" onClick={this.signUp.bind(this)}>Перейти</div>
              </div>
            </div>

          </div>
        </div>
      </div>
    );
  }
}
export default Enterform;

declare global {
  var isUpActive: boolean;
}