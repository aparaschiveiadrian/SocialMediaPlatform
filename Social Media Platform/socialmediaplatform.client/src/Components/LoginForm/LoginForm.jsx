import '../RegisterForm/RegisterForm.css'
import { useState } from 'react';

const RegisterForm = () => {
    const [formData, setFormData] = useState({
        Username: '',
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
                <span className="headline">Login</span>
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
                            value={formData.Username}
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
