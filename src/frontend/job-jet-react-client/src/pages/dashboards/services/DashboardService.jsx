import axios from 'axios';

const COMPANY_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/dashboards";

class DashboardService {

    getAverageSalariesForTechnologies(){
        return axios.get(COMPANY_API_BASE_URL + '/' + 'average-salaries-for-technologies');
    }

    getAverageSalariesForCountries(){
        return axios.get(COMPANY_API_BASE_URL + '/' + 'average-salaries-for-countries');
    }

    getAverageSalariesForSeniorityLevels(companyId){
        return axios.get(COMPANY_API_BASE_URL + '/' + 'average-salaries-for-seniority-levels');
    }
}

export default new DashboardService()