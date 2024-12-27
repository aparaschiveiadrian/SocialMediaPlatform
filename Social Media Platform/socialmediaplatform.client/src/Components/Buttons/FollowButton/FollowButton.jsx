import './FollowButton.css';
import { useState } from 'react';
const FollowButton = ({ username }) => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [followingId, setFollowingId] = useState(null);
    
    const getProfileId = async () => {
        setLoading(true);
        try {
            const response = await fetch(`https://localhost:44354/getUserIdByUsername/${username}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (response.ok) {
                const data = await response.json();
                const userId = data.userId;
                setFollowingId(userId);
                await followUser(userId);
            } else {
                setError('Failed to fetch user ID');
            }
        } catch (err) {
            setError('An error occurred while fetching user ID');
        } finally {
            setLoading(false);
        }
    };
    const followUser = async (userId) => {
        try {
            const response = await fetch(`https://localhost:44354/follow/${userId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${localStorage.getItem('token')}`,
                },
            });

            if (response.ok) {
                setError(null);
                window.location.reload();
            } else {
                setError('Failed to follow the user');
            }
        } catch (err) {
            setError('An error occurred while following the user');
        }
    };

    return (
        <div style={{display: "flex", flexDirection: "column", alignItems: "center", gap:"1rem"}}>
            <button
                className="followingBtn"
                onClick={getProfileId}  // get the id and follow the person with that id
                disabled={loading} 
            >
                <span className="IconContainer">
                    <svg
                        fill="#f5fdff"
                        height="24px"
                        width="24px"
                        version="1.1"
                        id="Layer_1"
                        xmlns="http://www.w3.org/2000/svg"
                        xmlnsXlink="http://www.w3.org/1999/xlink"
                        viewBox="0 0 328 328"
                        xmlSpace="preserve"
                        stroke="#f5fdff"
                    >
                        <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                        <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                        <g id="SVGRepo_iconCarrier">
                            <g id="XMLID_350_">
                                <path
                                    id="XMLID_351_"
                                    d="M52.25,64.001c0,34.601,28.149,62.749,62.75,62.749c34.602,0,62.751-28.148,62.751-62.749 S149.602,1.25,115,1.25C80.399,1.25,52.25,29.4,52.25,64.001z"
                                ></path>
                                <path
                                    id="XMLID_352_"
                                    d="M217.394,262.357c2.929,2.928,6.768,4.393,10.606,4.393c3.839,0,7.678-1.465,10.607-4.394 c5.857-5.858,5.857-15.356-0.001-21.214l-19.393-19.391l19.395-19.396c5.857-5.858,5.857-15.356-0.001-21.214 c-5.858-5.857-15.356-5.856-21.214,0.001l-30,30.002c-2.813,2.814-4.393,6.629-4.393,10.607c0,3.979,1.58,7.794,4.394,10.607 L217.394,262.357z"
                                ></path>
                                <path
                                    id="XMLID_439_"
                                    d="M15,286.75h125.596c19.246,24.348,49.031,40,82.404,40c57.896,0,105-47.103,105-105 c0-57.896-47.104-105-105-105c-34.488,0-65.145,16.716-84.297,42.47c-7.764-1.628-15.695-2.47-23.703-2.47 c-63.411,0-115,51.589-115,115C0,280.034,6.716,286.75,15,286.75z M223,146.75c41.355,0,75,33.645,75,75s-33.645,75-75,75 s-75-33.645-75-75S181.644,146.75,223,146.75z"
                                ></path>
                            </g>
                        </g>
                    </svg>
                </span>
                <p className="text-follow">Follow</p>
                {loading && <span>Loading...</span>} 
                 
            </button>
            {error && <span className="errorMessage">
                Please log in first.
            </span>}
        </div>
    );
};

export default FollowButton;
