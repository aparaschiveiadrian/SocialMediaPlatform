import React, { useState } from "react";
import "./SendConversationMessage.css";

const SendConversationMessage = ({ conversationId, onMessageSent }) => {
    const [messageContent, setMessageContent] = useState("");
    const [isSending, setIsSending] = useState(false);

    const handleSendMessage = async () => {
        if (!messageContent.trim()) return;

        const token = localStorage.getItem("token");
        const username = localStorage.getItem("username");

        try {
            setIsSending(true);

            const response = await fetch("https://localhost:44354/message/send", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`,
                },
                body: JSON.stringify({
                    content: messageContent,
                    conversationId,
                }),
            });

            if (!response.ok) {
                throw new Error("Failed to send the message");
            }

            const contentType = response.headers.get("Content-Type");
            let newMessage;

            if (contentType && contentType.includes("application/json")) {
                newMessage = await response.json();
            } else {
                newMessage = {
                    id: Date.now(),
                    content: messageContent,
                    sentAt: new Date().toISOString(),
                    userId: "currentUserId", 
                    username,
                };
            }

            setMessageContent("");

            if (onMessageSent) {
                onMessageSent(newMessage);
            }
        } catch (error) {
            console.error("Error sending message:", error);
        } finally {
            setIsSending(false);
        }
    };

    const handleInputChange = (event) => {
        setMessageContent(event.target.value);
    };

    const handleKeyPress = (event) => {
        if (event.key === "Enter") {
            handleSendMessage();
        }
    };

    return (
        <div className="sendMessageContainer">
            <input
                type="text"
                className="messageInput"
                placeholder="Type your message..."
                value={messageContent}
                onChange={handleInputChange}
                onKeyPress={handleKeyPress}
                disabled={isSending}
            />
            <button
                className="sendButton"
                onClick={handleSendMessage}
                disabled={isSending || !messageContent.trim()}
            >
                {isSending ? "Sending..." : "Send"}
            </button>
        </div>
    );
};

export default SendConversationMessage;
