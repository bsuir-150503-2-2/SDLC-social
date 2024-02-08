import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

function Login() {
  const [credentials, setCredentials] = useState({ login: '', password: '' });
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCredentials({ ...credentials, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const apiEndpoint = 'razam.com/register';
  
      const response = await fetch(apiEndpoint, {
        method: 'POST', 
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(credentials), 
      });
  
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
    } catch (error) {
      console.error('Error fetching random profile:', error);
    }
    navigate('/edit-profile');
  };

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial' }}>
    <div style={{ marginBottom: '20px' }}>
    <h2 style={{ color: 'blue' }}>Register/login</h2>
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        name="login"
        value={credentials.login}
        onChange={handleChange}
        placeholder="Login"
      />
      <input
        type="password"
        name="password"
        value={credentials.password}
        onChange={handleChange}
        placeholder="Password"
      />
      <button type="submit">Login</button>
    </form>
    </div>
    </div>
  );
}

export default Login;
