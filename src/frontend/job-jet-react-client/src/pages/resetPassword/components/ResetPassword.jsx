import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../reset-password.css';
import AuthService from '../../../clients/AuthService';

export default function ResetPasswordComponent()
{
    const [email, setEmail] = useState('');
    const [token, setToken] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();

    function resetPassword()
    {
        let resetPassword = {
            email: email,
            token: token,
            newPassword: password
        };

        AuthService.resetPassword(resetPassword).then(res => {
            navigate(`/`);
        });
    }

    function changeEmailHandler(event)
    {
        event.preventDefault();

        setEmail(event.target.value);
    }

    function changeTokenHandler(event)
    {
        event.preventDefault();

        setToken(event.target.value);
    }

    function changePasswordHandler(event)
    {
        event.preventDefault();

        setPassword(event.target.value);
    }

    function cancel()
    {
        navigate(`/`);
    }

    return (
        <div className="reset-password">
            <div className = "container">
                <div className = "row">
                    <div className = "card col-md-6 offset-md-3 offset-md-3">
                        <h3 className="text-center">Reset Password</h3>
                        <div className = "card-body">
                            <form>
                                <div className = "form-group">
                                    <label>Email:</label>
                                    <input placeholder="Email" name="email" className="form-control" 
                                        value={email} onChange={changeEmailHandler}/>
                                </div>

                                <div className = "form-group">
                                    <label>Token:</label>
                                    <input placeholder="Token" name="token" className="form-control" 
                                        value={token} onChange={changeTokenHandler}/>
                                </div>

                                <div className = "form-group">
                                    <label>New Password:</label>
                                    <input placeholder="Password" name="password" className="form-control" 
                                        value={password} onChange={changePasswordHandler}/>
                                </div>

                                <button className="btn btn-success" onClick={resetPassword()}>Reset</button>
                                <button className="btn btn-danger" onClick={cancel()} style={{marginLeft: "10px"}}>Cancel</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}