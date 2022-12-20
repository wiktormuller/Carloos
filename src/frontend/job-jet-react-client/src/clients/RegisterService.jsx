import axios from 'axios';
import Environment from './Environment';

const REGISTER_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/auth/register`;

class RegisterService
{
    async register(credentials)
    {
        const response = axios.post(REGISTER_API_BASE_URL, credentials);

        const id = response.data.id;
        if (id) {
            return response.data;
        }

        return response.data;
    }
}

export default new RegisterService();