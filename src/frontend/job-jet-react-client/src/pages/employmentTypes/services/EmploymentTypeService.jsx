import axios from 'axios';

const EMPLOYMENT_TYPES_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/employment-types";

class EmploymentTypeService {

    getEmploymentTypes(){
        return axios.get(EMPLOYMENT_TYPES_API_BASE_URL);
    }

    createEmploymentType(employmentType){
        return axios.post(EMPLOYMENT_TYPES_API_BASE_URL, employmentType);
    }

    getEmploymentTypeById(employmentTypeId){
        return axios.get(EMPLOYMENT_TYPES_API_BASE_URL + '/' + employmentTypeId);
    }

    updateEmploymentType(employmentType, employmentTypeId){
        return axios.put(EMPLOYMENT_TYPES_API_BASE_URL + '/' + employmentTypeId, employmentType);
    }

    deleteEmploymentType(employmentTypeId){
        return axios.delete(EMPLOYMENT_TYPES_API_BASE_URL + '/' + employmentTypeId);
    }
}

export default new EmploymentTypeService();