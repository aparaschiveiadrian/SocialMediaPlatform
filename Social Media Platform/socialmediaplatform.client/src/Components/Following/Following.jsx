import './Following.css';
import { useState, useEffect } from 'react';

const Following = ({ username }) => {
    const [following, setFollowing] = useState(0);
    const [followers, setFollowers] = useState(0);

    const fetchData = async () => {
        try {
            const userIdResponse = await fetch(`https://localhost:44354/getUserIdByUsername/${username}`);
            if (!userIdResponse.ok) {
                console.error("Failed to fetch user ID");
                return;
            }

            const { userId } = await userIdResponse.json();
            
            const [followersResponse, followingResponse] = await Promise.all([
                fetch(`https://localhost:44354/follow/GetFollowersByUser/${userId}`),
                fetch(`https://localhost:44354/follow/GetFollowingsByUser/${userId}`)
            ]);

            if (followersResponse.ok) {
                const followersData = await followersResponse.json();
                setFollowers(followersData.length);
            } else {
                console.error("Failed to fetch followers");
            }

            if (followingResponse.ok) {
                const followingData = await followingResponse.json();
                setFollowing(followingData.length);
            } else {
                console.error("Failed to fetch followings");
            }
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    };
    
    useEffect(() => {
        if (username) fetchData();
    }, [username]);

    return (
        <div className="followingContainer">
            <div className="followers">
                <a>
                    {followers} Followers
                </a>
            </div>
            <div className="following">
                <a>
                    {following} Following
                </a>
            </div>
        </div>
    );
};

export default Following;
