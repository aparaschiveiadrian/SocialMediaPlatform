import { useEffect, useState } from "react";
import SVGdownArrow from "@/Components/SVGs/SVGdownArrow.jsx";
import SVGthreeDots from "@/Components/SVGs/SVGthreeDots.jsx";

const UsersComments = ({ postId, reload }) => {
    const [commentList, setCommentList] = useState([]);
    const [loading, setLoading] = useState(true);
    const [errorMessage, setErrorMessage] = useState(null);
    const [visibleComments, setVisibleComments] = useState(2);

    const [isModalOpen, setIsModalOpen] = useState(false);
    const [currentComment, setCurrentComment] = useState({ id: null, content: "" });
    const [commentIsModified, setCommentIsModified] = useState(false);
    const [openMenuId, setOpenMenuId] = useState(null); // Tracks which comment's menu is open

    const fetchComments = async () => {
        setLoading(true);
        setErrorMessage(null);
        try {
            const response = await fetch(`https://localhost:44354/comment/get/${postId}`);
            if (response.ok) {
                const data = await response.json();
                setCommentList(data);
            } else {
                setErrorMessage(`Failed to fetch comments: ${response.status} ${response.statusText}`);
            }
        } catch (error) {
            //console.log("List is empty");
        } finally {
            setLoading(false);
        }
    };

    const handleEdit = (commentId) => {
        const commentToEdit = commentList.find((comment) => comment.id === commentId);
        if (commentToEdit) {
            setCurrentComment({...commentToEdit, id: commentId, content: commentToEdit.content });
            setIsModalOpen(true);
        }
    };

    const handleDelete = async (commentId) => {
        try {
            const response = await fetch(`https://localhost:44354/comment/delete/${commentId}`, {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${localStorage.getItem("token")}`,
                },
            });
            if (response.ok) {
                setCommentList((prev) => prev.filter((comment) => comment.id !== commentId));
            } else {
                window.alert("You are not authorized to delete this comment");
            }
        } catch (error) {
            window.alert(`An error occurred while deleting the comment: ${error}`);
        }
    };

    const handleSaveEdit = async () => {
        try {
            const response = await fetch(`https://localhost:44354/comment/edit/${currentComment.id}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${localStorage.getItem("token")}`,
                },
                body: JSON.stringify({ content: currentComment.content }),
            });

            if (response.ok) {
                setIsModalOpen(false);
                setCurrentComment({ id: null, content: "" });
                setCommentIsModified(!commentIsModified);
            } else {
                window.alert("You are not authorized to edit this comment.");
            }
        } catch (error) {
            window.alert("An error occurred while updating the comment.");
        }
    };

    const handleCancelEdit = () => {
        setIsModalOpen(false);
        setCurrentComment({...currentComment, id: null, content: "" });
    };

    useEffect(() => {
        fetchComments();
    }, [commentIsModified, reload]);

    const handleViewMore = () => {
        setVisibleComments((prev) => prev + 2);
    };

    const toggleMenu = (commentId) => {
        setOpenMenuId((prev) => (prev === commentId ? null : commentId));
    };

    return (
        <div className={`commentsListContainer`}>
            {loading ? (
                <p>Loading...</p>
            ) : errorMessage ? (
                <p className="error">{errorMessage}</p>
            ) : commentList.length > 0 ? (
                <>
                    {commentList.slice(0, visibleComments).map((comment) => (
                        <div key={comment.id} className="commentContainer">
                            <div className="commentInfo">
                                <a href={`/profile/${comment.username}`} className="userTag">
                                    @{comment.username}
                                </a>
                                <small className="postDate">
                                    {new Date(comment.createdAt).toLocaleString()}
                                </small>
                                {comment.isEdited && <small className="postDate">(Edited)</small>}
                                {(comment.username == localStorage.getItem("username") || localStorage.getItem("isAdmin")=="true") &&
                                    (<button
                                        className="commentOptions threeDotsIcon"
                                        onClick={() => toggleMenu(comment.id)}
                                    >
                                        <SVGthreeDots/>
                                    </button>)}
                                {openMenuId === comment.id && (
                                    <div className="dropdownMenu">
                                        <button
                                            onClick={() => handleEdit(comment.id)}
                                            className="editButton"
                                        >
                                            Edit
                                        </button>
                                        <button
                                            onClick={() => handleDelete(comment.id)}
                                            className="deleteButton"
                                        >
                                            Delete
                                        </button>
                                    </div>
                                )}
                            </div>
                            <div className="contentContainer">
                                <p>{comment.content}</p>
                            </div>
                        </div>
                    ))}
                    {visibleComments < commentList.length && (
                        <div className="viewMoreContainer">
                            <button onClick={handleViewMore} className="viewMoreButton">
                                View more <SVGdownArrow />
                            </button>
                        </div>
                    )}
                </>
            ) : (
                <small style={{ color: "gray" }}>Be the first to leave a comment.</small>
            )}

            {/* Modal */}
            {isModalOpen && (
                <div className="modalOverlay">
                    <div className="modalContent">
                        <h3>Edit Comment</h3>
                        <textarea
                            value={currentComment.content}
                            onChange={(e) =>
                                setCurrentComment({ ...currentComment, content: e.target.value })
                            }
                        />
                        <div className="modalActions">
                            <button className="saveButton" onClick={handleSaveEdit}>
                                Save
                            </button>
                            <button className="cancelButton" onClick={handleCancelEdit}>
                                Cancel
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default UsersComments;