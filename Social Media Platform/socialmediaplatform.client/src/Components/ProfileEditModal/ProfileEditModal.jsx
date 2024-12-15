import React, { useState } from "react";
import "./ProfileEditModal.css";

const ProfileEditModal = ({ firstName, lastName, initialDescription, initialVisibility, toggleModal }) => {
    const [editedFirstName, setEditedFirstName] = useState(firstName);
    const [editedLastName, setEditedLastName] = useState(lastName);
    const [editedDescription, setEditedDescription] = useState(initialDescription);
    const [profileVisibility, setProfileVisibility] = useState(initialVisibility);

    const handleSave = () => {
        console.log("Updated Profile Details:", {
            firstName: editedFirstName,
            lastName: editedLastName,
            description: editedDescription,
            visibility: profileVisibility,
        });

        toggleModal();
    };

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <span className={"editHeader"}>Edit Profile</span>

                <div className="modal-inputs">
                    <div className="input-field">
                        <label className={"profileLabel"}>Profile Picture URL</label>
                        <input type="text" placeholder="Enter URL for profile picture" />
                    </div>
                    <div className="input-field">
                        <label className={"profileLabel"}>First Name</label>
                        <input
                            type="text"
                            value={editedFirstName}
                            onChange={(e) => setEditedFirstName(e.target.value)}
                        />
                    </div>
                    <div className="input-field">
                        <label className={"profileLabel"}>Last Name</label>
                        <input
                            type="text"
                            value={editedLastName}
                            onChange={(e) => setEditedLastName(e.target.value)}
                        />
                    </div>
                    <div className="input-field">
                        <label className={"profileLabel"}>Description</label>
                        <textarea
                            value={editedDescription}
                            onChange={(e) => setEditedDescription(e.target.value)}
                            placeholder="Write something about yourself..."
                            className="description-textarea"
                        />
                    </div>
                    <div className="input-field">
                        <label className={"profileLabel"}>Profile Visibility</label>
                        <div className={"visibilityContainer"}>
                            <label className="toggle-switch">
                                <input
                                    type="checkbox"
                                    checked={profileVisibility}
                                    onChange={() => setProfileVisibility(!profileVisibility)}
                                />
                                <span className="slider"></span>
                            </label>
                            <span>{profileVisibility ? "Private" : "Public"}</span>
                        </div>
                    </div>
                </div>

                <div className="modal-actions">
                    <button onClick={handleSave}>Save Changes</button>
                    <button onClick={toggleModal}>Cancel</button>
                </div>
            </div>
        </div>
    );
};

export default ProfileEditModal;
