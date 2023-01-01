import axios from 'axios';
import Environment from './Environment';
import LoginService from './LoginService';

const PROFILE_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/profiles/me`;

class ProfileService
{
    getProfile()
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.get(PROFILE_API_BASE_URL, config);
    }
}

export default new ProfileService();