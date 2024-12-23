import './Following.css';
import { useState, useEffect } from 'react';

const Following = ({ username }) => {
    const [userId, setUserId] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [followings, setFollowings] = useState([]);
    const [followers, setFollowers] = useState([]);
    
    const fetchUserId = async () => {
        setIsLoading(true);
        setError(null);

        try {
            const response = await fetch(`https://localhost:44354/getUserIdByUsername/${username}`);

            if (!response.ok) {
                console.error(`Failed to fetch user ID for username: ${username}. Status: ${response.status}`);
                setError("Failed to fetch user ID.");
                return;
            }

            const rawData = await response.json();
            setUserId(rawData.userId); 
            console.log("Fetched User ID:", rawData.userId);

        } catch (err) {
            console.error("Error fetching user ID:", err);
            setError("Failed to fetch user ID. Please try again later.");
        } finally {
            setIsLoading(false);
        }
    };
    
    const fetchFollowers = async () => {
        if (!userId) return; 
        setIsLoading(true);

        try {
            const response = await fetch(`https://localhost:44354/follow/getFollowersByUser/${userId}`,{
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${localStorage.getItem("token")}`,
                }
            });
            
            const data = await response.json();
            console.log("Fetched Followers:", data);
            setFollowers(data);
        } catch (err) {
            console.error("Error fetching followers:", err);
            setError("Failed to fetch user followers list. Please try again later.");
        } finally {
            setIsLoading(false);
        }
    };
    
    useEffect(() => {
        if (username) {
            fetchUserId();
        }
    }, [username]);
    
    useEffect(() => {
        if (userId) {
            fetchFollowers();
        }
    }, [userId]);

    if (isLoading) {
        return <div className="followingContainer">Loading...</div>;
    }

    if (error) {
        return <div className="followingContainer error">{error}</div>;
    }

    return (
        <div className="followingContainer">
            <div className="followers">
                <a>{followers.length} Followers</a>
            </div>
            <div className="following">
                <a>{followings.length} Following</a>
            </div>
        </div>
    );
};

export default Following;
