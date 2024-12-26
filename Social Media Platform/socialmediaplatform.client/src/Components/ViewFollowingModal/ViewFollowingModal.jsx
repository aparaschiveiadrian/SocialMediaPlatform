import './ViewFollowingModal.css';
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom'; // Import Link from react-router-dom

const ViewFollowingModal = ({ list, content, onClose }) => {
    const [userDetails, setUserDetails] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const fetchUserDetails = async () => {
        try {
            setLoading(true);
            const responses = await Promise.all(
                content.map(username =>
                    fetch(`https://localhost:44354/user/${username}`, {
                        method: 'GET',
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${localStorage.getItem('token')}`,
                        },
                    }).then(response => {
                        if (!response.ok) {
                            throw new Error(`Failed to fetch details for ${username}`);
                        }
                        return response.json();
                    })
                )
            );
            setUserDetails(responses);
        } catch (err) {
            console.error("Error fetching user details:", err);
            setError("Failed to fetch user details. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        if (content && content.length > 0) {
            fetchUserDetails();
        }
    }, [content]);

    return (
        <div className="modalOverlay">
            <div className="modalContent">
                <button className="closeButton" onClick={onClose}>
                    &times;
                </button>
                <h2>{list}</h2>
                {loading ? (
                    <p>Loading...</p>
                ) : error ? (
                    <p className="error">{error}</p>
                ) : (
                    <ul className="userList">
                        {userDetails.map((user, index) => (
                            <li key={index} className="userListItem">
                                <Link to={`/profile/${user.username}`} className="userLink">
                                    <strong>{user.username}</strong> <br/>
                                    <span className="userName">{`${user.firstName} ${user.lastName}`}</span>
                                </Link>
                            </li>
                        ))}
                    </ul>
                )}
                {!loading && userDetails.length === 0 && !error && <p>No items to display.</p>}
            </div>
        </div>
    );
};

export default ViewFollowingModal;
