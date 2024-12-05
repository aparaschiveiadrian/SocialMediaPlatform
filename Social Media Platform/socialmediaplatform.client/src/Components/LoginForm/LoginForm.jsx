import '../RegisterForm/RegisterForm.css'
import { useState } from 'react';
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
        console.log("Submitting form data:", formData);

        try {
            const response = await fetch('https://localhost:44354/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData),
            });
            // Check if the response status is not OK (status code 2xx)
            if (!response.ok) {
                const errorData = await response.json(); // Try to parse error response if available
                throw new Error(
                    errorData?.message || `HTTP error! status: ${response.status}`
                );
            }
            if(response.ok) {
                const data = await response.json(); // Parse successful response
                setMessage({type: 'success', text: 'Registration successful!'});
                console.log('Registration successful:', data);
                const userId = data.id;
                const username = data.username;
                localStorage.setItem("userId", userId);
                localStorage.setItem("username", username);
                window.location.href = '/';
            }
            // Reset the form
            setFormData({
                username: '',
                password: '',
            });
        } catch (error) {
            // Log detailed error and set error message
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
