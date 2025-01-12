import React, { useState } from 'react';
import Media from './Media';
import CommentSection from './CommentSection';
import UsersComments from './UsersComments.jsx';
import { FaTrashAlt } from 'react-icons/fa';

import SVGthreeDots from "@/Components/SVGs/SVGthreeDots.jsx";

const Post = ({ post }) => {
    const [reload, setReload] = useState(false);
    const [openMenuId, setOpenMenuId] = useState(null);

    const toggleMenu = (postId) => {
        setOpenMenuId((prev) => (prev === postId ? null : postId));
    };

    const handleDelete = async (postId) => {
        try {
            const response = await fetch(`https://localhost:44354/post/delete/${postId}`, {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${localStorage.getItem("token")}`,
                },
            });
            if (response.ok) {
                window.location.reload(); // Reloads the page
                setReload(!reload);
            } else {
                window.alert("You are not authorized to delete this post.");
            }
        } catch (error) {
            window.alert(`An error occurred while deleting the post: ${error}`);
        }
    };

    return (
        <div className="post">
            <div className="postDetails">
                <span>
                    <small className="postDate">
                        {new Date(post.createdAt).toLocaleString()}
                    </small>
                    {(post.username === localStorage.getItem("username") || localStorage.getItem("isAdmin") === "true") && (
                        <div className="threeDotsWrapper">
                            <button
                                className="commentOptions threeDotsIconPost"
                                onClick={() => handleDelete(post.id)}
                            >
                                <FaTrashAlt style={{ fontSize: '1.5rem', color: 'rgb(237, 239, 243)' }} />
                            </button>
                            
                        </div>
                    )}
                </span>
                <a href={`/profile/${post.username}`} className="userTag">
                    @{post.username}
                </a>
            </div>

            <p className="postContent">{post.content}</p>
            {post.mediaUrl && (
                <div className="mediaWrapper">
                    <Media mediaUrl={post.mediaUrl} contentType={post.contentType} />
                </div>
            )}
            <hr className="postDivider" />
            <UsersComments postId={post.id} reload={reload} />
            <hr className="postDivider" />
            <CommentSection postId={post.id} setReload={setReload} />
        </div>
    );
};

export default Post;
