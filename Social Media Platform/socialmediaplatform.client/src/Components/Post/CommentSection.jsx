import React, {useEffect, useState} from 'react';

const CommentSection = ({ postId }) => {
    
    const [comment, setComment] = useState("");
    const handleInputChange = (event) => {
        if (event.key === "Enter" && !event.shiftKey) {
            event.preventDefault();
            console.log(`Post ${postId} comment:`, comment);
            setComment("");
        }
    };
    
    const submitComment = async(event) => {
        event.preventDefault();
        try{
            const response = await fetch("https://localhost:44354/comment/create",
                    {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json",
                            "Authorization": `Bearer ${localStorage.getItem("token")}`,
                        },
                        body: JSON.stringify({
                            content: comment,
                            postId: postId,
                        })
                    });
            if(response.ok)
            {
                setComment("");
            }
            else{
                window.alert("You have to be logged in to be able to leave a comment");
            }
        }
        catch(error) {
            window.alert("An error occurred while deleting the comment");
        }
        
    }
   /* const submitComment = (event) => {
        event.preventDefault();
        console.log(`Post ${postId} comment submitted:`, comment);
        setComment(""); 
    };*/

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
