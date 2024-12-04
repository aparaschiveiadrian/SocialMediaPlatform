import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom"; // Correct import
import Navbar from "@/Components/Navbar/Navbar.jsx";
import Footer from "@/Components/Footer/Footer.jsx";
import Feed from "@/Components/Feed/Feed.jsx";
import RegisterPage from "@/Pages/RegisterPage/RegisterPage.jsx";
const App = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="register" element={
                    <RegisterPage/>
                } />
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
