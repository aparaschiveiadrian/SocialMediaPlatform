import './UserPost.css';
import React, { useState } from "react";
import SVGVideo from "@/Components/SVGs/SVGvideo.jsx";
import SVGText from "@/Components/SVGs/SVGtext.jsx";
import SVGImage from "@/Components/SVGs/SVGimage.jsx";

const UserPost = () => {
    const [inputValue, setInputValue] = useState("");
    const [selectedOption, setSelectedOption] = useState('text');

    const postOptions = [
        { id: 'text', Component: SVGText, label: 'Add Text' },
        { id: 'image', Component: SVGImage, label: 'Add Image' },
        { id: 'video', Component: SVGVideo, label: 'Add Video' },
    ];


    const changePostType = (id) => {
        setSelectedOption(id);
    };


    const handleKeyDown = (event) => {
        if (event.key === "Enter" && !event.shiftKey) {
            event.preventDefault();
            console.log(inputValue); 
            setInputValue(""); 
        }
    };

    // Create post request
    const createPost = () => {
        const postData = {
            content: inputValue,
            contentType: selectedOption,  
            createdAt: new Date().toISOString(),
            mediaUrl: "string2"
        };

        fetch('https://localhost:44354/post/create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                //autorizare dupa token
                'Authorization': 'Bearer ' + localStorage.getItem('token'),
            },
            body: JSON.stringify(postData),
        })
            .then((response) => response.json())
            .then((data) => {
                console.log('Post created successfully:', data);
                setInputValue("");
                setSelectedOption('text');
                window.location.reload();
            })
            .catch(() => {
                window.alert("An error occurred. You have to be logged in.");
            });
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
                            className={`btnDisable svg ${selectedOption === id ? 'selected' : ''}`} 
                            onClick={() => changePostType(id)}
                            aria-label={label}
                        >
                            <Component hoverColor={"#82A6ECFF"}/>
                            <span style={{ color:"white", fontWeight: "400" }}>
                                {label}
                            </span>
                        </button>
                    ))}
                    <button className={"btn"} onClick={createPost}>
                        Create Post
                    </button>
                </div>
            </div>
        </div>
    );
};

export default UserPost;
