import React, { useState } from "react";
import "./ProfileEditModal.css";

const ProfileEditModal = ({
                              firstName,
                              lastName,
                              initialDescription,
                              initialVisibility,
                              toggleModal,
                          }) => {
    const [editedFirstName, setEditedFirstName] = useState(firstName);
    const [editedLastName, setEditedLastName] = useState(lastName);
    const [editedDescription, setEditedDescription] = useState(initialDescription);
    const [profileVisibility, setProfileVisibility] = useState(initialVisibility);
    const [profilePicture, setProfilePicture] = useState("");
    const [errorMessage, setErrorMessage] = useState("");

    const changeVisibility = async () => {
        try {
            const response = await fetch("https://localhost:44354/changePrivacy", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                },
                body: JSON.stringify({ visibility: profileVisibility }),
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || "Failed to update visibility.");
            }
        } catch (error) {
            setErrorMessage(error.message);
        }
    };

    const changeProfileDetails = async () => {
        try {
            const response = await fetch("https://localhost:44354/user/edit", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                },
                body: JSON.stringify({
                    firstName: editedFirstName,
                    lastName: editedLastName,
                    description: editedDescription,
                    profilePicture: profilePicture,
                }),
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || "Failed to update profile details.");
            }
        } catch (error) {
            setErrorMessage(error.message);
        }
    };

    const handleSave = async () => {
        if (!editedFirstName || !editedLastName) {
            setErrorMessage("First and last name cannot be empty.");
            return;
        }

        setErrorMessage(""); 
        try {
            if (profileVisibility !== initialVisibility) {
                await changeVisibility();
            }
            await changeProfileDetails();
            toggleModal(); 
            window.location.reload();
        } catch (error) {
            console.error("Save failed:", error.message);
        }
    };

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <span className="editHeader">Edit Profile</span>

                <div className="modal-inputs">
                    <div className="input-field">
                        <label className="profileLabel">Profile Picture URL</label>
                        <input
                            type="text"
                            value={profilePicture}
                            onChange={(e) => setProfilePicture(e.target.value)}
                            placeholder="Enter URL for profile picture"
                        />
                    </div>
                    <div className="input-field">
                        <label className="profileLabel">First Name</label>
                        <input
                            type="text"
                            value={editedFirstName}
                            onChange={(e) => setEditedFirstName(e.target.value)}
                            placeholder="Enter your first name"
                        />
                    </div>
                    <div className="input-field">
                        <label className="profileLabel">Last Name</label>
                        <input
                            type="text"
                            value={editedLastName}
                            onChange={(e) => setEditedLastName(e.target.value)}
                            placeholder="Enter your last name"
                        />
                    </div>
                    <div className="input-field">
                        <label className="profileLabel">Description</label>
                        <textarea
                            value={editedDescription}
                            onChange={(e) => setEditedDescription(e.target.value)}
                            placeholder="Write something about yourself..."
                            className="description-textarea"
                        />
                    </div>
                    <div className="input-field">
                        <label className="profileLabel">Profile Visibility</label>
                        <div className="visibilityContainer">
                            <span
                                className={
                                    !profileVisibility
                                        ? "visibilityLabel active"
                                        : "visibilityLabel"
                                }
                            >
                                Public
                            </span>
                            <label className="toggle-switch">
                                <input
                                    type="checkbox"
                                    checked={profileVisibility}
                                    onChange={() => setProfileVisibility(!profileVisibility)}
                                />
                                <span className="slider"></span>
                            </label>
                            <span
                                className={
                                    profileVisibility
                                        ? "visibilityLabel active"
                                        : "visibilityLabel"
                                }
                            >
                                Private
                            </span>
                        </div>
                    </div>
                </div>

                {errorMessage && <div className="error-message">{errorMessage}</div>}

                <div className="modal-actions">
                    <button onClick={handleSave}>Save Changes</button>
                    <button onClick={toggleModal}>Cancel</button>
                </div>
            </div>
        </div>
    );
};

export default ProfileEditModal;
