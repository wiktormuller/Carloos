import axios from 'axios';
import Environment from './Environment';
import LoginService from './LoginService';

const JOB_OFFERS_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/job-offers`;

class JobOfferService {

    getJobOffers(searchText, selectedSeniorityLevelId, selectedWorkSpecification, selectedEmploymentTypeId, selectedTechnologyTypesId)
    {
        var query = new URLSearchParams();

        if (searchText !== undefined && searchText !== null && searchText !== '')
        {
            query.append("GeneralSearchByText", searchText);
        }
        
        if (selectedSeniorityLevelId !== undefined && selectedSeniorityLevelId !== 0)
        {
            query.append("SeniorityLevelId", selectedSeniorityLevelId);
        }

        if (selectedWorkSpecification !== undefined && selectedWorkSpecification !== '')
        {
            query.append("WorkSpecification", selectedWorkSpecification);
        }

        if (selectedEmploymentTypeId !== undefined && selectedEmploymentTypeId !== 0)
        {
            query.append("EmploymentTypeId", selectedEmploymentTypeId);
        }

        if (selectedTechnologyTypesId !== undefined && selectedTechnologyTypesId > 0)
        {
            query.append("TechnologyIds", selectedTechnologyTypesId);
        }

        var resultUrl = JOB_OFFERS_API_BASE_URL;
        if (query.toString() !== undefined && query.toString() !== '')
        {
            resultUrl = resultUrl + '?' + query.toString();
        }

        return axios.get(resultUrl);
    }

    createJobOffer(jobOffer)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.post(JOB_OFFERS_API_BASE_URL, jobOffer, config);
    }

    getJobOfferById(jobOfferId){
        return axios.get(`${JOB_OFFERS_API_BASE_URL}/${jobOfferId}`);
    }

    updateJobOffer(jobOffer, jobOfferId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.put(`${JOB_OFFERS_API_BASE_URL}/${jobOfferId}`, jobOffer, config);
    }

    deleteJobOffer(jobOfferId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.delete(`${JOB_OFFERS_API_BASE_URL}/${jobOfferId}`, config);
    }

    getJobOfferApplications(jobOfferId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`
            }
        }

        return axios.get(`${JOB_OFFERS_API_BASE_URL}/${jobOfferId}/offer-applications`, config);
    }

    getJobOfferApplicationFile(jobOfferId, jobOfferApplicationId)
    {
        var config = {
            headers: {
                'Authorization': `Bearer ${LoginService.getAuthenticatedUser().accessToken}`,
                'Content-Type': 'application/octet-stream',
            },
            responseType: 'blob'
        }

        return axios.get(`${JOB_OFFERS_API_BASE_URL}/${jobOfferId}/offer-applications/${jobOfferApplicationId}`, config);
    }

    sendJobOfferApplication(jobOfferId, userEmail, phoneNumber, file)
    {
        var formData = new FormData();
        formData.append("File", file)
        formData.append('UserEmail', userEmail);
        formData.append('PhoneNumber', phoneNumber);

        return axios.post(`${JOB_OFFERS_API_BASE_URL}/${jobOfferId}/offer-applications`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
              },
        });
    }
}

export default new JobOfferService();