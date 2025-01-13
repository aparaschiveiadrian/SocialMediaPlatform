import React, { useState } from "react";
import ViewMembersModal from "@/Components/ViewMembersModal/ViewMembersModal.jsx";

const ShowMembersButton = ({ conversationId, userIsModerator }) => {
    const [members, setMembers] = useState([]);
    const [isModalOpen, setModalOpen] = useState(false);

    const currentUsername = localStorage.getItem("username");

    const handleMembers = async () => {
        const token = localStorage.getItem("token");

        try {
            const response = await fetch(`https://localhost:44354/conversation/users/${conversationId}`, {
                method: "GET",
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            if (response.ok) {
                const data = await response.json();
                setMembers(data);
                setModalOpen(true);
            } else {
                console.error("Failed to fetch members");
            }
        } catch (error) {
            console.error("Error fetching members:", error);
        }
    };

    const handleKick = async (userId) => {
        const token = localStorage.getItem("token");

        try {
            const response = await fetch(
                `https://localhost:44354/conversation/kick/${conversationId}/${userId}`,
                {
                    method: "POST",
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                }
            );

            if (response.ok) {
                setMembers((prevMembers) => prevMembers.filter((member) => member.id !== userId));
            } else {
                console.error("Failed to kick member");
            }
        } catch (error) {
            console.error("Error kicking member:", error);
        }
    };

    return (
        <>
            <button className="btn btn-chat" onClick={handleMembers}>
                View Members
            </button>
            <ViewMembersModal isOpen={isModalOpen} onClose={() => setModalOpen(false)}>
                <h2>Members</h2>
                <ul style={{ listStyle: "none", padding: 0 }}>
                    {members.map((member) => (
                        <li
                            key={member.id}
                            style={{
                                display: "flex",
                                justifyContent: "space-between",
                                alignItems: "center",
                                marginBottom: "10px",
                            }}
                        >
                            <a
                                href={`/profile/${member.userName}`}
                                target="_blank"
                                rel="noopener noreferrer"
                                style={{ textDecoration: "none", color: "#4dabf7" }}
                            >
                                @{member.userName}
                            </a>
                            {userIsModerator && member.userName !== currentUsername && (
                                <button
                                    onClick={() => handleKick(member.id)}
                                    style={{
                                        padding: "5px 10px",
                                        backgroundColor: "#ff4d4d",
                                        border: "none",
                                        borderRadius: "4px",
                                        color: "#fff",
                                        cursor: "pointer",
                                        fontSize: "0.9rem",
                                    }}
                                >
                                    Kick
                                </button>
                            )}
                        </li>
                    ))}
                </ul>
            </ViewMembersModal>
        </>
    );
};

export default ShowMembersButton;
