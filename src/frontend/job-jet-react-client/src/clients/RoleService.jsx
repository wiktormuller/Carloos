import axios from 'axios';
import Environment from './Environment';
import LoginService from './LoginService';

const ROLES_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/roles`;

class RoleService {

    getRoles()
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.get(ROLES_API_BASE_URL, config);
    }

    createRole(role)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.post(ROLES_API_BASE_URL, role, config);
    }

    getRoleById(roleId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.get(`${ROLES_API_BASE_URL}/${roleId}`, config);
    }
}

export default new RoleService();