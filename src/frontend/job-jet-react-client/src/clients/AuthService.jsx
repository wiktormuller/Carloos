import axios from 'axios';
import Environment from './Environment';

const AUTH_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/auth`;

class AuthService
{
    activateAccount(request){
        return axios.post(`${AUTH_API_BASE_URL}/activate`, request);
    }

    resetPassword(request){
        return axios.post(`${AUTH_API_BASE_URL}/reset-password`, request);
    }

    triggerResettingPassword(request){
        return axios.post(`${AUTH_API_BASE_URL}/trigger-resetting-password`, request);
    }
}

export default new AuthService();