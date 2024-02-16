import React, { useState, useEffect } from 'react';
import axios from 'axios';

function ProfileEdit() {
  const [profile, setProfile] = useState({ name: '', bio: '' });
  const [randomProfile, setRandomProfile] = useState({});
  const [matchProfiles, setMatchProfiles] = useState([]);
  const [currentMatchIndex, setCurrentMatchIndex] = useState(0);

  useEffect(() => {
    fetchRandomProfile();
    fetchMatches();
  }, []);

  const fetchRandomProfile = async () => {
    try {
      const response = await axios.get('http://localhost:5006/api/Profile/random');
      setRandomProfile(response.data);
    } catch (error) {
      console.error('Error fetching random profile:', error);
    }
  };

  const fetchMatches = async () => {
    try {
      const response = await axios.get('http://localhost:5006/api/Profile/matches');
      setMatchProfiles(response.data);
    } catch (error) {
      console.error('Error fetching matches:', error);
    }
  };

  const handleLike = async () => {
    try {
      await axios.put(`http://localhost:5006/api/Profile/${randomProfile.id}/like`);
      fetchRandomProfile();
    } catch (error) {
      console.error('Error liking profile:', error);
    }
  };

  const handleReject = async () => {
    try {
      await axios.put(`http://localhost:5006/api/Profile/${randomProfile.id}/reject`);
      fetchRandomProfile();
    } catch (error) {
      console.error('Error rejecting profile:', error);
    }
  };

  const handleSkip = () => {
    fetchRandomProfile();
  };

  const handleUpdateProfile = async () => {
    try {
      await axios.post('http://localhost:5006/api/User/update', profile);
    } catch (error) {
      console.error('Error updating profile:', error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setProfile({ ...profile, [name]: value });
  };

  const handleViewNextMatch = () => {
    setCurrentMatchIndex((prevIndex) => (prevIndex + 1) % matchProfiles.length);
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
              type="text"
              name="bio"
              value={profile.bio}
              onChange={handleChange}
              placeholder="Bio"
          />
          <button onClick={handleUpdateProfile}>Update</button>
        </div>
        <div>
          <h2>Random Profile</h2>
          <p>Name: {randomProfile.name}</p>
          <p>Bio: {randomProfile.bio}</p>
          <button onClick={handleLike}>Like</button>
          <button onClick={handleReject}>Reject</button>
          <button onClick={handleSkip}>Skip</button>
        </div>
        <div>
          <h2>Matches</h2>
          {matchProfiles.length > 0 ? (
              <div>
                <p>Name: {matchProfiles[currentMatchIndex].name}</p>
                <p>Bio: {matchProfiles[currentMatchIndex].bio}</p>
                <button onClick={handleViewNextMatch}>Next Match</button>
              </div>
          ) : (
              <p>No matches found.</p>
          )}
        </div>
      </div>
  );
}

export default ProfileEdit;
