import './ProfilePage.css';
import { useParams } from "react-router";
import Navbar from "@/Components/Navbar/Navbar.jsx";
import { useEffect, useState } from "react";
import ProfileCard from "@/Components/Profile/Profile.jsx";
import Post from "@/Components/Post/Post.jsx";

const ProfilePage = () => {
    const { username } = useParams();
    const [profile, setProfile] = useState(null);
    const [error, setError] = useState("");
    const [postList, setPostList] = useState([]);

    const getUser = async () => {
        try {
            const response = await fetch(`https://localhost:44354/user/${username}`);
            if (response.ok) {
                const data = await response.json();
                setProfile(data);
                setError("");
            } else {
                setError(`This profile does not exist`);
            }
        } catch (error) {
            setError(`An error occurred: ${error.message}`);
        }
    };

    const getUsersPosts = async () => {
        try {
            const response = await fetch(`https://localhost:44354/posts/${username}`, {
                method: "GET",
            });
            if (response.ok) {
                const data = await response.json();
                setPostList(data);
            } else {
                console.error(`Failed to fetch posts: ${response.status} ${response.statusText}`);
            }
        } catch (error) {
            console.error("An error occurred while fetching user posts:", error);
        }
    };

    useEffect(() => {
        setProfile(null);
        setPostList([]);
        setError("");

        getUser();
        getUsersPosts();
    }, [username]); 

    return (
        <>
            <Navbar />
            <section className="profileSection">
                {error ? (
                    <p className="error-message errorMessage">{error}</p>
                ) : profile ? (
                    <>
                        <ProfileCard
                            firstName={profile.firstName}
                            lastName={profile.lastName}
                            username={profile.username}
                            initialDescription={profile.description}
                        />
                        <span className="subtitle">User's posts</span>
                        <div className="postList">
                            {postList.length > 0 ? (
                                postList.map((post) => <Post key={post.id} post={post} />)
                            ) : (
                                <p style={{ color: "white" }}>This user hasn't posted yet.</p>
                            )}
                        </div>
                    </>
                ) : (
                    <p>Loading...</p>
                )}
            </section>
        </>
    );
};

export default ProfilePage;
