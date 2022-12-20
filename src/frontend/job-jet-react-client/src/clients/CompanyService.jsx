import axios from 'axios';
import Environment from './Environment';

const COMPANIES_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/companies`;

class CompanyService {

    getCompanies(){
        return axios.get(COMPANIES_API_BASE_URL);
    }

    createCompany(company){
        return axios.post(COMPANIES_API_BASE_URL, company);
    }

    getCompanyById(companyId){
        return axios.get(`${COMPANIES_API_BASE_URL}/companyId`);
    }

    updateCompany(company, companyId){
        return axios.put(`${COMPANIES_API_BASE_URL}/${companyId}`, company);
    }

    deleteCompany(companyId){
        return axios.delete(`${COMPANIES_API_BASE_URL}/${companyId}`);
    }
}

export default new CompanyService();