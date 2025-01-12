import { useState, useEffect } from "react";
import "./SearchBar.css";

const SearchBar = () => {
    const [searchTerm, setSearchTerm] = useState("");
    const [users, setUsers] = useState([]);
    const [filteredUsers, setFilteredUsers] = useState([]);

    const getAllUsers = async () => {
        try {
            const response = await fetch("https://localhost:44354/user/allUsers", {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
            });

            if (response.ok) {
                const users = await response.json();
                setUsers(users);
                //console.log(users);
            } else {
                console.error(`HTTP error! Status: ${response.status}`);
            }
        } catch (error) {
            console.error("Fetch error:", error);
        }
    };

    useEffect(() => {
        getAllUsers();
    }, []);

    const onSearch = (e) => {
        const searchValue = e.target.value;
        setSearchTerm(searchValue);

        const newFilteredUsers = users.filter(user =>
            user.username.toLowerCase().includes(searchValue.toLowerCase())
        );
        setFilteredUsers(newFilteredUsers);

        if (searchValue.length === 0) {
            setFilteredUsers([]);
        }
        //console.log("filtrat" + filteredUsers);
    };

    const clearInput = () => {
        setSearchTerm("");
        setFilteredUsers([]);
    };

    return (
        <div className="searchContainer">
            <form className="searchBar" onSubmit={(e) => e.preventDefault()}>
                <input
                    type="text"
                    name="searchInput"
                    value={searchTerm}
                    placeholder="Search users by username..."
                    className="searchInput"
                    onChange={onSearch}
                    aria-label="Search for users"
                />
                {searchTerm && (
                    <button
                        type="button"
                        className="clearButton"
                        onClick={clearInput}
                        aria-label="Clear search input"
                    >
                        ✕
                    </button>
                )}
            </form>
            <ul className="searchedUsersContainer">
                {filteredUsers.map((user) => (
                    <li key={user.username} className="searchedUser">
                        <div className="resultInfo">
                            <a href={`/profile/${user.username.toLowerCase()}`}>
                                <span>
                                    <strong>{user.username}</strong> 
                                    &nbsp;
                                    <small>
                                    ({user.firstName} {user.lastName})
                                    </small>
                                </span>
                            </a>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default SearchBar;
