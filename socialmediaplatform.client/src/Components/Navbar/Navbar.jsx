import './Navbar.css';
import Cookies from "js-cookie";
import { Link } from 'react-router-dom';
import { useEffect } from "react";
import SearchBar from "@/Components/SearchBar/SearchBar.jsx";
import SVGnotifications from "@/Components/SVGs/SVGnotifications.jsx";

const Navbar = () => {
    useEffect(() => {
        const stickyNav = () => {
            const header = document.querySelector("header");
            const navbar = document.querySelector("nav");

            if (!header || !navbar) return;

            const headerHeight = header.offsetHeight;
            if (window.scrollY > headerHeight) {
                navbar.classList.add("sticky");
            } else {
                navbar.classList.remove("sticky");
            }
        };

        window.addEventListener("scroll", stickyNav);

        return () => {
            window.removeEventListener("scroll", stickyNav);
        };
    }, []);

    const authToken = localStorage.getItem("token");
    const username = localStorage.getItem("username");

    const logOut = () => {
        Object.keys(localStorage).forEach((key) => {
            localStorage.removeItem(key);
        });
        Cookies.remove("session"); 
        window.location.href = "/"; 
    };

    return (
        <header>
            <nav>
                <div className="mainNav">
                    <Link to="/" className="logo">Militan</Link>
                    <SearchBar />

                    <ul className="navbar">
                        <li className="menuItem">
                            <Link to="/" className="menuLink">Home</Link>
                        </li>
                        <li className="menuItem">
                            <Link to="/conversations" className="menuLink">Conversations</Link>
                        </li>
                        {authToken ? (
                            <>
                                <li className="menuItem">
                                    <Link to={`/profile/${username}`} className="menuLink">{username}</Link>
                                </li>
                                <li className="menuItem">
                                    <Link to={`/notifications`} className="menuLink">
                                        <SVGnotifications />
                                    </Link>
                                </li>
                                <li className="menuItem">
                                    <a className="menuLink logout" onClick={logOut}>Logout</a>
                                </li>
                            </>
                        ) : (
                            <li className="menuItem">
                                <Link to="/register" className="menuLink signin">Sign in</Link>
                            </li>
                        )}
                    </ul>
                </div>
            </nav>
        </header>
    );
};

export default Navbar;
