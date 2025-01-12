import React, { useState } from "react";
import "./Profile.css";
import SVGprofile from "@/Components/SVGs/SVGprofile.jsx";
import ProfileEditModal from "@/Components/ProfileEditModal/ProfileEditModal.jsx";
import FollowButton from "@/Components/Buttons/FollowButton/FollowButton.jsx";
import PendingButton from "@/Components/Buttons/PendingButton/PendingButton.jsx";
import FollowingButton from "@/Components/Buttons/FollowingButton/FollowingButton.jsx";
import UnfollowButton from "@/Components/Buttons/UnfollowButton/UnfollowButton.jsx";
const ProfileCard = ({
                         firstName,
                         lastName,
                         username,
                         initialDescription,
                         initialVisibility,
                         followStatus,
                         setFollowStatus,
                        profilePicture
                     }) => {
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
                        <img src={profilePicture || "/../../public/default_profile_picture.png"} alt="Profile"/>
                    </div>
                    {renderProfileContent()}
                </div>
                <div className="profile-footer">
                    {username === localStorage.getItem("username") ? (
                        <button className="profile-button" onClick={toggleModal}>
                            Edit Profile
                        </button>
                    ) : (
                            followStatus === "Following" ? (
                                <div style={{ display: "flex", gap: "1rem" }}>
                                    <FollowingButton/>
                                    <UnfollowButton
                                    text={'Unfollow'}
                                    username={username}
                                    />
                                </div>
                            ) : followStatus === "Pending" ? (
                                <div style={{ display: "flex", gap: "1rem" }}>
                                    <PendingButton/>
                                    <UnfollowButton
                                        text={'Cancel'}
                                        username={username}
                                    />
                                </div>
                            ) :  (
                                <FollowButton 
                                username={username}
                                />
                            )
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
