import './ProfilePage.css';
import { useParams } from "react-router";
import Navbar from "@/Components/Navbar/Navbar.jsx";
import { useEffect, useState } from "react";
import ProfileCard from "@/Components/Profile/Profile.jsx";
import Post from "@/Components/Post/Post.jsx";
import Following from "../../Components/Following/Following.jsx";

const ProfilePage = () => {
    const { username } = useParams();
    const [profile, setProfile] = useState(null);
    const [error, setError] = useState("");
    const [postList, setPostList] = useState([]);
    const [loading, setLoading] = useState(true); // Unified loading state
    const [followStatus, setFollowStatus] = useState("Not Following");

    // Fetch follow status
    const fetchFollowRelation = async () => {
        try {
            const response = await fetch(`https://localhost:44354/follow/checkIfFollower/${username}`, {
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': `Bearer ${localStorage.getItem("token")}`,
                },
            });
            if (response.status === 202) {
                return "Pending";
            } else if (response.ok) {
                return "Following";
            } else {
                return "Not Following";
            }
        } catch (error) {
            console.error("Error fetching follow status:", error);
            return "Not Following";
        }
    };

    // Fetch user data
    const getUser = async () => {
        try {
            const response = await fetch(`https://localhost:44354/user/${username}`);
            if (response.ok) {
                const data = await response.json();
                return { profile: data, error: "" };
            } else {
                return { profile: null, error: `This profile does not exist` };
            }
        } catch (error) {
            return { profile: null, error: `An error occurred: ${error.message}` };
        }
    };

    // Fetch user's posts
    const getUsersPosts = async () => {
        try {
            const response = await fetch(`https://localhost:44354/posts/${username}`, { method: "GET" });
            if (response.ok) {
                const data = await response.json();
                return data;
            } else {
                console.error(`Failed to fetch posts: ${response.status} ${response.statusText}`);
                return [];
            }
        } catch (error) {
            console.error("Error fetching user posts:", error);
            return [];
        }
    };

    useEffect(() => {
        const loadData = async () => {
            setLoading(true);
            setProfile(null);
            setPostList([]);
            setError("");
            setFollowStatus("Not Following");

            const [userResult, postsResult, followResult] = await Promise.all([
                getUser(),
                getUsersPosts(),
                fetchFollowRelation(),
            ]);

            if (userResult.error) {
                setError(userResult.error);
            } else {
                setProfile(userResult.profile);
                setPostList(postsResult);
                setFollowStatus(followResult);
            }

            setLoading(false);
        };

        loadData();
    }, [username]);

    return (
        <>
            <Navbar />
            <section className="profileSection">
                {loading ? (
                    <p>Loading...</p>
                ) : error ? (
                    <p className="error-message errorMessage">{error}</p>
                ) : (
                    profile && (
                        <>
                            <ProfileCard
                                firstName={profile.firstName}
                                lastName={profile.lastName}
                                username={profile.username}
                                initialDescription={profile.description}
                                initialVisibility={profile.isPrivate}
                                followStatus={followStatus}
                                setFollowStatus={setFollowStatus}
                            />
                            <Following username={username} />
                            <span className="subtitle">User's posts</span>
                            <div className="postList">
                                {profile.isPrivate && username !== localStorage.getItem('username') ? (
                                    <>
                                        <p style={{ color: "white" }}>This user's profile is private.</p>
                                        <p style={{ color: "white" }}>You have to be a follower in order to view posts.</p>
                                    </>
                                ) : postList.length > 0 ? (
                                    postList.map((post) => <Post key={post.id} post={post} />)
                                ) : username === localStorage.getItem('username') ? (
                                    <p style={{ color: "white" }}>You haven't posted yet.</p>
                                ) : (
                                    <p style={{ color: "white" }}>This user hasn't posted yet.</p>
                                )}
                            </div>
                        </>
                    )
                )}
            </section>
        </>
    );
};

export default ProfilePage;
