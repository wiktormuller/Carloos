import axios from 'axios';
import Environment from './Environment';
import LoginService from './LoginService';

const COMPANIES_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/companies`;

class CompanyService
{
    getCompanies(){
        return axios.get(COMPANIES_API_BASE_URL);
    }

    createCompany(company)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.post(COMPANIES_API_BASE_URL, company, config);
    }

    getCompanyById(companyId){
        return axios.get(`${COMPANIES_API_BASE_URL}/${companyId}`);
    }

    updateCompany(company, companyId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.put(`${COMPANIES_API_BASE_URL}/${companyId}`, company, config);
    }

    deleteCompany(companyId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.delete(`${COMPANIES_API_BASE_URL}/${companyId}`, config);
    }
}

export default new CompanyService();