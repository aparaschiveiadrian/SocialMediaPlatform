import '../RegisterForm/RegisterForm.css'
import { useState } from 'react';
import Cookies from 'js-cookie';
import {redirect} from "react-router";
//import {jwtDecode} from "jwt-decode";
const RegisterForm = () => {
    const [formData, setFormData] = useState({
        username: '',
        password: '',
    });
    const [message, setMessage] = useState(null);

    const handleChange = (event) => {
        const { name, value } = event.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };
    const handleSubmit = async (event) => {
        event.preventDefault();
        setMessage(null); // Reset the message initially

        try {
            const response = await fetch('https://localhost:44354/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData),
                credentials: 'include'
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData?.message || `HTTP error! status: ${response.status}`);
            }

            const data = await response.json(); // Parse successful response
            Cookies.set('userId', data.id, { expires: 1, secure: true, sameSite: 'Strict' });
            Cookies.set('username', data.username, { expires: 1, secure: true, sameSite: 'Strict' });
            Cookies.set('firstname', data.firstName, { expires: 1, secure: true, sameSite: 'Strict' }); // Expires in 1 day
            Cookies.set('lastname', data.lastName, { expires: 1, secure: true, sameSite: 'Strict' });
            Cookies.set('email', data.email, { expires: 1, secure: true, sameSite: 'Strict' });




            console.log(data);

            setMessage({ type: 'success', text: 'Login successful!' });
            console.log('Login successful:', data);

            // Redirect the user to another page (home or dashboard)
            window.location.href = '/';

        } catch (error) {
            console.error('Error during login:', error);
            setMessage({ type: 'error', text: error.message || 'Something went wrong!' });
        }
    };


    return (
        <section className="registerForm">
            <div className="registerContainer">
                <span className="headline">Login</span>
                <hr className="postDivider" />
                <form className="userInfoContainer" onSubmit={handleSubmit}>
                    <div className="userInfoItem">
                        <label htmlFor="username">Username</label>
                        <input
                            type="text"
                            id="username"
                            name="username"
                            className="registerInput"
                            placeholder="Enter your username"
                            value={formData.username}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="userInfoItem">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            className="registerInput"
                            placeholder="Enter your password"
                            value={formData.password}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <button type="submit" className="btn">
                        Login
                    </button>
                    {message && (
                        <p
                            className={`message ${
                                message.type === 'success' ? 'successMessage' : 'errorMessage'
                            }`}
                        >
                            {message.text}
                        </p>
                    )}
                </form>
            </div>
        </section>
    );
};

export default RegisterForm;
