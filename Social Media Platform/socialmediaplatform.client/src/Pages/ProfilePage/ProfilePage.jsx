import './ProfilePage.css';
import { useParams } from "react-router";
import Navbar from "@/Components/Navbar/Navbar.jsx";
import { useEffect, useState } from "react";
import Cookies from "js-cookie";
// Remove jwtDecode if not used
import ProfileCard from "@/Components/Profile/Profile.jsx";

const ProfilePage = () => {
    const { username } = useParams();
    const [profile, setProfile] = useState(null);
    const [error, setError] = useState("");

    const getUser = async () => {
        try {
            // const token = Cookies.get("authToken");
            // const decoded = jwtDecode(token);
            // const userId = decoded.sub;

            const response = await fetch(`https://localhost:44354/user/${username}`);
            if (response.ok) {
                const data = await response.json();
                setProfile(data); 
                setError(""); 
            } else {
                setError(`Failed to fetch profile: ${response.status} ${response.statusText}`);
            }
        } catch (error) {
            setError(`An error occurred: ${error.message}`);
        }
    };

    useEffect(() => {
        getUser();
    }, []);

    return (
        <>
            <Navbar />
                <section className="profileSection">
                <h1>Profile Information</h1>
                {error ? (
                    <p className="error-message errorMessage">There is no user with such username!</p>
                ) : profile ? (
                    <ProfileCard
                        firstName={profile.firstName}
                        lastName={profile.lastName}
                        username={profile.username}
                        initialDescription={profile.description}
                    />
                ) : (
                    <p>Loading...</p>
                )}
                </section>
        </>
    );
};

export default ProfilePage;
