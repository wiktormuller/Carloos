import "../login-styles.css";
import { useState, useContext } from "react";
import { Navigate, Link } from 'react-router-dom';
import LoginService from '../../../clients/LoginService';
import 'bootstrap/dist/css/bootstrap.min.css';
import AuthService from "../../../clients/AuthService";
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import { AuthenticationContext } from "../../../common/AuthenticationContext";

export default function LoginComponent()
{
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [loginError, setLoginError] = useState(''); // TODO: - How to use it at form
  const [redirect, setRedirect] = useState(false);

  const [show, setShow] = useState(false);
  const [emailToReset, setEmailToReset] = useState('');

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [ currentUser, setCurrentUser ] = useContext(AuthenticationContext);

  function handleTriggeringResetPassword(event)
    {
        event.preventDefault();

        let request = {
          email: emailToReset
        };

        AuthService.triggerResettingPassword(request)
        .then(res => {
            handleClose();
        });
    }

    function handleSetEmailToReset(event)
    {
        event.preventDefault();

        setEmailToReset(event.target.value);
    }

  const handleLogin = async (event) => {
    event.preventDefault();
    try {
      await LoginService.login({
        email: email,
        password: password
      });

      var loggedUser = LoginService.getAuthenticatedUser();
      setCurrentUser(loggedUser);

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
    <div className="login">
      {renderRedirected()}
      
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
            <Link to='/activate-account'>Activate account</Link>
            <br />
            <Link to='/reset-password'>Reset password</Link>
            <div>
              <Link variant="warning" onClick={handleShow}>
                  Send email for resetting password
              </Link>

              <Modal show={show} onHide={handleClose}>
                  <Modal.Header closeButton>
                  <Modal.Title>Sending email for resetting password</Modal.Title>
                  </Modal.Header>
                  <Modal.Body>

                      <Form onSubmit={handleTriggeringResetPassword}>
                          <Form.Group className="mb-3" controlId="formBasicEmail">
                              <Form.Label>Email Address</Form.Label>
                              <Form.Control type="email" placeholder="Enter email" onChange={handleSetEmailToReset} />
                              <Form.Text className="text-muted">
                                  We'll never share your email with anyone else.
                              </Form.Text>
                          </Form.Group>

                          <Button variant="primary" type="submit">
                              Trigger
                          </Button>
                      </Form>
                      
                  </Modal.Body>
                  <Modal.Footer>
                  <Button variant="secondary" onClick={handleClose}>
                      Close
                  </Button>
                  </Modal.Footer>
              </Modal>
            </div>
          </p>
        </div>
      </form>
    </div>
  );
}