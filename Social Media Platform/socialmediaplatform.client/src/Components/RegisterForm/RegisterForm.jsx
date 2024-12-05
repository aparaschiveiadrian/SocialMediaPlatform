import './RegisterForm.css';
import { useState } from 'react';

const RegisterForm = () => {
    const [formData, setFormData] = useState({
        Username: '',
        FirstName: '',
        LastName: '',
        Email: '',
        Password: '',
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
        setMessage(null);
        console.log(formData);
        try {
            const response = await fetch('http://localhost:44354/registers', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData),
            });
            const data = await response.json();
            setMessage({ type: 'success', text: 'Registration successful!' });
            console.log('Registration successful:', data);
            
            setFormData({
                Username: '',
                FirstName: '',
                LastName: '',
                Email: '',
                Password: '',
            });
        } catch (error) {
            setMessage({ type: 'error', text: error.message });
            console.error('Error registering user:', error);
        }
    };

    return (
        <section className="registerForm">
            <div className="registerContainer">
            <span className="headline">Register</span>
                <hr className="postDivider" />
            <form className="userInfoContainer" onSubmit={handleSubmit}>
                <div className="userInfoItem">
                    <label htmlFor="username">Username</label>
                    <input
                        type="text"
                        id="username"
                        name="Username"
                        className="registerInput"
                        placeholder="Enter your username"
                        value={formData.username}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="userInfoItem">
                    <label htmlFor="first_name">First Name</label>
                    <input
                        type="text"
                        id="first_name"
                        name="FirstName"
                        className="registerInput"
                        placeholder="Enter your first name"
                        value={formData.FirstName}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="userInfoItem">
                    <label htmlFor="last_name">Last Name</label>
                    <input
                        type="text"
                        id="last_name"
                        name="LastName"
                        className="registerInput"
                        placeholder="Enter your last name"
                        value={formData.LastName}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="userInfoItem">
                    <label htmlFor="email">Email Address</label>
                    <input
                        type="email"
                        id="email"
                        name="Email"
                        className="registerInput"
                        placeholder="Enter your email"
                        value={formData.Email}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="userInfoItem">
                    <label htmlFor="password">Password</label>
                    <input
                        type="password"
                        id="password"
                        name="Password"
                        className="registerInput"
                        placeholder="Enter your password"
                        value={formData.Password}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit" className="btn">
                    Register
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
