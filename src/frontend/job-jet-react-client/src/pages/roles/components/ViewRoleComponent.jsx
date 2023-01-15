import React, { useState, useEffect } from 'react';
import RoleService from '../../../clients/RoleService'
import { useParams } from 'react-router-dom';

export default function ViewRoleComponent(props)
{
    const { id } = useParams();
    const[roleState, setRoleState] = useState({
        role: {}
    });

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        RoleService.getRoleById(id).then((res) => {
            setRoleState({
                role: res.data
            })
        });
    });

    return (
        <div className="roles">
            <div className = "card col-md-6 offset-md-3">
                <h3 className = "text-center"> View Role Details</h3>
                <div className = "card-body">
                    <div className = "row">
                        <label>Id</label>
                        <p>{ roleState.role.id }</p>
                    </div>
                    <div className = "row">
                        <label>Name</label>
                        <p>{ roleState.role.name }</p>
                    </div>
                </div>
            </div>
        </div>
    );
}