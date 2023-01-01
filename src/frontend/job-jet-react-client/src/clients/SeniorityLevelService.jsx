import axios from 'axios';
import Environment from './Environment';
import LoginService from './LoginService';

const SENIORITY_LEVELS_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/seniority-levels`;

class SeniorityLevelService {

    getSeniorityLevels()
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.get(SENIORITY_LEVELS_API_BASE_URL, config);
    }

    createSeniorityLevel(seniorityLevel)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.post(SENIORITY_LEVELS_API_BASE_URL, seniorityLevel, config);
    }

    getSeniorityLevelById(seniorityLevelId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.get(`${SENIORITY_LEVELS_API_BASE_URL}/${seniorityLevelId}`, config);
    }

    updateSeniorityLevel(seniorityLevel, seniorityLevelId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.put(`${SENIORITY_LEVELS_API_BASE_URL}/${seniorityLevelId}`, seniorityLevel, config);
    }

    deleteSeniorityLevel(seniorityLevelId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.delete(`${SENIORITY_LEVELS_API_BASE_URL}/${seniorityLevelId}`, config);
    }
}

export default new SeniorityLevelService();