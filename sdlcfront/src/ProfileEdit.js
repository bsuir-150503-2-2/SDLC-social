import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function ProfileEdit() {
  const [profile, setProfile] = useState({ id: 4, name: 'Joe Biden', bio: 'I like democracy' });
  const [randomProfile, setRandomProfile] = useState({ id : 3, name: 'Donald Trump', bio: 'I like liberalism' });
  const [matchProfile, setMatchingProfile] = useState({ if: 77, name: 'Barack Obama', bio: 'I prefer pizza' });
  const navigate = useNavigate();

  const fetchRandomProfile = async () => {
    try {
      const apiEndpoint = 'razam.com/random';
  
      const response = await fetch(apiEndpoint, {
        method: 'GET',

      });
  
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
  
      const data = await response.json();
      setRandomProfile(data);
    } catch (error) {
      console.error('Error fetching random profile:', error);
    }
  };

  const fetchRandomMatchProfile = async () => {
    try {
      const apiEndpoint = 'razam.com/matches';
  
      const response = await fetch(apiEndpoint, {
        method: 'GET',

      });
  
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
  
      const data = await response.json();
      setMatchingProfile(data[0]);
    } catch (error) {
      console.error('Error fetching random profile:', error);
    }
  };

  useEffect(() => {
    fetchRandomProfile();
    fetchRandomMatchProfile();
  }, []);

  const handleLike = async () => {
    const apiEndpoint = 'razam.com/' + randomProfile.id + '/like';
  
    const response = await fetch(apiEndpoint, {
      method: 'PUT',
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    fetchRandomProfile(); 
  };

  const handleSkip = () => {
    fetchRandomProfile(); 
  };

  const handleReject = async () => {
    const apiEndpoint = 'razam.com/' + randomProfile.id + '/reject';
  
    const response = await fetch(apiEndpoint, {
      method: 'PUT',
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    fetchRandomProfile(); 
  };

  const updateProfile = async () => {
    const apiEndpoint = 'razam.com/update';
  
    const response = await fetch(apiEndpoint, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json', 
      },
      body: JSON.stringify(profile),
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setProfile({ ...profile, [name]: value });
  };

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial' }}>
    <div style={{ marginBottom: '20px' }}>
      <h2 style={{ color: 'blue' }}>Edit Own Profile</h2>
        <input
        type="text"
        name="name"
        value={profile.name}
        onChange={handleChange}
        placeholder="Name"
      />
      <input
        type="bio"
        name="bio"
        value={profile.bio}
        onChange={handleChange}
        placeholder="Bio"
      />
        <button onClick={updateProfile}>Update</button>
      </div>
      <div>
        <h2>Random Profile</h2>
        <p>Name: {randomProfile.name}</p>
        <p>Bio: {randomProfile.bio}</p>
        <button onClick={handleLike}>Like</button>
        <button onClick={handleSkip}>Skip</button>
        <button onClick={handleReject}>Reject</button>
      </div>
      <div>
        <h2>Matches</h2>
        <p>Name: {matchProfile.name}</p>
        <p>Bio: {matchProfile.bio}</p>
      </div>
    </div>
  );
}

export default ProfileEdit;
