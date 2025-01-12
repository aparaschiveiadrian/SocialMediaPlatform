import './Feed.css';
import { useState, useEffect } from 'react';
import UserPost from "@/Components/UserPost/UserPost.jsx";
import Post from "@/Components/Post/Post.jsx";

const Feed = () => {
    const [postList, setPostList] = useState([]);
    const [loading, setLoading] = useState(true);

    const fetchPosts = async () => {
        try {
            const response = await fetch('https://localhost:44354/posts');
            if (!response.ok) {
                throw new Error('Failed to fetch posts. Please try again.');
            }
            const data = await response.json();
            setPostList(data);
        } catch (error) {
            console.error('Error fetching posts:', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPosts();
    }, []);

    return (
        <section className="containerFeed">
            <UserPost />
            <div className="containerPosts">
                <h2 className="globalPostsInfo">Latest Posts</h2>
                {loading ? (
                    <p className="loadingMessage">Loading posts...</p>
                ) : postList.length > 0 ? (
                    postList.map((post) => <Post key={post.id} post={post} />)
                ) : (
                    <p className="errorMessage">No posts available</p>
                )}
            </div>
        </section>
    );
};

export default Feed;
