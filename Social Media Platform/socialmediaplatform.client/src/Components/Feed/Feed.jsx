import './Feed.css';
import React from 'react';
import UserPost from "@/Components/UserPost/UserPost.jsx";

const Feed = () => {
    const obj = [
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

    return (
        <section className="containerFeed">
            <UserPost />
            <div className="containerPosts">
                <h2 className="globalPostsInfo">Latest Posts</h2>
                {obj.map((post) => (
                    <div key={post.id} className="post">
                        <small className="postDate">
                            {new Date(post.createdAt).toLocaleString()}
                        </small>
                        <p className="postContent">{post.content}</p>
                        {post.mediaUrl && (
                            <div className="postMedia">
                                {post.contentType === "photo" ? (
                                    <img src={post.mediaUrl} alt="Sursa" />
                                ) : (
                                    <video src={post.mediaUrl} controls />
                                )}
                            </div>
                        )}
                    </div>
                ))}
            </div>
        </section>
    );
};

export default Feed;
