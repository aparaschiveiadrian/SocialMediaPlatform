﻿import './FollowingButton.css';

const FollowingButton = () => {
    return (
        <button className="followingBtn">
             <span className="IconContainer">
                <svg
                    width="24px"
                    height="24px"
                    viewBox="0 0 48 48"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                >
                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                    <g id="SVGRepo_iconCarrier">
                        <path
                            fillRule="evenodd"
                            clipRule="evenodd"
                            d="M24 42C33.9411 42 42 33.9411 42 24C42 14.0589 33.9411 6 24 6C14.0589 6 6 14.0589 6 24C6 33.9411 14.0589 42 24 42ZM24 44C35.0457 44 44 35.0457 44 24C44 12.9543 35.0457 4 24 4C12.9543 4 4 12.9543 4 24C4 35.0457 12.9543 44 24 44Z"
                            fill="#a3edff"
                        ></path>
                        <path
                            fillRule="evenodd"
                            clipRule="evenodd"
                            d="M34.6709 16.2585C35.0805 16.629 35.1121 17.2614 34.7415 17.6709L21.3858 32.4325L13.3095 24.7234C12.91 24.342 12.8953 23.709 13.2766 23.3095C13.658 22.91 14.291 22.8953 14.6905 23.2767L21.2809 29.5675L33.2585 16.3291C33.629 15.9196 34.2614 15.8879 34.6709 16.2585Z"
                            fill="#a3edff"
                        ></path>
                    </g>
                </svg>
            </span>
            <p className="text">Following</p>
        </button>
    );
};

export default FollowingButton;
