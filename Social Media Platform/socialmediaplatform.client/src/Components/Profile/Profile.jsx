import React, { useState } from "react";
import "./Profile.css";
import SVGprofile from "@/Components/SVGs/SVGprofile.jsx";

const ProfileCard = ({ firstName, lastName, username, initialDescription }) => {
    const [isEditing, setIsEditing] = useState(false);
    const [editedFirstName, setEditedFirstName] = useState(firstName);
    const [editedLastName, setEditedLastName] = useState(lastName);
    const [editedDescription, setEditedDescription] = useState(initialDescription);
    
    const handleSave = () => {
        console.log("Updated Profile Details:", {
            firstName: editedFirstName,
            lastName: editedLastName,
            description: editedDescription,
        });

        setIsEditing(false);
    };
    
    const renderProfileContent = () => {
        if (isEditing) {
            return (
                <>
                    <input
                        type="text"
                        className="profile-input"
                        value={editedFirstName}
                        onChange={(e) => setEditedFirstName(e.target.value)}
                        placeholder="First Name"
                    />
                    <input
                        type="text"
                        className="profile-input"
                        value={editedLastName}
                        onChange={(e) => setEditedLastName(e.target.value)}
                        placeholder="Last Name"
                    />
                    <textarea
                        className="profile-textarea"
                        value={editedDescription}
                        rows={10}
                        onChange={(e) => setEditedDescription(e.target.value)}
                        placeholder="Description"
                    />
                </>
            );
        } else {
            return (
                <>
                    <h2 className="profile-name">
                        {firstName} {lastName}
                    </h2>
                    <p className="profile-username">@{username}</p>
                    <p className="profile-description">{initialDescription}</p>
                </>
            );
        }
    };

    return (
        <div className="profile-card-container">
            <div className="profile-card">
                <div className="profile-header">
                    <div className="profile-picture">
                        <SVGprofile />
                    </div>
                    {renderProfileContent()}
                </div>
                <div className="profile-footer">
                    {isEditing ? (
                        <button className="save-button" onClick={handleSave}>
                            Save Changes
                        </button>
                    ) : (
                        <button className="edit-button" onClick={() => setIsEditing(true)}>
                            Edit Profile
                        </button>
                    )}
                </div>
            </div>
        </div>
    );
};

export default ProfileCard;
