import React, { useState } from "react";
import "./Profile.css";
import SVGprofile from "@/Components/SVGs/SVGprofile.jsx";
import ProfileEditModal from "@/Components/ProfileEditModal/ProfileEditModal.jsx";

const ProfileCard = ({ firstName, lastName, username, initialDescription, initialVisibility }) => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    
    const toggleModal = () => setIsModalOpen(!isModalOpen);

    const renderProfileContent = () => (
        <>
            <h2 className="profile-name">
                {firstName} {lastName}
            </h2>
            <p className="profile-username">@{username}</p>
            <p className="profile-description">{initialDescription}</p>
        </>
    );

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
                    {
                        username === localStorage.getItem("username") ? (
                            <button className="profile-button" onClick={toggleModal}>
                                Edit Profile
                            </button>
                        ) : (
                            <button className="profile-button" onClick={() => {}}>
                                Follow
                            </button>
                        )
                    }
                </div>
            </div>

            {isModalOpen && (
                <ProfileEditModal
                    firstName={firstName}
                    lastName={lastName}
                    initialVisibility={initialVisibility}
                    toggleModal={toggleModal}
                />
            )}
        </div>
    );
};

export default ProfileCard;
