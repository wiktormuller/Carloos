import axios from 'axios';
import Environment from './Environment';
import LoginService from './LoginService';

const EMPLOYMENT_TYPES_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/employment-types`;

class EmploymentTypeService {

    getEmploymentTypes(){
        return axios.get(EMPLOYMENT_TYPES_API_BASE_URL);
    }

    createEmploymentType(employmentType)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.post(EMPLOYMENT_TYPES_API_BASE_URL, employmentType, config);
    }

    getEmploymentTypeById(employmentTypeId)
    {
        return axios.get(`${EMPLOYMENT_TYPES_API_BASE_URL}/${employmentTypeId}`);
    }

    updateEmploymentType(employmentType, employmentTypeId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.put(`${EMPLOYMENT_TYPES_API_BASE_URL}/${employmentTypeId}`, employmentType, config);
    }

    deleteEmploymentType(employmentTypeId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.delete(`${EMPLOYMENT_TYPES_API_BASE_URL}/${employmentTypeId}`, config);
    }
}

export default new EmploymentTypeService();