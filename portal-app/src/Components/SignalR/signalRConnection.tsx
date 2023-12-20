import React from 'react'
import * as signalR from '@microsoft/signalr'
import ApiService from '../../Services/ApiService'
import { HubConnection } from '@microsoft/signalr';
import './signalR.scss'
import { useDispatch } from 'react-redux';
import { ReplyApiService } from '../../Services/ReplyApiService';

function SignalRBlock() {
    const [connection, setConnection] = React.useState<HubConnection | null>(null);
    const [message, setMessage] = React.useState<string>("");
    const [classInfo, setClass] = React.useState<string>("info hideInfo");
    const [timerClass, setTimer] = React.useState<string>("timer hidetimer");
    
    const dispatch = useDispatch();
    
    React.useEffect(() => {
    var newConnection = new signalR.HubConnectionBuilder()
        .withUrl(ApiService.portalUrl + '/srbus')
        .withAutomaticReconnect()
        .build()
        setConnection(newConnection);
        if (newConnection) {
            newConnection.start()
                .then(() => {
                    console.log('connected signalR!')                    
                })                
                newConnection.on('ReceiveMessageOnFront', (message:string) => {
                    showShortMessage(message)
                });
                }
        },[]);

     function showShortMessage(mes:string): void{
        setClass("info showInfo");
        setMessage(mes)
        setTimeout(() => {
                setTimer("timer showTimer");
          }, 1000);
        setTimeout(() => {
            ReplyApiService.UpdateAll(dispatch);
               setTimeout(() => {
                setTimer("timer hidetimer")
                setMessage("")                
              }, 3000);
            setClass("info hideInfo");
          }, 6000);
     }

    return (
        <div className={classInfo}>
            <div className={timerClass}>
            </div>
            <div className='textBox'>
                {message}
                </div>
        </div>
    )
}
export default SignalRBlock;

