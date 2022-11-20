import axios from 'axios';

const JOB_OFFERS_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/job-offers";

class JobOfferService {

    getJobOffers(){
        return axios.get(JOB_OFFERS_API_BASE_URL);
    }

    createJobOffer(jobOffer){
        return axios.post(JOB_OFFERS_API_BASE_URL, jobOffer);
    }

    getJobOfferById(jobOfferId){
        return axios.get(JOB_OFFERS_API_BASE_URL + '/' + jobOfferId);
    }

    updateJobOffer(jobOffer, jobOfferId){
        return axios.put(JOB_OFFERS_API_BASE_URL + '/' + jobOfferId, jobOffer);
    }

    deleteJobOffer(jobOfferId){
        return axios.delete(JOB_OFFERS_API_BASE_URL + '/' + jobOfferId);
    }
}

export default new JobOfferService();