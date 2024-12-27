import { useEffect, useState } from "react";
import './FollowRequests.css'; // Add the dark theme styles here

const FollowRequests = () => {
    const [followRequests, setFollowRequests] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchFollowRequests = async () => {
            try {
                const response = await fetch('https://localhost:44354/follow/getFollowRequests', {
                    method: 'GET',
                    headers: {
                        'Authorization': `Bearer ${localStorage.getItem('token')}`,
                        'Content-Type': 'application/json',
                    },
                });
                if(response.ok)
                {
                    const data = await response.json();
                    setFollowRequests(data);    
                }
                
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchFollowRequests();
    }, []);

    return (
        <div className="followRequestsContainer">
            <h2 className="followRequestsTitle">Follow Requests</h2>
            {loading ? (
                <p className="loadingMessage">Loading...</p>
            ) : error ? (
                <p className="errorMessage">{error}</p>
            ) : (
                <ul className="followRequestsList">
                    {followRequests.length > 0 ? (
                        followRequests.map((requestId) => (
                            <li key={requestId} className="followRequestItem">
                                <span className="requestId">{requestId}</span>
                                <button className="acceptButton">Accept</button>
                                <button className="rejectButton">Reject</button>
                            </li>
                        ))
                    ) : (
                        <p className="noRequestsMessage">No follow requests at the moment.</p>
                    )}
                </ul>
            )}
        </div>
    );
};

export default FollowRequests;
