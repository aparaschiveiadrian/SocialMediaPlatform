import './Navbar.css';
import Cookies from "js-cookie";
import { Link } from 'react-router-dom';
import { useEffect } from "react";

const Navbar = () => {
    useEffect(() => {
        const stickyNav = () => {
            const headerHeight = document.querySelector("header")?.offsetHeight || 0;
            const navbar = document.querySelector("nav");
            if (window.scrollY > headerHeight) {
                navbar?.classList.add("sticky");
            } else {
                navbar?.classList.remove("sticky");
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
            })

        window.location.reload();
    };

    return (
        <header>
            <nav>
                <div className="mainNav">
                    <Link to="/" className="logo">Militan Media</Link>

                    <form className="searchBar">
                        <input
                            type="text"
                            name="searchInput"
                            placeholder="Search users..."
                            className="searchInput"
                            onKeyDown={(e) => {
                                if (e.key === 'Enter') {
                                    e.preventDefault();
                                    console.log(`Searching for: ${e.target.value}`);
                                }
                            }}
                        />
                    </form>

                    <ul className="navbar">
                        <li className="menuItem">
                            <Link to="/" className="menuLink">Home</Link>
                        </li>
                        <li className="menuItem">
                            <Link to="/home" className="menuLink">Groups</Link>
                        </li>
                        {authToken ? (
                            <>
                                <li className="menuItem">
                                    <Link to={`/profile/${username}`} className="menuLink">{username}</Link>
                                </li>
                                <li className="menuItem">
                                    <a className="menuLink" onClick={logOut}>Logout
                                    </a>
                                </li>
                            </>
                        ) : (
                            <li className="menuItem">
                                <Link to="/register" className="menuLink">Sign in</Link>
                            </li>
                        )}
                    </ul>
                </div>
            </nav>
        </header>
    );
};

export default Navbar;
