import React from "react";
import "./Footer.css";

const Footer = () => {
    return (
        <footer className="footer">
            <div className="footer-container">
                <div className="footer-logo">
                    <h2>Social Media Platform</h2>
                </div>
                
                
            </div>
            <div className="footer-bottom">
                <p>&copy; {new Date().getFullYear()} Social Media Platform. All rights reserved.</p>
            </div>
        </footer>
    );
};

export default Footer;
