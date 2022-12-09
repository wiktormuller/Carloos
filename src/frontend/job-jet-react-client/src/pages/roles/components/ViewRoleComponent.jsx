import React, { useState, useEffect } from 'react';
import RoleService from '../../../clients/RoleService'
import { useParams } from 'react-router-dom';

export default function ViewRoleComponent(props)
{
    const { id } = useParams();
    const[roleState, setRoleState] = useState({
        id: id,
        role: {}
    });

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        RoleService.getRoleById(roleState.id).then((res) => {
            setRoleState({
                role: res.data
            })
        });
    });

    return (
        <div>
            <br></br>
            <div className = "card col-md-6 offset-md-3">
                <h3 className = "text-center"> View Role Details</h3>
                <div className = "card-body">
                    <div className = "row">
                        <label>Id:</label>
                        <div> { roleState.id }</div>
                    </div>
                    <div className = "row">
                        <label>Name:</label>
                        <div> { roleState.role.name }</div>
                    </div>
                </div>

            </div>
        </div>
    );
}