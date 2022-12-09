import React, { useState, useEffect } from 'react';
import JobOfferService from '../../../clients/JobOfferService';
import { useParams } from 'react-router-dom';
import '../view-job-offer-styles.css';
import MapComponent from '../../landingPage/components/map/MapComponent';
import JobOfferDetailsComponent from './JobOfferDetailsComponent';

export default function ViewJobOfferComponent(props) // TODO: Select job offer geolocation and list of job offers to render map
{
    let { id } = useParams();
    const [jobOffers, setJobOffers] = useState([]);

    // TODO: To change
    const [selectedJobOfferGeoLocation, setSelectedJobOfferGeoLocation] = useState(
        {
          longitude: undefined,
          latitude: undefined
        }
    );

    // TODO: To change
    const [userGeoLocation, setUserGeolocation] = useState(
        {
          longitude: undefined,
          latitude: undefined
        }
    );

      // Similar to componentDidMount and componentDidUpdate
  useEffect(() => {
    JobOfferService.getJobOffers().then(res => {
      setJobOffers(res.data.response.data);
    });
  }, []);

    const [jobOffer, setJobOffer] = useState({
        id: 0,
        name: '',
        description: '',
        salaryFrom: 0,
        salaryTo: 0,
        address: {
            countryName: '',
            town: '',
            street: '',
            zipCode: ''
        },
        technologyTypes: [],
        seniority: '',
        employmentType: '',
        workSpecification: '',
        createdAt: '',
        companyName: ''
    });

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        JobOfferService.getJobOfferById(id).then((res) => {
            let jobOfferResponse = res.data;
            setJobOffer({
                id: jobOfferResponse.id,
                name: jobOfferResponse.name,
                description: jobOfferResponse.description,
                salaryFrom: jobOfferResponse.salaryFrom,
                salaryTo: jobOfferResponse.salaryTo,
                address: {
                    countryName: jobOfferResponse.address.countryName,
                    town: jobOfferResponse.address.town,
                    street: jobOfferResponse.address.street,
                    zipCode: jobOfferResponse.address.zipCode
                },
                technologyTypes: jobOfferResponse.technologyTypes,
                seniority: jobOfferResponse.seniority,
                employmentType: jobOfferResponse.employmentType,
                workSpecification: jobOfferResponse.workSpecification,
                createdAt: jobOfferResponse.createdAt,
                companyName: jobOfferResponse.companyName
            });
        });
    }, []);

    return (
        <div className="view-job-offer">
            <JobOfferDetailsComponent jobOffer={jobOffer} />
            <MapComponent jobOffers={jobOffers} 
                userGeoLocation={userGeoLocation} 
                selectedJobOfferGeoLocation={selectedJobOfferGeoLocation}/>
        </div>
    );
}