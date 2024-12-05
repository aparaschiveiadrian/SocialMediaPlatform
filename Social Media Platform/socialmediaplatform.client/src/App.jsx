import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "@/Components/Navbar/Navbar.jsx";
import Footer from "@/Components/Footer/Footer.jsx";
import Feed from "@/Components/Feed/Feed.jsx";
import RegisterPage from "@/Pages/RegisterPage/RegisterPage.jsx";
import ProfilePage from "@/Pages/ProfilePage/ProfilePage.jsx";
import LoginPage from "@/Pages/LoginPage/LoginPage.jsx";


const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="register" element={<RegisterPage />} />
                <Route path="login" element={<LoginPage/>}/>
                <Route path="profile/:id" element={<ProfilePage />} /> {/* Dynamic Route */}
                <Route path="/" element={
                    <>
                        <Navbar />
                        <Feed />
                        <Footer />
                    </>
                } />
            </Routes>
        </BrowserRouter>
    );
};

export default App;
