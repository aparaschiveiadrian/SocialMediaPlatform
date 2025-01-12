import React, { useEffect, useState } from 'react';
import './Conversations.css';
import Chat from "@/Components/Chat/Chat.jsx";

const Conversations = () => {
    const [groups, setGroups] = useState([]);
    const [selectedGroup, setSelectedGroup] = useState(null);
    const [loadingGroups, setLoadingGroups] = useState(true);
    const [error, setError] = useState(null);

    const fetchGroups = async () => {
        try {
            const response = await fetch('https://localhost:44354/user/groups', {
                method: 'GET',
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                },
            });

            if (!response.ok) {
                throw new Error('Failed to fetch groups.');
            }

            const data = await response.json();
            setGroups(data);
            if (data.length > 0) {
                setSelectedGroup(data[0]);
            }
        } catch (err) {
            setError(err.message);
        } finally {
            setLoadingGroups(false);
        }
    };

    const handleGroupClick = (group) => {
        setSelectedGroup(group);
    };

    const handleLeaveConversation = () => {
        setSelectedGroup(null);
        fetchGroups();
    };

    const handleViewMembers = () => {
        console.log('View members clicked');
       
    };

    useEffect(() => {
        fetchGroups();
    }, []);

    return (
        <div className="conversationsPage">
            {loadingGroups ? (
                <p className="loadingMessage">Loading groups...</p>
            ) : error ? (
                <p className="errorMessage">{error}</p>
            ) : (
                <div className="conversationsLayout">
                    <div className="groupsPanel">
                        <h3 className="panelTitle">Your Groups</h3>
                        <ul className="groupsList">
                            {groups.map(group => (
                                <li
                                    key={group.id}
                                    className={`groupItem ${selectedGroup?.id === group.id ? 'activeGroup' : ''}`}
                                    onClick={() => handleGroupClick(group)}
                                >
                                    {group.name}
                                </li>
                            ))}
                        </ul>
                    </div>

                    <div className="chatPanel">
                        {selectedGroup ? (
                            <Chat
                                groupId={selectedGroup.id}
                                groupName={selectedGroup.name}
                                onLeaveConversation={handleLeaveConversation}
                                onViewMembers={handleViewMembers}
                            />
                        ) : (
                            <p className="selectGroupMessage">Select a group to view messages.</p>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
};

export default Conversations;
