import axios from 'axios';

const TECHNOLOGY_TYPE_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/technology-types";

class TechnologyTypeService {

    getTechnologyTypes(){
        return axios.get(TECHNOLOGY_TYPE_API_BASE_URL);
    }

    createTechnologyType(technologyType){
        return axios.post(TECHNOLOGY_TYPE_API_BASE_URL, technologyType);
    }

    getTechnologyTypeById(technologyTypeId){
        return axios.get(TECHNOLOGY_TYPE_API_BASE_URL + '/' + technologyTypeId);
    }

    updateTechnologyType(technologyType, technologyTypeId){
        return axios.put(TECHNOLOGY_TYPE_API_BASE_URL + '/' + technologyTypeId, technologyType);
    }

    deleteTechnologyType(technologyTypeId){
        return axios.delete(TECHNOLOGY_TYPE_API_BASE_URL + '/' + technologyTypeId);
    }
}

export default new TechnologyTypeService()