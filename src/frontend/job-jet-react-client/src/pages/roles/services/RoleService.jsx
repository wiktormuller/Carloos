import axios from 'axios';

const ROLES_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/roles";

class RoleService {

    getRoles(){
        return axios.get(ROLES_API_BASE_URL);
    }

    createRole(role){
        return axios.post(ROLES_API_BASE_URL, role);
    }

    getRoleById(roleId){
        return axios.get(ROLES_API_BASE_URL + '/' + roleId);
    }
}

export default new RoleService();