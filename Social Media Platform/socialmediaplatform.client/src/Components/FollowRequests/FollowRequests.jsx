import { useEffect, useState } from "react";
import "./FollowRequests.css";

const FollowRequests = () => {
    const [followRequests, setFollowRequests] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchFollowRequests = async () => {
        try {
            const response = await fetch("https://localhost:44354/follow/getFollowRequests", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                    "Content-Type": "application/json",
                },
            });
            if (!response.ok) throw new Error("Failed to fetch follow requests");
            setFollowRequests(await response.json());
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    const getUserIdByUsername = async (username) => {
        try {
            const response = await fetch(`https://localhost:44354/getUserIdByUsername/${username}`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                    "Content-Type": "application/json",
                },
            });
            if (!response.ok) throw new Error("Failed to fetch user ID");
            const data = await response.json();
            return data.userId;
        } catch (err) {
            setError(err.message);
            return null;
        }
    };

    const handleRequest = async (username, action) => {
        try {
            const userId = await getUserIdByUsername(username);
            if (!userId) throw new Error("Invalid user ID");

            const response = await fetch(`https://localhost:44354/follow/${action}/${userId}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                },
            });
            if (!response.ok) throw new Error(`Failed to ${action} follow request`);
            setFollowRequests((prev) => prev.filter((name) => name !== username));
        } catch (err) {
            setError(err.message);
        }
    };

    useEffect(() => {
        fetchFollowRequests();
    }, []);

    return (
        <div className="followRequestsContainer">
            <h2 className="followRequestsTitle">Follow Requests</h2>
            {loading ? (
                <p className="loadingMessage">Loading...</p>
            ) : error ? (
                <p className="errorMessage">{error}</p>
            ) : followRequests.length > 0 ? (
                <ul className="followRequestsList">
                    {followRequests.map((username) => (
                        <li key={username} className="followRequestItem">
                            <span className="requestId">{username}</span>
                            <button className="acceptButton" onClick={() => handleRequest(username, "accept")}>
                                Accept
                            </button>
                            <button className="rejectButton" onClick={() => handleRequest(username, "decline")}>
                                Reject
                            </button>
                        </li>
                    ))}
                </ul>
            ) : (
                <p className="noRequestsMessage">No follow requests at the moment.</p>
            )}
        </div>
    );
};

export default FollowRequests;
