import axios from 'axios';
import Environment from './Environment';

const CURRENCIES_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/currencies`;

class CurrencyService {

    getCurrencies(){
        return axios.get(CURRENCIES_API_BASE_URL);
    }

    createCurrency(currency){
        return axios.post(CURRENCIES_API_BASE_URL, currency);
    }

    getCurrencyById(technologyTypeId){
        return axios.get(`${CURRENCIES_API_BASE_URL}/${technologyTypeId}`);
    }

    updateCurrency(currency, currencyId){
        return axios.put(`${CURRENCIES_API_BASE_URL}/${currencyId}`, currency);
    }

    deleteCurrency(currencyId){
        return axios.delete(`${CURRENCIES_API_BASE_URL}/${currencyId}`);
    }
}

export default new CurrencyService();