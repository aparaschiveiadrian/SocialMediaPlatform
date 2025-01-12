import './Following.css';
import { useState, useEffect } from 'react';
import ViewFollowingModal from "@/Components/ViewFollowingModal/ViewFollowingModal.jsx";

const Following = ({ username, isPrivate, isFollowing }) => {
    const [userId, setUserId] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [followings, setFollowings] = useState([]);
    const [followers, setFollowers] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [modalContent, setModalContent] = useState([]);
    const [listType, setListType] = useState("");
    const [followCount, setFollowCount] = useState(0);
    const [followingCount, setFollowingCount] = useState(0);

    // Fetch follower and following counts
    const fetchFollowingCount = async () => {
        try {
            const response = await fetch(`https://localhost:44354/follow/getFollowCounter/${username}`);
            if (response.ok) {
                const data = await response.json();
                setFollowCount(data.followerCounter);
                setFollowingCount(data.followingCounter);
            } else {
                setError("Failed to fetch follower/following counts.");
            }
        } catch (e) {
            console.error('Failed to fetch follower and following counts:', e);
            setError("Failed to fetch counts. Please try again later.");
        }
    };

    // Fetch user ID
    const fetchUserId = async () => {
        try {
            const response = await fetch(`https://localhost:44354/getUserIdByUsername/${username}`);
            if (response.ok) {
                const rawData = await response.json();
                setUserId(rawData.userId);
            } else {
                setError("Failed to fetch user ID.");
            }
        } catch (err) {
            console.error("Error fetching user ID:", err);
            setError("Failed to fetch user ID. Please try again later.");
        }
    };

    // Fetch followers and followings
    const fetchFollowersAndFollowings = async () => {
        try {
            const [followersResponse, followingsResponse] = await Promise.all([
                fetch(`https://localhost:44354/follow/getFollowersByUser/${userId}`, {
                    headers: {
                        "Authorization": `Bearer ${localStorage.getItem("token")}`,
                    },
                }),
                fetch(`https://localhost:44354/follow/getFollowingsByUser/${userId}`, {
                    headers: {
                        "Authorization": `Bearer ${localStorage.getItem("token")}`,
                    },
                }),
            ]);

            if (followersResponse.ok) {
                setFollowers(await followersResponse.json());
            } else {
                setError("Failed to fetch followers.");
            }

            if (followingsResponse.ok) {
                setFollowings(await followingsResponse.json());
            } else {
                setError("Failed to fetch followings.");
            }
        } catch (err) {
            console.error("Error fetching lists:", err);
            setError("Failed to fetch lists. Please try again later.");
        }
    };

    const openModal = (contentType) => {
        if (contentType === 'followers') {
            setModalContent(followers);
            setListType('Followers List');
        } else {
            setModalContent(followings);
            setListType('Followings List');
        }
        setShowModal(true);
    };

    const closeModal = () => {
        setShowModal(false);
        setModalContent([]);
    };

    useEffect(() => {
        setIsLoading(true);
        const loadData = async () => {
            await fetchFollowingCount();
            await fetchUserId();
        };
        loadData().finally(() => setIsLoading(false));
    }, [username]);

    useEffect(() => {
        if (userId && (!isPrivate || isFollowing || localStorage.getItem("username") == username)) {
            fetchFollowersAndFollowings();
        }
    }, [userId, isPrivate, isFollowing]);

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
                    {followCount} Followers
                </button>
            </div>
            <div className="following">
                <button onClick={() => openModal('followings')}>
                    {followingCount} Following
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
