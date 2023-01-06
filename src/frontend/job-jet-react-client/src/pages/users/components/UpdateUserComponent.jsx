import React, { useState, useEffect } from 'react';
import UserService from '../../../clients/UserService';
import { Navigate, useParams } from 'react-router-dom';
import '../update-user-styles.css';

export default function UpdateUserComponent(props)
{
    const { id } = useParams();
    
    const [redirect, setRedirect] = useState(false);

    const [user, setUser] = useState({
        id: id,
        userName: ''
    });

    function updateUser(event)
    {
        event.preventDefault();
        let userRequest = {
            userName: user.userName
        };

        UserService.updateUser(userRequest, user.id).then(res => {
            setRedirect(true);
        });
    }

    function changeUserNameHandler(event)
    {
        event.preventDefault();
        setUser({...user, userName: event.target.value});
    };

    function cancel(event)
    {
        event.preventDefault();
        setRedirect(true);
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        UserService.getUserById(user.id).then((res) => {
            let userResponse = res.data;
            setUser({
                userName: userResponse.userName
            });
        });
    }, []);

    const renderRedirected = () => {
        if (redirect) {
            return <Navigate to='/users' />
        }
    }

    return (
        <div className = "update-user">
            {renderRedirected()}
            
            <div className = "card col-md-6 offset-md-3 offset-md-3">
                <h3 className="text-center">Update User</h3>
                <div className = "card-body">
                    <form onSubmit={updateUser}>
                        <div className = "form-group">
                            <label>Username</label>
                            <input placeholder="Username" name="userName" className="form-control" 
                                value={user.userName} onChange={changeUserNameHandler} />
                        </div>

                        <button type="submit" className="btn btn-success">Save</button>
                        <button className="btn btn-danger" onClick={cancel} style={{marginLeft: "10px"}}>Cancel</button>
                    </form>
                </div>
            </div>
        </div>
    );
}