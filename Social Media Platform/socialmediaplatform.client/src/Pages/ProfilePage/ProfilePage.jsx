import './ProfilePage.css'
import {useParams} from "react-router";
import Navbar from "@/Components/Navbar/Navbar.jsx";
const ProfilePage = () => {
    const {id} = useParams();
    return (
        <>
            <Navbar/>
            <div>
                {id}
            </div>
        </>
    )
}
export default ProfilePage