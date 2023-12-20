import CandidateMain from "../CandidatesScreen/MainCandidatesComponent/mainCandidatesComponent";
import JobMain from "../JobScreen/MainJobComponent/mainJobComponent";
import ReplyMain from "../ReplyScreen/MainReplyComponent/mainReplyComponent";
import "./menuComponent.scss"
import MenuItem from './menuItemComponent/menuItemComponent';


function Menu() {
  return (
    <div className='leftSideMenu'>
      <div className='menuExpander'>
        <div className="buttonBlock">
        <div className="blockName">Экран</div>
        <div className="sep">
        {MenuItem({ name: "Вакансии", companentName:JobMain.name, imageName: "vacancy"})}
        {MenuItem({ name: "Кандидаты", companentName:CandidateMain.name, imageName: "candidat"})}
        {MenuItem({ name: "Отклики", companentName:ReplyMain.name, imageName: "reply"})}
        </div>
        </div>
      </div>
    </div>
  );
}
export default Menu;
 