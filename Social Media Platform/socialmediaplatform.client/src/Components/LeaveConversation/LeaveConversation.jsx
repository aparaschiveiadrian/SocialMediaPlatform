import React from 'react';

const LeaveConversation = ({ groupId, onLeave }) => {
    const leaveConversation = async () => {
        try {
            const response = await fetch(`https://localhost:44354/conversation/leave/${groupId}`, {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                },
            });

            if (!response.ok) {
                throw new Error(await response.text());
            }

            onLeave();
        } catch (err) {
            console.error(err.message);
        }
    };

    return (
        <button className="actionButton" onClick={leaveConversation}>
            Leave
        </button>
    );
};

export default LeaveConversation;
