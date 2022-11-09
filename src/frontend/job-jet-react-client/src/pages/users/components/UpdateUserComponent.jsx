import React, { useState } from 'react';
import UserService from '../services/UserService'

function UpdateUserComponent(props)
{
    const [user, setUser] = useState({
        id: this.props.match.params.id,
        userName: ''
    });

    function updateUser(e) {
        e.preventDefault();
        let userRequest = {
            userName: user.userName
        };

        UserService.updateUser(userRequest, user.id).then(res => {
            this.props.history.push('/users');
        });
    }

    changeUserNameHandler= (event) => {
        setUser({userName: event.target.value});
    };

    cancel= () => {
        this.props.history.push('/users');
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        UserService.getUserById(user.id).then((res) => {
            let userResponse = res.data;
            setUser({
                userName: userResponse.userName
            });
        });
    });

    return (
        <div>
            <br></br>
                <div className = "container">
                    <div className = "row">
                        <div className = "card col-md-6 offset-md-3 offset-md-3">
                            <h3 className="text-center">Update User</h3>
                            <div className = "card-body">
                                <form>
                                    <div className = "form-group">
                                        <label>Username</label>
                                        <input placeholder="Username" name="userName" className="form-control" 
                                            value={user.userName} />
                                    </div>

                                    <button className="btn btn-success" onClick={this.updateUser}>Save</button>
                                    <button className="btn btn-danger" onClick={this.cancel.bind(this)} style={{marginLeft: "10px"}}>Cancel</button>
                                </form>
                            </div>
                        </div>
                    </div>

                </div>
        </div>
    );
}

export default UpdateUserComponent;