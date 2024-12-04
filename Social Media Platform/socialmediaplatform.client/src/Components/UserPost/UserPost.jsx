import './UserPost.css';
import React, { useState } from "react";
import SVGVideo from "@/Components/SVGs/SVGvideo.jsx";
import SVGText from "@/Components/SVGs/SVGtext.jsx";
import SVGImage from "@/Components/SVGs/SVGimage.jsx";

const UserPost = () => {
    const [inputValue, setInputValue] = useState(""); // To manage the input value

    const postOptions = [
        { id: 'text', Component: SVGText },
        { id: 'image', Component: SVGImage },
        { id: 'video', Component: SVGVideo },
    ];

    const handleKeyDown = (event) => {
        if (event.key === "Enter" && !event.shiftKey) {
            event.preventDefault(); 
            console.log(inputValue);
            setInputValue(""); 
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
                    {postOptions.map(({ id, Component }) => (
                        <a key={id} className="svg">
                            <Component hoverColor={"#82A6ECFF"} />
                        </a>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default UserPost;
