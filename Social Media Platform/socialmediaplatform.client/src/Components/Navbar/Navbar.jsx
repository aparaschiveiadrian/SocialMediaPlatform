import './Navbar.css'
const Navbar = () => {
    const stickyNav = () => {
        let headerHeight = document.querySelector('header').offsetHeight;
        let navbar = document.querySelector('nav');
        let scrollValue = window.scrollY;

        if(scrollValue > headerHeight) {
            navbar.classList.add('sticky');
        }
        else {
            navbar.classList.remove('sticky');
        }
    };
    window.addEventListener('scroll', stickyNav);
    return (
        <header>
            <nav>
                <div className = "mainNav">
                    <a href="/home" className={"logo"}>FridgeQuestApp</a>
                    <ul className={"navbar"}>
                        <li className={"menuItem"}>
                            <a href="/home" className={"menuLink"}>Home</a>
                        </li>
                        <li className={"menuItem"}>
                            <a href="/home" className={"menuLink"}>About</a>
                        </li>
                        <li className={"menuItem"}>
                            <a href="/home" className={"menuLink"}>Recipes</a>
                        </li>
                    </ul>
                </div>
            </nav>
        </header>
    );
};

export default Navbar;