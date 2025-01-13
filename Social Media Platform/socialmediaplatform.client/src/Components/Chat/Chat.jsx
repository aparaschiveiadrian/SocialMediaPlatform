import React, { useState, useEffect } from "react";
import "./Chat.css";
import SendConversationMessage from "@/Components/SendConversationMessage/SendConversationMessage";
import LeaveButton from "@/Components/Buttons/LeaveButton/LeaveButton.jsx";
import DeleteConversationButton from "@/Components/Buttons/DeleteConversationButton/DeleteConversationButton.jsx";
import ViewMembersModal from "@/Components/ViewMembersModal/ViewMembersModal.jsx";
import ShowMembersButton from "@/Components/Buttons/ShowMembersButton/ShowMembersButton.jsx";
import ConversationRequestsButton from "@/Components/Buttons/ConversationRequestsButton/ConversationRequestsButton.jsx";

const Chat = ({ messages, conversationId, CurrentUserId, conversationModeratorId }) => {
    const [chatMessages, setChatMessages] = useState(messages);
    const [userCache, setUserCache] = useState({});

    useEffect(() => {
        setChatMessages(messages);
    }, [messages]);

    useEffect(() => {
        const fetchAllUsers = async () => {
            const uniqueUserIds = [...new Set(chatMessages.map((msg) => msg.userId))];
            const token = localStorage.getItem("token");

            const userIdsToFetch = uniqueUserIds.filter((userId) => !userCache[userId]);

            if (userIdsToFetch.length === 0) return;

            try {
                const fetchPromises = userIdsToFetch.map(async (userId) => {
                    const response = await fetch(`https://localhost:44354/getUserById/${userId}`, {
                        method: "GET",
                        headers: {
                            "Content-Type": "application/json",
                            Authorization: `Bearer ${token}`,
                        },
                    });

                    if (!response.ok) {
                        throw new Error("Failed to fetch user details");
                    }

                    const data = await response.json();
                    return { userId, username: data.username };
                });

                const results = await Promise.all(fetchPromises);

                setUserCache((prevCache) => {
                    const updatedCache = { ...prevCache };
                    results.forEach(({ userId, username }) => {
                        updatedCache[userId] = username;
                    });
                    return updatedCache;
                });
            } catch (error) {
                console.error("Error fetching user details:", error);
            }
        };

        fetchAllUsers();
    }, [chatMessages]);

    const handleNewMessage = (newMessage) => {
        setChatMessages((prevMessages) => [...prevMessages, newMessage]);

        if (newMessage.username) {
            setUserCache((prevCache) => ({
                ...prevCache,
                [newMessage.userId]: newMessage.username,
            }));
        }
    };

    return (
        <div className="chatContainer">
            <div className="messagesContainer">
                {chatMessages.length > 0 ? (
                    chatMessages.map((message) => (
                        <div key={message.id} className="message">
                            <p className="messageContent">
                                <a
                                    href={`/profile/${
                                        message.username || userCache[message.userId] || "unknown"
                                    }`}
                                    className="messageSender"
                                >
                                    @{message.username || userCache[message.userId] || "Unknown"}
                                </a>
                                : {message.content}
                            </p>
                            <span className="messageTimestamp">
                                {new Date(message.sentAt).toLocaleString()}
                            </span>
                        </div>
                    ))
                ) : (
                    <p className="noMessages">No messages yet</p>
                )}
            </div>

            <div className="chatButtons">
                <ShowMembersButton
                    conversationId={conversationId}
                    userIsModerator={conversationModeratorId === CurrentUserId}
                />
                {conversationModeratorId === CurrentUserId ? (
                    <>
                        <ConversationRequestsButton conversationId={conversationId} />
                        <DeleteConversationButton conversationId={conversationId} />
                    </>
                ) : (
                    <LeaveButton conversationId={conversationId} />
                )}
            </div>

            <SendConversationMessage
                conversationId={conversationId}
                onMessageSent={handleNewMessage}
            />
        </div>
    );
};

export default Chat;
