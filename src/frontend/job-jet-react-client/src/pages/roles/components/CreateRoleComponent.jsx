import React, { useState } from 'react';
import RoleService from '../../../clients/RoleService'
import { useNavigate } from 'react-router-dom';
import "../role-styles.css";

export default function CreateRoleComponent()
{
    const [role, setRole] = useState({
        name: ''
    });

    const navigate = useNavigate();

    function saveRole(event)
    {
        event.preventDefault();
        let roleRequest = {
            name: role.name
        };

        RoleService.createRole(roleRequest).then(res => {
            navigate('/roles');
        });
    }

    function changeNameHandler(event)
    {
        setRole({
            name: event.target.value
        });
    }

    function cancel(event)
    {
        event.preventDefault();
        navigate('/roles');
    }

    return (
        <div className="roles">
            <br></br>
                <div className = "container">
                    <div className = "row">
                        <div className = "card col-md-6 offset-md-3 offset-md-3">
                            <h3 className="text-center">Add Role</h3>
                            <div className = "card-body">
                                <form>
                                    <div className = "form-group">
                                        <label>Name:</label>
                                        <input placeholder="Name" name="name" className="form-control" 
                                            value={role.name} onChange={changeNameHandler}/>
                                    </div>

                                    <button className="btn btn-success" onClick={saveRole}>Save</button>
                                    <button className="btn btn-danger" onClick={cancel} style={{marginLeft: "10px"}}>Cancel</button>
                                </form>
                            </div>
                        </div>
                    </div>

                </div>
        </div>
    );
}