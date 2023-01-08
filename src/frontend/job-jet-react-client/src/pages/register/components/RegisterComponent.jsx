import "../register-styles.css";
import { useState } from "react";
import { useNavigate, Link } from 'react-router-dom';
import RegisterService from '../../../clients/RegisterService';

export default function RegisterComponent()
{
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [registerError, setRegisterError] = useState(''); // TODO: - How to use it at form

  const navigate = useNavigate();

  function handleRegister(event)
  {
    event.preventDefault();

    RegisterService.register({
      name: name,
      email: email,
      password: password
    }).then(response =>
      {
        navigate(`/activate-account`);;
      }
    );
  };

  return (
    <div className="register">
      <form className="Auth-form" onSubmit={handleRegister}>
        <div className="Auth-form-content">
          <h3 className="Auth-form-title">Sign In</h3>
          <div className="form-group mt-3">
            <label>Name</label>
            <input
              type="text"
              className="form-control mt-1 name-input"
              placeholder="Enter name"
              required
              onChange={(e) => {
                setName(e.target.value);
              }}
            />
          </div>
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
            Already have an account?
            <br />
            <Link to='/login'>Log-in here</Link>
          </p>
        </div>
      </form>
    </div>
  );
};