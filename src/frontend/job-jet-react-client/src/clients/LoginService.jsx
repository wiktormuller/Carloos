import axios from 'axios';
import LocalStorageManager from '../common/LocalStorageManager';
import jwt_decode from "jwt-decode";
import Environment from './Environment';

const LOGIN_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/auth/login`;

class LoginService
{
    async login(credentials)
    {
        const response = await axios.post(LOGIN_API_BASE_URL, credentials);

        console.log(response);
        const token = response.data.accessToken;
        if (token) {
            LocalStorageManager.setActualLoginResponse(response);
        }

        return response.data;
    }

    getAuthenticatedUser()
    {
        const loginResponse = LocalStorageManager.getActualLoginResponse();

        if (loginResponse === null) {
            return '';
        }

        const decodedLoginResponse = JSON.parse(loginResponse);

        // Extend loginResponse by decoded JWT token
        var decodedAccessToken = jwt_decode(decodedLoginResponse.accessToken);
        var hasAdministratorRole = decodedAccessToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'].includes('Administrator');
        var hasUserRole = decodedAccessToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'].includes('User');

        decodedLoginResponse.decodedAccessToken = decodedAccessToken;
        decodedLoginResponse.hasAdministratorRole = hasAdministratorRole;
        decodedLoginResponse.hasUserRole = hasUserRole;

        return decodedLoginResponse;
    }
}

export default new LoginService();