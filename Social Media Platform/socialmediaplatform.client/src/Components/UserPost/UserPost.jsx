import './UserPost.css';
import React, { useState } from "react";
import SVGVideo from "@/Components/SVGs/SVGvideo.jsx";
import SVGText from "@/Components/SVGs/SVGtext.jsx";
import SVGImage from "@/Components/SVGs/SVGimage.jsx";

const UserPost = () => {
    const [inputValue, setInputValue] = useState(""); // To manage the input value

    const postOptions = [
        { id: 'text', Component: SVGText, label: 'Add Text' },
        { id: 'image', Component: SVGImage, label: 'Add Image' },
        { id: 'video', Component: SVGVideo, label: 'Add Video' },
    ];

    const displayId = (id) => {
        console.log(id);
    };

    const handleKeyDown = (event) => {
        if (event.key === "Enter" && !event.shiftKey) {
            event.preventDefault();
            console.log(inputValue); // Send or handle input value
            setInputValue(""); // Clear the input
        }
    };

    return (
        <div className="containerUserPost">
            <span className="createPostInfo">
                Hello, want to share some thoughts?
            </span>
            <div className="createPost">
                <form className="postForm">
                    <textarea
                        name="userInput"
                        placeholder="Feel free to share with us!"
                        aria-label="Create Post"
                        className="userInput"
                        value={inputValue}
                        onChange={(e) => setInputValue(e.target.value)}
                        onKeyDown={handleKeyDown}
                    ></textarea>
                </form>
                <hr className="postDivider" />
                <div className="postOptionsContainer">
                    {postOptions.map(({ id, Component, label }) => (
                        <button
                            key={id}
                            className={"btnDisable svg"}
                            onClick={() => displayId(id)}
                            aria-label={label}
                        >
                            <Component hoverColor={"#82A6ECFF"}/>
                            <span style={{ color:"white", fontWeight: "400" }}>
                                {label}
                            </span>
                        </button>
                    ))}
                    <button className={"btn"}>
                        Create Post
                    </button>
                </div>
            </div>
        </div>
    );
};

export default UserPost;
