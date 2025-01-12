import React, { useState } from 'react';
import Media from './Media';
import CommentSection from './CommentSection';
import UsersComments from './UsersComments.jsx';

const Post = ({ post }) => {
    const [reload, setReload] = useState(false);

    return (
        <div className="post">
            <div className="postDetails">
                <small className="postDate">
                    {new Date(post.createdAt).toLocaleString()}
                </small>
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
