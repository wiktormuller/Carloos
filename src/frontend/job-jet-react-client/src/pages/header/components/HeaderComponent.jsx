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
        <div className="header__unregistered-user">
          <Link className="custom-link" to="/login">
            <button className="header__btn">LogIn</button>
          </Link>
          <Link className="custom-link" to="/register">
            <button className="header__btn">Register</button>
          </Link>
        </div>
      );
    }
    else if (currentUser.accessToken) {
      return (
        <div className="header__registered-user">
          <Link className="custom-link" to="/profile">
            <button className="header__btn">My profile</button>
          </Link>
          <Link className="custom-link" to="/">
            <button className="header__btn" type="button" onClick={handleLogOut}>LogOut</button>
          </Link>
        </div>
      );
    }
  };

  return (
    <div>
      {renderRedirected()}
      <div className="header">
        <Link className="custom-link" to="/">
          <p className="header__logo">JobJet</p>
        </Link>
        {loginOrRegisterButton()}
      </div>
    </div>
  );
}