import axios from 'axios';
import Environment from './Environment';
import LoginService from './LoginService';

const USERS_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/users`;

class UserService
{
    getUsers()
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.get(
            USERS_API_BASE_URL, config
        );
    }

    getUserById(usersId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.get(`${USERS_API_BASE_URL}/${usersId}`, config);
    }

    updateUser(user, userId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.put(`${USERS_API_BASE_URL}/${userId}`, user, config);
    }

    deleteUser(userId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.delete(`${USERS_API_BASE_URL}/${userId}`, config);
    }
}

export default new UserService();