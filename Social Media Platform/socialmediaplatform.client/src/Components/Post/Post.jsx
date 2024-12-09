import React, { useState } from 'react';
import Media from './Media';
import CommentSection from './CommentSection';

const Post = ({ post }) => {
    return (
        <div className="post">
            <div className="postDetails">
                <small className="postDate">
                    {new Date(post.createdAt).toLocaleString()}
                </small>
                <a href={`/profile/${post.username}`} className={'userTag'}>
                    @{post.username}
                </a>
            </div>

            <p className="postContent">{post.content}</p>
            {post.mediaUrl && <Media mediaUrl={post.mediaUrl} contentType={post.contentType} />}
            <hr className="postDivider" />

            <CommentSection postId={post.id} />
        </div>
    );
};

export default Post;
