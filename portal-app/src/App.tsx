
import Menu from './Components/MenuComponent/menuComponent';
import Main from './Components/MainComponent/mainComponent';
import "./App.scss"
import SignalRBlock from './Components/SignalR/signalRConnection';

function App() {  
    return (
      <div className='app'>
        {Menu()}
        {Main()}
        {SignalRBlock()}
      </div>
    );
}
export default App;
