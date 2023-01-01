import axios from 'axios';
import Environment from './Environment';
import LoginService from './LoginService';

const COUNTRIES_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/countries`;

class CountryService
{
    getCountries(){
        return axios.get(COUNTRIES_API_BASE_URL);
    }

    createCountry(country)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.post(COUNTRIES_API_BASE_URL, country, config);
    }

    getCountryById(countryId){
        return axios.get(`${COUNTRIES_API_BASE_URL}/${countryId}`);
    }

    updateCountry(country, countryId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.put(`${COUNTRIES_API_BASE_URL}/${countryId}`, country, config);
    }

    deleteCountry(countryId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.delete(`${COUNTRIES_API_BASE_URL}/${countryId}`, config);
    }
}

export default new CountryService();