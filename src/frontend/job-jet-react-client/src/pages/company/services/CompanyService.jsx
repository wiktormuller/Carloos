import axios from 'axios';

const COMPANY_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/companies";

class CompanyService {

    getCompanies(){
        return axios.get(COMPANY_API_BASE_URL);
    }

    createCompany(company){
        return axios.post(COMPANY_API_BASE_URL, company);
    }

    getCompanyById(companyId){
        return axios.get(COMPANY_API_BASE_URL + '/' + companyId);
    }

    updateCompany(company, companyId){
        return axios.put(COMPANY_API_BASE_URL + '/' + companyId, company);
    }

    deleteCompany(companyId){
        return axios.delete(COMPANY_API_BASE_URL + '/' + companyId);
    }
}

export default new CompanyService()