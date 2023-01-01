import axios from 'axios';
import Environment from './Environment';
import LoginService from './LoginService';

const CURRENCIES_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/currencies`;

class CurrencyService {

    getCurrencies(){
        return axios.get(CURRENCIES_API_BASE_URL);
    }

    createCurrency(currency)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.post(CURRENCIES_API_BASE_URL, currency, config);
    }

    getCurrencyById(technologyTypeId){
        return axios.get(`${CURRENCIES_API_BASE_URL}/${technologyTypeId}`);
    }

    updateCurrency(currency, currencyId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.put(`${CURRENCIES_API_BASE_URL}/${currencyId}`, currency, config);
    }

    deleteCurrency(currencyId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.delete(`${CURRENCIES_API_BASE_URL}/${currencyId}`, config);
    }
}

export default new CurrencyService();