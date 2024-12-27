import './UnfollowButton.css';
import { useState } from 'react';

const UnfollowButton = ({ text, username }) => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const handleUnfollow = async () => {
        setLoading(true);
        setError(null);

        try {
            const userIdResponse = await fetch(
                `https://localhost:44354/getUserIdByUsername/${username}`,
                {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                }
            );

            if (userIdResponse.ok) {
                const { userId } = await userIdResponse.json();

                const unfollowResponse = await fetch(
                    `https://localhost:44354/follow/delete/following/${userId}`,
                    {
                        method: 'DELETE',
                        headers: {
                            'Content-Type': 'application/json',
                            Authorization: `Bearer ${localStorage.getItem('token')}`,
                        },
                    }
                );

                if (unfollowResponse.ok) {
                    setLoading(false);
                    window.location.reload();
                }
            }
        } catch (err) {
            setError(err.message);
            setLoading(false);
        }
    };

    return (
        <button
            className="unfollowBtn"
            onClick={handleUnfollow}
            disabled={loading}
        >
            <span className="IconContainer">
                <svg
                    width="24px"
                    height="24px"
                    viewBox="0 0 1024 1024"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="#000000"
                >
                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                    <g id="SVGRepo_iconCarrier">
                        <path
                            fill="#fe6262"
                            d="M512 64a448 448 0 1 1 0 896 448 448 0 0 1 0-896zM288 512a38.4 38.4 0 0 0 38.4 38.4h371.2a38.4 38.4 0 0 0 0-76.8H326.4A38.4 38.4 0 0 0 288 512z"
                        ></path>
                    </g>
                </svg>
            </span>
            <p className="text-unfollow">
                {loading ? 'Processing...' : error ? 'Error' : text || 'Unfollow'}
            </p>
        </button>
    );
};

export default UnfollowButton;
