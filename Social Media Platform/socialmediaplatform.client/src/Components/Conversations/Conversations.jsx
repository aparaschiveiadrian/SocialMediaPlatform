import React, { useState, useEffect } from "react";
import Chat from "@/Components/Chat/Chat";
import "./Conversations.css";

const Conversations = ({ userId }) => {
    const [conversations, setConversations] = useState([]);
    const [selectedConversationId, setSelectedConversationId] = useState(null);
    const [selectedModeratorId, setSelectedModeratorId] = useState(null); 
    const [messages, setMessages] = useState([]);

    useEffect(() => {
        const fetchConversations = async () => {
            const token = localStorage.getItem("token");

            try {
                const response = await fetch("https://localhost:44354/user/groups", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${token}`,
                    },
                });

                if (!response.ok) {
                    throw new Error("Failed to fetch conversations");
                }

                const data = await response.json();
                setConversations(data);
            } catch (error) {
                console.error("Error fetching conversations:", error);
            }
        };

        fetchConversations();
    }, []);

    const handleConversationSelect = async (conversationId) => {
        setSelectedConversationId(conversationId);

        // Find the selected conversation's moderatorId
        const selectedConversation = conversations.find(
            (conversation) => conversation.id === conversationId
        );
        setSelectedModeratorId(selectedConversation?.moderatorId || null);

        const token = localStorage.getItem("token");

        try {
            const response = await fetch(
                `https://localhost:44354/conversation/get/messages/${conversationId}`,
                {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${token}`,
                    },
                }
            );

            if (!response.ok) {
                throw new Error("Failed to fetch messages");
            }

            const data = await response.json();
            setMessages(data);
        } catch (error) {
            console.error("Error fetching messages:", error);
        }
    };

    return (
        <div className="conversationsContainer">
            <div className="conversationsList">
                <h2>Conversations</h2>
                {conversations.map((conversation) => (
                    <div
                        key={conversation.id}
                        className={`conversationItem ${
                            selectedConversationId === conversation.id ? "active" : ""
                        }`}
                        onClick={() => handleConversationSelect(conversation.id)}
                    >
                        <p>{conversation.name} #{conversation.id}</p>
                        <small>
                            Last Message:{" "}
                            {new Date(conversation.lastMessageSentAt).toLocaleString()}
                        </small>
                    </div>
                ))}
            </div>
            <div className="chatPanel">
                {selectedConversationId ? (
                    <Chat
                        messages={messages}
                        conversationId={selectedConversationId}
                        CurrentUserId={userId}
                        conversationModeratorId={selectedModeratorId}
                    />
                ) : (
                    <div className="noConversationSelected">
                        <p>Select a conversation to view messages</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Conversations;
