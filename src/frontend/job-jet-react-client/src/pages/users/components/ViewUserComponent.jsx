import React, { useState, useEffect } from 'react';
import UserService from '../../../clients/UserService';
import { useParams } from 'react-router-dom';

export default function ViewUserComponent(props)
{
    const { id } = useParams();
    const[userState, setUserState] = useState({
        user: {}
    });

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        UserService.getUserById(id).then((res) => {
            setUserState({
                user: res.data
            })
        });
    });

    return (
        <div className="user-details">
            <h3 className = "text-center"> View User Details</h3>
            <div className = "card col-md-6 offset-md-3">
                <div className = "card-body">
                    <div className = "row">
                        <label>Id</label>
                        <p>{ userState.user.id }</p>
                    </div>
                    <div className = "row">
                        <label>Username</label>
                        <p>{ userState.user.userName }</p>
                    </div>
                    <div className = "row">
                        <label>Email</label>
                        <p>{ userState.user.email }</p>
                    </div>
                </div>
            </div>
        </div>
    );
}