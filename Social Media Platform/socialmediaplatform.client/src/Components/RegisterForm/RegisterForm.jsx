import './RegisterForm.css';
import { useState } from 'react';

const RegisterForm = () => {
    const [formData, setFormData] = useState({
        username: '',
        first_name: '',
        last_name: '',
        email: '',
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
        setMessage(null);

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
                username: '',
                first_name: '',
                last_name: '',
                email: '',
                password: '',
            });
        } catch (error) {
            setMessage({ type: 'error', text: error.message });
            console.error('Error registering user:', error);
        }
    };

    return (
        <section className="registerForm">
            <form className="userInfoContainer" onSubmit={handleSubmit}>
                <div className="userInfoItem">
                    <label htmlFor="username">Username</label>
                    <input
                        type="text"
                        id="username"
                        name="username"
                        className="postInput"
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
                        name="first_name"
                        className="postInput"
                        placeholder="Enter your first name"
                        value={formData.first_name}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="userInfoItem">
                    <label htmlFor="last_name">Last Name</label>
                    <input
                        type="text"
                        id="last_name"
                        name="last_name"
                        className="postInput"
                        placeholder="Enter your last name"
                        value={formData.last_name}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="userInfoItem">
                    <label htmlFor="email">Email Address</label>
                    <input
                        type="email"
                        id="email"
                        name="email"
                        className="postInput"
                        placeholder="Enter your email"
                        value={formData.email}
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
                        className="postInput"
                        placeholder="Enter your password"
                        value={formData.password}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit" className="submitButton">
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
        </section>
    );
};

export default RegisterForm;
