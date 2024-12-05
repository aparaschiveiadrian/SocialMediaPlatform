import './Navbar.css';

const Navbar = () => {
    const stickyNav = () => {
        let headerHeight = document.querySelector('header').offsetHeight;
        let navbar = document.querySelector('nav');
        let scrollValue = window.scrollY;

        if (scrollValue > headerHeight) {
            navbar.classList.add('sticky');
        } else {
            navbar.classList.remove('sticky');
        }
    };

    window.addEventListener('scroll', stickyNav);

    const handleSearch = (e) => {
        e.preventDefault();
        const searchTerm = e.target.value; 
        console.log(`Searching for: ${searchTerm}`); 
    };

    const handleKeyPress = (e) => {
        if (e.key === 'Enter') {
            e.preventDefault(); 
            const searchTerm = e.target.value; 
            console.log(`Searching for: ${searchTerm}`); 
        }
    };

    return (
        <header>
            <nav>
                <div className="mainNav">
                    <a href="/" className="logo">Militan Media</a>
                    <form className="searchBar" onSubmit={handleSearch}>
                        <input
                            type="text"
                            name="searchInput"
                            placeholder="Search users..."
                            className="searchInput"
                            onKeyDown={handleKeyPress}
                        />
                    </form>
                    <ul className="navbar">
                        <li className="menuItem">
                            <a href="/" className="menuLink">Feed</a>
                        </li>
                        <li className="menuItem">
                            <a href="/home" className="menuLink">Groups</a>
                        </li>
                        <li className="menuItem">
                            {
                                localStorage.getItem('userId') ?
                                    (
                                        <a
                                            href={`/profile/${localStorage.getItem('username')}`}
                                            className="menuLink"
                                        >
                                            Your Account
                                        </a>
                                    )
                                    : (
                                        <a href="/register" className="menuLink">Register</a>
                                    )
                            }
                        </li>

                    </ul>
                </div>
            </nav>
        </header>
    );
};

export default Navbar;
