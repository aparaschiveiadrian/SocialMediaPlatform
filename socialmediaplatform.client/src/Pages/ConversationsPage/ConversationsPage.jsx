import './ConversationsPage.css'
import JoinConversation from "@/Components/JoinConversation/JoinConversation.jsx";
import Navbar from "@/Components/Navbar/Navbar.jsx";

const ConversationsPage = () => {
    
    return (
        <>
            <Navbar/>
            <div className="delimiter">
            </div>
            <JoinConversation/>
        </>
    )
}
export default ConversationsPage;