import React, { useState, useContext } from 'react';
import UserService from '../../../clients/UserService'
import { useNavigate } from 'react-router-dom';
import '../user-styles.css';
import { AuthenticationContext } from "../../../common/AuthenticationContext";

export default function ListUsersComponent()
{
    const [users, setUsers] = useState([]);
    const navigate = useNavigate();
    const [ currentUser, setCurrentUser ] = useContext(AuthenticationContext);

    function deleteUser(id) {
        UserService.deleteUser(id).then(re => {
            setUsers(UserService.getUsers())
        });
    }

    function viewUser(id) {
        navigate(`/users/${id}`);
    }

    function editUser(id) {
        navigate(`/users/update/${id}`);  
    }

    if (currentUser && currentUser.accessToken !== null)
    {
        UserService.getUsers(currentUser.accessToken).then(res => {
            setUsers(res.data.response.data);
        });
    }

    return (
        <div className="users">
             <h2 className="text-center">Users List</h2>
             <br></br>
             <div className = "row">
                    <table className = "table table-striped table-bordered">

                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Username</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                users.map(
                                    user => 
                                    <tr key = {user.id}>
                                         <td> {user.id} </td>
                                         <td> {user.userName} </td>   
                                         <td> {user.email}</td>
                                         <td>
                                             <button onClick={ () => editUser(user.id)} className="btn btn-info">Update</button>
                                             <button style={{marginLeft: "10px"}} onClick={ () => deleteUser(user.id)} className="btn btn-danger">Delete</button>
                                             <button style={{marginLeft: "10px"}} onClick={ () => viewUser(user.id)} className="btn btn-info">View</button>
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