import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "@/Components/Navbar/Navbar.jsx";
import Footer from "@/Components/Footer/Footer.jsx";
import Feed from "@/Pages/FeedPage/Feed.jsx";
import RegisterPage from "@/Pages/RegisterPage/RegisterPage.jsx";
import ProfilePage from "@/Pages/ProfilePage/ProfilePage.jsx";
import LoginPage from "@/Pages/LoginPage/LoginPage.jsx";
import NotificationsPage from "@/Pages/NotificationsPage/NotificationsPage.jsx";
import ConversationsPage from "@/Pages/ConversationsPage/ConversationsPage.jsx";

const App = () => {
    
    return (
        <BrowserRouter>
            <Routes>
                <Route path="register" element={<RegisterPage />} />
                <Route path="login" element={<LoginPage/>}/>
                <Route path="profile/:username" element={<ProfilePage />} /> {/* Dynamic Route */}
                <Route path="notifications" element={<NotificationsPage/>} />
                <Route path="conversations" element={<ConversationsPage/>} />
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
