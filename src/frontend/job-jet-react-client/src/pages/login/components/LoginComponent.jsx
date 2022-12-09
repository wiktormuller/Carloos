import "../login-styles.css";
import { useState } from "react";
import { Navigate, Link } from 'react-router-dom';
import LoginService from '../../../clients/LoginService';
import 'bootstrap/dist/css/bootstrap.min.css';

export default function LoginComponent(props)
{
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loginError, setLoginError] = useState(''); // TODO: - How to use it at form
  const [redirect, setRedirect] = useState(false);

  const handleLogin = async (event) => {
    event.preventDefault();
    try {
      await LoginService.login({
        email: email,
        password: password
      });
      setRedirect(true);
    }
    catch (error) {
      console.log('error', error);
      setLoginError(error);
    }
  };

  const renderRedirected = () => {
    if (redirect) {
      return <Navigate to='/' />
    }
  }

  return (
    <div>
      {renderRedirected()}
      <div className="login">
        <form className="Auth-form" onSubmit={handleLogin}>
          <div className="Auth-form-content">
            <h3 className="Auth-form-title">Sign In</h3>
            <div className="form-group mt-3">
              <label>Email address</label>
              <input
                type="email"
                className="form-control mt-1 email-input"
                placeholder="Enter email"
                required
                onChange={(e) => {
                  setEmail(e.target.value);
                }}
              />
            </div>
            <div className="form-group mt-3">
              <label>Password</label>
              <input
                type="password"
                className="form-control mt-1 password-input"
                placeholder="Enter password"
                required
                onChange={(e) => {
                  setPassword(e.target.value);
                }}
              />
            </div>
            <div className="d-grid gap-2 mt-3">
              <button type="submit" className="btn btn-primary">
                Submit
              </button>
            </div>
            <p className="forgot-password text-right mt-2">
              <Link to='/register'>Don't have an account yet?</Link>
              <br />
              <Link to='/reset-password'>Reset password</Link>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
}