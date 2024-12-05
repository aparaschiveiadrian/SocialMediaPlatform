import './LoginPage.css'
import Navbar from "@/Components/Navbar/Navbar.jsx";
import Footer from "@/Components/Footer/Footer.jsx";
import LoginForm from "@/Components/LoginForm/LoginForm.jsx";

const LoginPage = () => {
    return (
        <>
            <Navbar/>
            <LoginForm/>
            <Footer/>
        </>
    )
}
export default LoginPage