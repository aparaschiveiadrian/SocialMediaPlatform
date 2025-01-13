import "./ConversationsPage.css";
import JoinConversation from "@/Components/JoinConversation/JoinConversation.jsx";
import Navbar from "@/Components/Navbar/Navbar.jsx";
import Conversations from "@/Components/Conversations/Conversations.jsx";
import { useEffect, useState } from "react";
import CreateGroup from "@/Components/CreateGroup/CreateGroup.jsx";

const ConversationsPage = () => {
    const [userId, setUserId] = useState(null);

    useEffect(() => {
        const username = localStorage.getItem("username");
        if (username) {
            fetch(`https://localhost:44354/getUserIdByUsername/${username}`)
                .then((response) => {
                    if (!response.ok) {
                        throw new Error("Failed to fetch user ID");
                    }
                    return response.json();
                })
                .then((data) => {
                    setUserId(data.userId);
                })
                .catch(() => {
                    console.error("An error occurred while fetching user ID.");
                });
        } else {
            console.log("You have to be authenticated in order to view conversations.");
        }
    }, []);

    return (
        <div className="conversationsPage">
            <Navbar />
            <div className="delimiter"></div>
            <div className="pageContent">
                <div className="joinCreateContainer">
                    <JoinConversation />
                    <CreateGroup />
                </div>
                {userId && <Conversations userId={userId} />}
            </div>
        </div>
    );
};

export default ConversationsPage;
