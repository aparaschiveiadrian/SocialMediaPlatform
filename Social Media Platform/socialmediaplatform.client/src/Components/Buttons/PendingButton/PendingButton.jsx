import './PendingButton.css';

const PendingButton = () => {
    return (
        <button className="pendingBtn" >
            <span className="IconContainer">
                <svg
                    width="32px"
                    height="32px"
                    viewBox="0 0 1024 1024"
                    className="icon"
                    version="1.1"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="#000000"
                    stroke="#000000"
                >
                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                    <g id="SVGRepo_iconCarrier">
                        <path
                            d="M511.9 183c-181.8 0-329.1 147.4-329.1 329.1s147.4 329.1 329.1 329.1c181.8 0 329.1-147.4 329.1-329.1S693.6 183 511.9 183z m0 585.2c-141.2 0-256-114.8-256-256s114.8-256 256-256 256 114.8 256 256-114.9 256-256 256z"
                            fill="#a5c5c5"
                        ></path>
                        <path
                            d="M548.6 365.7h-73.2v161.4l120.5 120.5 51.7-51.7-99-99z"
                            fill="#a5c5c5"
                        ></path>
                    </g>
                </svg>
            </span>
            <p className="text-pending">Pending</p>
        </button>
    );
};

export default PendingButton;
