import axios from 'axios';
import Environment from './Environment';
import LoginService from './LoginService';

const TECHNOLOGY_TYPES_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/technology-types`;

class TechnologyTypeService {

    getTechnologyTypes()
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.get(TECHNOLOGY_TYPES_API_BASE_URL, config);
    }

    createTechnologyType(technologyType)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.post(TECHNOLOGY_TYPES_API_BASE_URL, technologyType, config);
    }

    getTechnologyTypeById(technologyTypeId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.get(`${TECHNOLOGY_TYPES_API_BASE_URL}/${technologyTypeId}`, config);
    }

    updateTechnologyType(technologyType, technologyTypeId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.put(`${TECHNOLOGY_TYPES_API_BASE_URL}/${technologyTypeId}`, technologyType, config);
    }

    deleteTechnologyType(technologyTypeId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.delete(`${TECHNOLOGY_TYPES_API_BASE_URL}/${technologyTypeId}`, config);
    }
}

export default new TechnologyTypeService();