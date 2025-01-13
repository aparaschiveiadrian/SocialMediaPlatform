import React, { useState } from "react";
import "./ConversationRequestsButton.css"; 

const ConversationRequestsButton = ({ conversationId }) => {
    const [isModalOpen, setModalOpen] = useState(false);
    const [requests, setRequests] = useState([]);
    const [loading, setLoading] = useState(false);

    const fetchRequests = async () => {
        const token = localStorage.getItem("token");

        setLoading(true);
        try {
            const response = await fetch(
                `https://localhost:44354/conversation/get/requests/${conversationId}`,
                {
                    method: "GET",
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                }
            );

            if (response.ok) {
                const data = await response.json();
                setRequests(data);
                setModalOpen(true);
            } else {
                console.error("Failed to fetch requests");
            }
        } catch (error) {
            console.error("Error fetching requests:", error);
        } finally {
            setLoading(false);
        }
    };

    const handleAccept = async (username) => {
        const token = localStorage.getItem("token");

        try {
            const response = await fetch(
                `https://localhost:44354/conversation/accept/request/${username}/${conversationId}`,
                {
                    method: "PUT",
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                }
            );

            if (response.ok) {
                setRequests((prevRequests) =>
                    prevRequests.filter((request) => request !== username)
                );
            } else {
                console.error("Failed to accept request");
            }
        } catch (error) {
            console.error("Error accepting request:", error);
        }
    };

    const handleDecline = async (username) => {
        const token = localStorage.getItem("token");

        try {
            const response = await fetch(
                `https://localhost:44354/conversation/decline/request/${username}/${conversationId}`,
                {
                    method: "PUT",
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                }
            );

            if (response.ok) {
                setRequests((prevRequests) =>
                    prevRequests.filter((request) => request !== username)
                );
            } else {
                console.error("Failed to decline request");
            }
        } catch (error) {
            console.error("Error declining request:", error);
        }
    };

    return (
        <>
            <button className="btn btn-chat" onClick={fetchRequests}>
                REQUESTS
            </button>
            {isModalOpen && (
                <div className="modal-overlay">
                    <div className="modal">
                        <button className="modal-close" onClick={() => setModalOpen(false)}>
                            &times;
                        </button>
                        <h2>Join Requests</h2>
                        {loading ? (
                            <p>Loading...</p>
                        ) : requests.length > 0 ? (
                            <ul style={{ listStyleType: "none", padding: 0 }}>
                                {requests.map((username) => (
                                    <li
                                        key={username}
                                        style={{
                                            display: "flex",
                                            justifyContent: "space-between",
                                            alignItems: "center",
                                            marginBottom: "10px",
                                        }}
                                    >
                                        <span>{username}</span>
                                        <div>
                                            <button
                                                onClick={() => handleAccept(username)}
                                                style={{
                                                    marginRight: "5px",
                                                    padding: "5px 10px",
                                                    backgroundColor: "#4caf50",
                                                    color: "#fff",
                                                    border: "none",
                                                    borderRadius: "4px",
                                                    cursor: "pointer",
                                                }}
                                            >
                                                Accept
                                            </button>
                                            <button
                                                onClick={() => handleDecline(username)}
                                                style={{
                                                    padding: "5px 10px",
                                                    backgroundColor: "#f44336",
                                                    color: "#fff",
                                                    border: "none",
                                                    borderRadius: "4px",
                                                    cursor: "pointer",
                                                }}
                                            >
                                                Decline
                                            </button>
                                        </div>
                                    </li>
                                ))}
                            </ul>
                        ) : (
                            <p>There are no requests to join.</p>
                        )}
                    </div>
                </div>
            )}
        </>
    );
};

export default ConversationRequestsButton;
