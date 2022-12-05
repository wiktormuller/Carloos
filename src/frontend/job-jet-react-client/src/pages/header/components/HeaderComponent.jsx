import "../header-styles.css";
import { useContext, useState } from 'react';
import { Link, Navigate } from "react-router-dom";
import { AuthenticationContext } from "../../../common/AuthenticationContext";

export default function HeaderComponent()
{
  const [ currentUser, setCurrentUser ] = useContext(AuthenticationContext);

  const[redirect, setRedirect] = useState(false);

  const handleLogOut = () => {
    localStorage.removeItem('loginResponse');
		setCurrentUser({});
		
    setRedirect(true);
  };

  const renderRedirected = () => {
    if (redirect) {
      return <Navigate to='/' />
    }
  }

  const loginOrRegisterButton = () => {
    if (!currentUser) {
      return (
        <div className="custom-header">
          <Link className="custom-link" to="/login">
            <button className="btn btn-primary">LogIn</button>
          </Link>
          <Link className="custom-link" to="/register">
            <button className="btn btn-primary">Register</button>
          </Link>
        </div>
      );
    }
    else if (currentUser.accessToken) {
      return (
        <div className="custom-header">
          <Link to="/profile">
            <button className="btn btn-primary">My profile</button>
          </Link>
          <Link to="/">
            <button className="btn btn-primary" type="button" onClick={handleLogOut}>LogOut</button>
          </Link>
        </div>
      );
    }
  };

  return (
    <div className="header">
      {renderRedirected()}
      <Link className="header-link" to="/">
        <p className="header-logo">JobJet</p>
      </Link>
      {loginOrRegisterButton()}
    </div>
  );
}