import './ProfilePage.css'
import {useParams} from "react-router";
import Navbar from "@/Components/Navbar/Navbar.jsx";
import {useEffect} from "react";
import Cookies from "js-cookie";
import {jwtDecode} from "jwt-decode";
const ProfilePage = () => {
    const {username} = useParams();
    const getUser = async () => {
        
        const token = Cookies.get("authToken");
        try {
            const decoded = jwtDecode(token);
            const userId = decoded.sub;
            const response = await fetch(`https://localhost:44354/user/${userId}`);
            if (!response.ok) {
                const errorData = await response.json(); // Try to parse error response if available
                throw new Error(
                    errorData?.message || `HTTP error! status: ${response.status}`
                );
            }
            if(response.ok) {
                const data = await response.json(); // Parse successful response
                console.log(data);
            }
            
            }
         catch (error) {
            console.error('Error fetching users:', error);
        }
    };
    useEffect(() => {
        getUser()},[])
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