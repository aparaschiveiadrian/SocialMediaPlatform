import './ProfilePage.css'
import {useParams} from "react-router";
import Navbar from "@/Components/Navbar/Navbar.jsx";
const ProfilePage = () => {
    const {username} = useParams();
    return (
        <>
            <Navbar/>
            <div>
                {username}
            </div>
        </>
    )
}
export default ProfilePage