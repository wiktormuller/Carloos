import React, { useState } from 'react';
import UserService from '../services/UserService'

function ListUsersComponent(props)
{
    const [users, setUsers] = useState([]);

    function deleteUser(id) {
        UserService.deleteUser(id).then(re => {
            setUsers(UserService.getUsers())
        });
    }

    function viewUser(id) {
        this.props.history.push(`/view-users/${id}`);
    }

    function editUser(id) {
        this.props.history.push(`/edit-users/${id}`);
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        UserService.getUsers().then(res => {
            setUsers(res.data.response.data);
        });
    });

    return (
        <div>
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
                                         <td> {user.userName} </td>   
                                         <td> {user.email}</td>
                                         <td>
                                             <button onClick={ () => this.editUser(user.id)} className="btn btn-info">Update</button>
                                             <button style={{marginLeft: "10px"}} onClick={ () => this.deleteUser(user.id)} className="btn btn-danger">Delete</button>
                                             <button style={{marginLeft: "10px"}} onClick={ () => this.viewUser(user.id)} className="btn btn-info">View</button>
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

export default ListUsersComponent;