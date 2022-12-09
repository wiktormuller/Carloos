import React, { useState, useEffect } from 'react';
import RoleService from '../../../clients/RoleService';
import { useNavigate } from "react-router-dom";
import "../role-styles.css";

export default function ListRolesComponent()
{
    const [roles, setRoles] = useState([]);
    const navigate = useNavigate();

    function viewRole(id) {
        navigate(`/roles/${id}`);
    }

    function addRole() {
        navigate(`/roles/create`);
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        RoleService.getRoles().then(res => {
            setRoles(res.data);
        });
    });

    return (
        <div className="roles">
             <h2 className="text-center">Roles List</h2>
             <div className = "row">
                <button className="btn btn-primary" onClick={addRole}>Add role</button>
             </div>
             <br></br>
             <div className = "row">
                    <table className = "table table-striped table-bordered">

                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Name</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                roles.map(
                                    role => 
                                    <tr key = {role.id}>
                                         <td> {role.name} </td>
                                         <td>
                                             <button style={{marginLeft: "10px"}} onClick={ () => viewRole(role.id)} className="btn btn-info">View</button>
                                         </td>
                                    </tr>
                                )
                            }
                        </tbody>
                    </table>

             </div>

        </div>
    )
}