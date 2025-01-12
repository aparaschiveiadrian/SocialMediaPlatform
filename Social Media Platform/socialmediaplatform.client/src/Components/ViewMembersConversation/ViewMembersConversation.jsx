import React, { useState, useEffect } from 'react';
import './ViewMembersConversation.css';

const ViewMembersConversation = ({ groupId, onClose }) => {
    const [members, setMembers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchMembers = async () => {
        try {
            const response = await fetch(`https://localhost:44354/conversation/users/${groupId}`, {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                },
            });

            if (!response.ok) {
                throw new Error('Failed to fetch members.');
            }

            const data = await response.json();
            setMembers(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchMembers();
    }, [groupId]);

    return (
        <div className="modalOverlay">
            <div className="modalContent">
                <h3>Conversation Members</h3>
                <button className="closeButton" onClick={onClose}>
                    ✖
                </button>
                {loading ? (
                    <p>Loading members...</p>
                ) : error ? (
                    <p className="errorMessage">{error}</p>
                ) : (
                    <ul className="membersList">
                        {members.map((member) => (
                            <li key={member.id}>
                                <a href={`/profile/${member.userName}`} className="memberLink">
                                    @{member.userName}
                                </a>
                            </li>
                        ))}
                    </ul>
                )}
            </div>
        </div>
    );
};

export default ViewMembersConversation;
