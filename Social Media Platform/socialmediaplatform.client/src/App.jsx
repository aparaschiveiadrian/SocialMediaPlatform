import './App.css'
import Navbar from "@/Components/Navbar/Navbar.jsx";
import Footer from "@/Components/Footer/Footer.jsx";
import Feed from "@/Components/Feed/Feed.jsx";
const App =()=> {
    return (
        <>
            <Navbar />
            <Feed/>
            
            <Footer />
        </>
    )
}
export default App;