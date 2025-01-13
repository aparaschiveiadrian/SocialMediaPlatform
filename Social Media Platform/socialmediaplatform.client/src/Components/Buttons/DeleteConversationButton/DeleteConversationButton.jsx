import React from "react";

const DeleteConversationButton = ({ conversationId }) => {
    const handleDeleteConversation = async () => {
        const token = localStorage.getItem("token");

        try {
            const response = await fetch(`https://localhost:44354/conversation/delete/${conversationId}`, {
                method: "DELETE",
                headers: {
                    
                    "Authorization": `Bearer ${token}`,
                },
            });


            if(response.ok) {
                window.location.reload();
            }
        } catch (error) {
            console.error("Error deleting conversation:", error);
        }
    };

    return (
        <button className="btn btn-chat" onClick={handleDeleteConversation}>
            Delete Conversation
        </button>
    );
};

export default DeleteConversationButton;
