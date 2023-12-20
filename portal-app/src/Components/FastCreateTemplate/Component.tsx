import  { Component } from 'react';
import "./Component.scss"

type State = {
}
type Props = {  
};

class FastComponent extends Component<Props,State> {
  constructor(props: Props) {
    super(props);
  }
  render() {
    return (
      <div className=''>
        
      </div>
    );
  }
}
export default FastComponent;
