import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

function Login() {
  const [credentials, setCredentials] = useState({ User: '', Password: '' });
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCredentials({ ...credentials, [name]: value });
  };

  const handleRegister = async (e) => {
    e.preventDefault();
    try {
      const apiEndpoint = 'http://localhost:5006/api/User/register';

      const response = await axios.post(apiEndpoint, {
        UserName: credentials.UserName,
        Password: credentials.Password
      });

      const token = response.data.token;

      localStorage.setItem('token', token);

      navigate('/edit-profile');
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const apiEndpoint = 'http://localhost:5006/api/User/login';

      const response = await axios.post(apiEndpoint, {
        UserName: credentials.UserName,
        Password: credentials.Password
      });

      const token = response.data.token;

      localStorage.setItem('token', token);

      navigate('/edit-profile');
    } catch (error) {
      console.error('Error:', error);
    }
  };

  return (
      <div style={{ padding: '20px', fontFamily: 'Arial' }}>
        <div style={{ marginBottom: '20px' }}>
          <h2 style={{ color: 'blue' }}>Register/Login</h2>
          <form>
            <input
                type="text"
                name="UserName"
                value={credentials.UserName}
                onChange={handleChange}
                placeholder="Login"
            />
            <input
                type="password"
                name="Password"
                value={credentials.Password}
                onChange={handleChange}
                placeholder="Password"
            />
            <button onClick={handleRegister}>Register</button>
            <button onClick={handleLogin}>Login</button>
          </form>
        </div>
      </div>
  );
}

export default Login;
