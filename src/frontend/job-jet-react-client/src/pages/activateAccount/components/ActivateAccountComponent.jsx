import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../activate-account-styles.css';
import AuthService from '../../../clients/AuthService';

export default function ActivateAccountComponent()
{
    const [email, setEmail] = useState('');
    const [token, setToken] = useState('');

    const navigate = useNavigate();

    function activateAccount()
    {
        let activationRequest = {
            email: email,
            token: token
        };

        AuthService.activateAccount(activationRequest).then(res => {
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

    function cancel()
    {
        navigate(`/`);
    }

    return (
        <div className="activate-account">
            <div className = "container">
                <div className = "row">
                    <div className = "card col-md-6 offset-md-3 offset-md-3">
                        <h3 className="text-center">Activate Account</h3>
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

                                <button className="btn btn-success" onClick={activateAccount()}>Activate</button>
                                <button className="btn btn-danger" onClick={cancel()} style={{marginLeft: "10px"}}>Cancel</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}