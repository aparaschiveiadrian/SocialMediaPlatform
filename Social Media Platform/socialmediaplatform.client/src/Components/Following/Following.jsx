import './Following.css';
import { useState, useEffect } from 'react';
import ViewFollowingModal from "@/Components/ViewFollowingModal/ViewFollowingModal.jsx";

const Following = ({ username }) => {
    const [userId, setUserId] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [followings, setFollowings] = useState([]);
    const [followers, setFollowers] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [modalContent, setModalContent] = useState([]);
    const [listType, setListType] = useState("");
    
    const fetchUserId = async () => {
        try {
            const response = await fetch(`https://localhost:44354/getUserIdByUsername/${username}`);
            if (response.ok) {
                const rawData = await response.json();
                setUserId(rawData.userId);
                console.log("Fetched User ID:", rawData.userId);
                setError(null); 
            } else {
                setError("Failed to fetch user ID.");
            }
        } catch (err) {
            console.error("Error fetching user ID:", err);
            setError("Failed to fetch user ID. Please try again later.");
        } finally {
            setIsLoading(false);
        }
    };

    const fetchFollowersAndFollowings = async () => {
        if (!userId) return;

        try {
            const [followersResponse, followingsResponse] = await Promise.all([
                fetch(`https://localhost:44354/follow/getFollowersByUser/${userId}`, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${localStorage.getItem("token")}`,
                    },
                }),
                fetch(`https://localhost:44354/follow/getFollowingsByUser/${userId}`, {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${localStorage.getItem("token")}`,
                    },
                }),
            ]);

            if (followersResponse.ok) {
                const followersData = await followersResponse.json();
                setFollowers(followersData);
                console.log("Fetched Followers:", followersData);
            } else {
                setError("Failed to fetch followers.");
            }

            if (followingsResponse.ok) {
                const followingsData = await followingsResponse.json();
                setFollowings(followingsData);
                console.log("Fetched Followings:", followingsData);
            } else {
                setError("Failed to fetch followings.");
            }
        } catch (err) {
            console.error("Error fetching followers or followings:", err);
            setError("Failed to fetch user followers or followings. Please try again later.");
        }
    };

    const openModal = (contentType) => {
        if (contentType === 'followers') {
            setModalContent((prev)=>followers);
            setListType((prev)=>'Followers List')
        } else if (contentType === 'followings') {
            setModalContent((prev)=>followings);
            setListType((prev)=>'Followings List');
        }
        setShowModal(true);
    };

    const closeModal = () => {
        setShowModal(false);
        setModalContent([]); 
    };

    useEffect(() => {
        if (username) {
            fetchUserId();
        }
    }, [username]);

    useEffect(() => {
        if (userId) {
            fetchFollowersAndFollowings();
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
                <button onClick={() => openModal('followers')}>
                    {followers.length} Followers
                </button>
            </div>
            <div className="following">
                <button onClick={() => openModal('followings')}>
                    {followings.length} Following
                </button>
            </div>

            {showModal && (
                <ViewFollowingModal
                    list={listType}
                    content={modalContent}
                    onClose={closeModal}
                />
            )}
        </div>
    );
};

export default Following;
