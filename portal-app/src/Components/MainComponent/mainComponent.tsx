import "./mainComponent.scss"
import JobMain from '../JobScreen/MainJobComponent/mainJobComponent';
import CandidateMain from "../CandidatesScreen/MainCandidatesComponent/mainCandidatesComponent";
import ReplyMain from "../ReplyScreen/MainReplyComponent/mainReplyComponent";
import { useSelector } from "react-redux";


function Main() {

  const selectedType = useSelector((x: {screen: any}) => {
    return x.screen.elementName});
    
  function getScreen() {
    switch (selectedType) {
      case JobMain.name:
        return <JobMain></JobMain>;
      case CandidateMain.name:
        return <CandidateMain></CandidateMain>;
      case ReplyMain.name:
        return <ReplyMain></ReplyMain>;
      default:
        return <JobMain></JobMain>;
    }
  }
  return (
    <div className='main'>
      {getScreen()}
    </div>
  );

}
export default Main;
