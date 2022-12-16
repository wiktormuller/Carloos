import axios from 'axios';

const JOB_OFFERS_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/job-offers";

class JobOfferService {

    getJobOffers(searchText, selectedSeniorityLevelId, selectedWorkSpecification, selectedEmploymentTypeId, selectedTechnologyTypesId){
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

    getJobOfferApplications(jobOfferId)
    {
        return axios.get(JOB_OFFERS_API_BASE_URL + '/' + jobOfferId + '/' + 'offer-applications');
    }

    getJobOfferApplicationFile(jobOfferId, jobOfferApplicationId)
    {
        return axios.get(JOB_OFFERS_API_BASE_URL + '/' + jobOfferId + '/' + 'offer-applications' + '/' + jobOfferApplicationId);
    }

    sendJobOfferApplication(jobOfferId, userEmail, phoneNumber, file)
    {
        var formData = new FormData();
        formData.append("jobOfferApplication", file)
        formData.append('UserEmail', userEmail);
        formData.append('PhoneNumber', phoneNumber);

        return axios.post(JOB_OFFERS_API_BASE_URL + '/' + jobOfferId + '/' + 'offer-applications', formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
              },
        });
    }
}

export default new JobOfferService();