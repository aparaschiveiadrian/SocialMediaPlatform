import './Feed.css';
import { useState, useEffect } from 'react';
import UserPost from "@/Components/UserPost/UserPost.jsx";
import Post from "@/Components/Post/Post.jsx";

const Feed = () => {
    const [postList, setPostList] = useState([]);
    const [loading, setLoading] = useState(true);

    const fetchPosts = () => {
        fetch('https://localhost:44354/posts')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                console.log(data);
                setPostList(data);
            })
            .catch(error => {
                console.error('Error fetching posts:', error);
            })
            .finally(() => {
                setLoading(false);
            });
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
                    <p>Loading posts...</p>
                ) : postList.length > 0 ? (
                    postList.map(post => <Post key={post.id} post={post} />)
                ) : (
                    <p className={"errorMessage"}>No posts available</p>
                )}
            </div>
        </section>
    );
};

export default Feed;
