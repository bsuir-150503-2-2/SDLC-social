import React from 'react';
import { Link } from 'react-router-dom';

function Confirmation() {
  return (
    <div>
      <h1>Profile Updated Successfully!</h1>
      <Link to="/">Go back to Login</Link>
    </div>
  );
}

export default Confirmation;
