import './Feed.css';
import { useState, useEffect } from 'react';
import UserPost from "@/Components/UserPost/UserPost.jsx";

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

    const handleInputChange = (event, postId) => {
        if (event.key === "Enter" && !event.shiftKey) {
            event.preventDefault();
            console.log(`Post ${postId} comment:`, event.target.value);
            event.target.value = "";
        }
    };

    const renderPost = (post) => (
        <div key={post.id} className="post">
            <small className="postDate">
                {new Date(post.createdAt).toLocaleString()}
            </small>
            <p className="postContent">{post.content}</p>
            {post.mediaUrl && renderMedia(post)}
            <hr className="postDivider" />
            <textarea
                className="postInput"
                placeholder="Add a comment..."
                onKeyDown={(event) => handleInputChange(event, post.id)}
            ></textarea>
        </div>
    );

    const renderMedia = (post) => {
        if (post.contentType === "photo") {
            return <img src={post.mediaUrl} alt="Source" className="postMedia" />;
        } else if (post.contentType === "video") {
            return <video src={post.mediaUrl} controls className="postMedia" />;
        }
    };

    return (
        <section className="containerFeed">
            <UserPost />
            <div className="containerPosts">
                <h2 className="globalPostsInfo">Latest Posts</h2>
                {loading ? (
                    <p>Loading posts...</p>
                ) : postList.length > 0 ? (
                    postList.map(renderPost)
                ) : (
                    <p>No posts available</p>
                )}
            </div>
        </section>
    );
};

export default Feed;
