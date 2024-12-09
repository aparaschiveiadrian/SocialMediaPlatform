import React, { useState } from 'react';

const CommentSection = ({ postId }) => {
    const [comment, setComment] = useState("");

    const handleInputChange = (event) => {
        if (event.key === "Enter" && !event.shiftKey) {
            event.preventDefault();
            console.log(`Post ${postId} comment:`, comment);
            setComment("");
        }
    };

    const submitComment = (event) => {
        event.preventDefault();
        console.log(`Post ${postId} comment submitted:`, comment);
        setComment(""); 
    };

    return (
        <div className="commentAddContainer">
            <textarea
                id={`commentInput-${postId}`}
                className="postInput"
                placeholder="Add a comment..."
                value={comment}
                onChange={(e) => setComment(e.target.value)}
                onKeyDown={handleInputChange}
            ></textarea>
            <button
                className="submitCommentButton"
                onClick={submitComment}
            >
                >
            </button>
        </div>
    );
};

export default CommentSection;
