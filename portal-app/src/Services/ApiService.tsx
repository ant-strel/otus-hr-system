class ApiService{    
    public static readonly portalUrl = process.env.REACT_APP_URL_PORTAL ?? "http://localhost:85"; 
    public static readonly wfUrl = process.env.REACT_APP_URL_WF ?? "http://localhost:7000";  
    public static readonly identityUrl = process.env.REACT_APP_URL_IDENTITY ?? "http://localhost:86";  
 }

export default ApiService;
