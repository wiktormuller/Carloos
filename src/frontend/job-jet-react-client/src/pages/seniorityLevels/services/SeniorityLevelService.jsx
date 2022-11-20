import axios from 'axios';

const SENIORITY_LEVELS_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/seniority-levels";

class SeniorityLevelService {

    getSeniorityLevels(){
        return axios.get(SENIORITY_LEVELS_API_BASE_URL);
    }

    createSeniorityLevel(seniorityLevel){
        return axios.post(SENIORITY_LEVELS_API_BASE_URL, seniorityLevel);
    }

    getSeniorityLevelById(seniorityLevelId){
        return axios.get(SENIORITY_LEVELS_API_BASE_URL + '/' + seniorityLevelId);
    }

    updateSeniorityLevel(seniorityLevel, seniorityLevelId){
        return axios.put(SENIORITY_LEVELS_API_BASE_URL + '/' + seniorityLevelId, seniorityLevel);
    }

    deleteSeniorityLevel(seniorityLevelId){
        return axios.delete(SENIORITY_LEVELS_API_BASE_URL + '/' + seniorityLevelId);
    }
}

export default new SeniorityLevelService();