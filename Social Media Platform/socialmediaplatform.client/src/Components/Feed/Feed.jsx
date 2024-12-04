import './Feed.css';
import { useState, useEffect } from 'react';
import UserPost from "@/Components/UserPost/UserPost.jsx";

const Feed = () => {
    const [postList, setPostList] = useState([]);

    const fetchPosts = async () => {
        try {
            const response = await fetch('https://localhost:44354/posts');
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json();
            setPostList(data);
        } catch (error) {
            console.error('Error fetching posts:', error);
        }
    };

    useEffect(() => {
        fetchPosts();
    }, []);

    // Temporary test object
    const placeholderPosts = [
        {
            id: 1,
            contentType: "text",
            createdAt: "2024-12-04T12:00:00Z",
            content: "This is a text post",
            mediaUrl: null,
        },
        {
            id: 2,
            contentType: "photo",
            createdAt: "2024-12-04T12:05:00Z",
            content: "Check out this amazing view!",
            mediaUrl: "https://example.com/images/photo.jpg",
        },
    ];

    const handleInputChange = (event, postId) => {
        if (event.key === "Enter" && !event.shiftKey) {
            event.preventDefault(); 
            console.log(`Post ${postId} comment:`, event.target.value);
            event.target.value = ""; 
        }
    };

    const renderPost = (post) => {
        return (
            <div key={post.id} className="post">
                <small className="postDate">
                    {new Date(post.createdAt).toLocaleString()}
                </small>
                <p className="postContent">{post.content}</p>
                {post.mediaUrl && renderMedia(post)}
                <hr className="postDivider"/>
                <textarea
                    className="postInput"
                    placeholder="Add a comment..."
                    onKeyDown={(event) => handleInputChange(event, post.id)}
                ></textarea>
            </div>
        );
    };

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
                {placeholderPosts.map(renderPost)}
            </div>
        </section>
    );
};

export default Feed;
