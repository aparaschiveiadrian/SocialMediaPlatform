import React, { useState } from 'react';

const SVGText = ({ hoverColor = "#00ff00", defaultColor = "#ffffff" }) => {
    const [currentColor, setCurrentColor] = useState(defaultColor);

    return (
        <svg
            width="32px"
            height="32px"
            viewBox="0 0 24 24"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
            onMouseEnter={() => setCurrentColor(hoverColor)} // Change color on hover
            onMouseLeave={() => setCurrentColor(defaultColor)} // Revert color on mouse leave
        >
            <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
            <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
            <g id="SVGRepo_iconCarrier">
                <path
                    d="M8 4V20M17 12V20M6 20H10M15 20H19M13 7V4H3V7M21 14V12H13V14"
                    stroke={currentColor} // Use dynamic color
                    strokeWidth="2"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                ></path>
            </g>
        </svg>
    );
};

export default SVGText;
