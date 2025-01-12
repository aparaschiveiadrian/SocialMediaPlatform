import React, { useState } from "react";
import './UserPost.css';
import SVGVideo from "@/Components/SVGs/SVGvideo.jsx";
import SVGText from "@/Components/SVGs/SVGtext.jsx";
import SVGImage from "@/Components/SVGs/SVGimage.jsx";
import axios from "axios";

const UserPost = () => {
    const [inputValue, setInputValue] = useState("");
    const [selectedOption, setSelectedOption] = useState('text');
    const [media, setMedia] = useState(null);
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(false);

    const postOptions = [
        { id: 'text', Component: SVGText, label: 'Add Text' },
        { id: 'image', Component: SVGImage, label: 'Add Image' },
        { id: 'video', Component: SVGVideo, label: 'Add Video' },
    ];

    const handleMediaChange = (e) => {
        setMedia(e.target.files[0]);
    };

    const changePostType = (id) => {
        setSelectedOption(id);
        setMedia(null); // Reset media when switching options
        setError(null); // Clear any existing errors
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);
        setSuccess(false);

        if (selectedOption !== 'text' && !media) {
            setError("Please upload a photo or video.");
            return;
        }

        try {
            let mediaUrl = null;

            // Upload media if an image or video is selected
            if (media) {
                const formData = new FormData();
                formData.append("file", media);
                formData.append("upload_preset", "socialmedia_preset");

                const isVideo = media.type.startsWith("video/");
                const cloudinaryUploadUrl = isVideo
                    ? `https://api.cloudinary.com/v1_1/do76h3uvd/video/upload`
                    : `https://api.cloudinary.com/v1_1/do76h3uvd/image/upload`;

                const cloudinaryResponse = await axios.post(cloudinaryUploadUrl, formData);
                mediaUrl = cloudinaryResponse.data.secure_url;
            }

            // Construct the post payload
            const postData = {
                Content: inputValue,
                MediaUrl: mediaUrl,
                ContentType: selectedOption,
                CreatedAt: new Date().toISOString(),
            };

            // Send the post data to the backend
            const token = localStorage.getItem("token");
            await axios.post(
                "https://localhost:44354/post/create",
                postData,
                {
                    headers: { Authorization: `Bearer ${token}` },
                }
            );

            setSuccess(true);
            setInputValue("");
            setMedia(null);
            setSelectedOption('text');
        } catch (err) {
            console.error(err);
            setError("Failed to create post. Please try again.");
        }
    };

    return (
        <div className="containerUserPost">
            <h2>Create Post</h2>
            {error && <p className="errorMessage">{error}</p>}
            {success && <p className="validMessage">Post created successfully!</p>}
            <div className="createPost">
                <form className="postForm" onSubmit={handleSubmit}>
                    {/* Input for text */}
                    {selectedOption === 'text' && (
                        <textarea
                            name="userInput"
                            placeholder="Feel free to share with us!"
                            aria-label="Create Post"
                            className="userInput"
                            value={inputValue}
                            onChange={(e) => setInputValue(e.target.value)}
                        ></textarea>
                    )}

                    {/* Input for media (image or video) */}
                    {(selectedOption === 'image' || selectedOption === 'video') && (
                        <div>
                            <label>Upload Media (Photo or Video):</label>
                            <input type="file" accept="image/*,video/*" onChange={handleMediaChange} />
                        </div>
                    )}

                    <hr className="postDivider" />

                    {/* Post Options */}
                    <div className="postOptionsContainer">
                        {postOptions.map(({ id, Component, label }) => (
                            <button
                                key={id}
                                className={`btnDisable svg ${selectedOption === id ? 'selected' : ''}`}
                                onClick={() => changePostType(id)}
                                type="button"
                                aria-label={label}
                            >
                                <Component hoverColor={"#82A6ECFF"} />
                                <span style={{ color: "white", fontWeight: "400" }}>{label}</span>
                            </button>
                        ))}
                        <button className="btn" type="submit">
                            Create Post
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default UserPost;
