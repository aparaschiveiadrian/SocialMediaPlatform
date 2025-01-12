import './ConversationsPage.css';
import JoinConversation from "@/Components/JoinConversation/JoinConversation.jsx";
import Navbar from "@/Components/Navbar/Navbar.jsx";
import Conversations from "@/Components/Conversations/Conversations.jsx";

const ConversationsPage = () => {
    return (
        <div className="conversationsPage">
            <Navbar />
            <div className="delimiter"></div>
            <div className="pageContent">
                <JoinConversation />
                <Conversations />
            </div>
        </div>
    );
};

export default ConversationsPage;
