import React, { useEffect, useState } from 'react';
import './Chat.css';
import LeaveConversation from "@/Components/LeaveConversation/LeaveConversation.jsx";
import ViewMembersConversation from "@/Components/ViewMembersConversation/ViewMembersConversation.jsx";

const Chat = ({ groupId, groupName, onLeaveConversation }) => {
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState('');
    const [loadingMessages, setLoadingMessages] = useState(true);
    const [error, setError] = useState(null);
    const [userCache, setUserCache] = useState({});
    const [viewingMembers, setViewingMembers] = useState(false);

    const fetchMessages = async () => {
        try {
            setLoadingMessages(true);
            const response = await fetch(`https://localhost:44354/conversation/get/messages/${groupId}`, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                },
            });

            if (!response.ok) {
                throw new Error('Failed to fetch messages.');
            }

            const data = await response.json();
            setMessages(data);

            const uniqueUserIds = [...new Set(data.map((message) => message.userId))];
            await Promise.all(uniqueUserIds.map(fetchUsername));
        } catch (err) {
            setError(err.message);
        } finally {
            setLoadingMessages(false);
        }
    };

    const fetchUsername = async (userId) => {
        if (userCache[userId]) return;

        try {
            const response = await fetch(`https://localhost:44354/getUserById/${userId}`, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                },
            });

            if (!response.ok) {
                throw new Error('Failed to fetch username.');
            }

            const data = await response.json();
            setUserCache((prevCache) => ({ ...prevCache, [userId]: data.username }));
        } catch (err) {
            console.error(`Error fetching username for userId ${userId}:`, err);
        }
    };

    const sendMessage = async () => {
        if (!newMessage.trim()) return;

        try {
            const response = await fetch('https://localhost:44354/message/send', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                },
                body: JSON.stringify({
                    content: newMessage,
                    conversationId: groupId,
                }),
            });

            if (!response.ok) {
                throw new Error(await response.text());
            }

            const sentMessageContent = await response.text();

            const newMessageObject = {
                content: sentMessageContent,
                sentAt: new Date().toISOString(),
                userId: 'currentUser',
            };

            setMessages((prevMessages) => [...prevMessages, newMessageObject]);
            setNewMessage('');

            await fetchUsername('currentUser');
        } catch (err) {
            setError(err.message);
        }
    };

    useEffect(() => {
        fetchMessages();
    }, [groupId]);

    return (
        <div className="chatContainer">
            <div className="chatHeader">
                <h3 className="chatTitle">Messages for: {groupName}</h3>
                <div className="chatActions">
                    <LeaveConversation groupId={groupId} onLeave={onLeaveConversation} />
                    <button className="actionButton" onClick={() => setViewingMembers(true)}>
                        View Members
                    </button>
                </div>
            </div>
            <div className="messagesContainer">
                {loadingMessages ? (
                    <p className="loadingMessage">Loading messages...</p>
                ) : messages.length > 0 ? (
                    <ul className="messagesList">
                        {messages.map((message, index) => (
                            <li key={index} className="messageItem">
                                <div className="messageHeader">
                                    <a href={`/profile/${userCache[message.userId] || '#'}`} className="messageUsername">
                                        @{userCache[message.userId] || 'Fetching...'}
                                    </a>
                                    <small className="messageTime">
                                        {new Date(message.sentAt).toLocaleString()}
                                    </small>
                                </div>
                                <p className="messageContent">{message.content}</p>
                            </li>
                        ))}
                    </ul>
                ) : (
                    <p className="noMessagesMessage">No messages yet.</p>
                )}
            </div>
            <div className="messageInputContainer">
                <textarea
                    className="messageInput"
                    placeholder="Type your message here..."
                    value={newMessage}
                    onChange={(e) => setNewMessage(e.target.value)}
                ></textarea>
                <button className="sendButton" onClick={sendMessage}>
                    Send
                </button>
            </div>
            {error && <p className="errorMessage">{error}</p>}
            {viewingMembers && (
                <ViewMembersConversation groupId={groupId} onClose={() => setViewingMembers(false)} />
            )}
        </div>
    );
};

export default Chat;
