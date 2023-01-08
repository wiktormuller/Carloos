import axios from 'axios';
import Environment from './Environment';

const REGISTER_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/auth/register`;

class RegisterService
{
    register(credentials)
    {
        return axios.post(REGISTER_API_BASE_URL, credentials);
    }
}

export default new RegisterService();