import React from "react";

const LeaveButton = ({ conversationId }) => {
    const handleLeaveConversation = async () => {
        const token = localStorage.getItem("token");

        try {
            const response = await fetch(`https://localhost:44354/conversation/leave/${conversationId}`, {
                method: "POST", 
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`,
                },
            });
            

            if(response.ok) {
                window.location.reload();
            }
        } catch (error) {
            console.error("Error leaving conversation:", error);
        }
    };

    return (
        <button className="btn btn-chat" onClick={handleLeaveConversation}>
            Leave Conversation
        </button>
    );
};

export default LeaveButton;
