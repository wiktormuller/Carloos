import axios from 'axios';

const COUNTRIES_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/countries";

class CountryService {

    getCountries(){
        return axios.get(COUNTRIES_API_BASE_URL);
    }

    createCountry(country){
        return axios.post(COUNTRIES_API_BASE_URL, country);
    }

    getCountryById(countryId){
        return axios.get(COUNTRIES_API_BASE_URL + '/' + countryId);
    }

    updateCountry(country, countryId){
        return axios.put(COUNTRIES_API_BASE_URL + '/' + countryId, country);
    }

    deleteCountry(countryId){
        return axios.delete(COUNTRIES_API_BASE_URL + '/' + countryId);
    }
}

export default new CountryService();