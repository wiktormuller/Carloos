import React, { useState, useEffect } from 'react';
import JobOfferService from '../../../clients/JobOfferService';
import { useParams } from 'react-router-dom';
import '../view-job-offer-styles.css';
import MapComponent from '../../landingPage/components/landingPage/map/MapComponent';
import JobOfferDetailsComponent from './JobOfferDetailsComponent';

export default function ViewJobOfferComponent()
{
    let { id } = useParams();
    const [jobOffers, setJobOffers] = useState([]);

    const [selectedJobOfferGeoLocation, setSelectedJobOfferGeoLocation] = useState(
        {
          longitude: undefined,
          latitude: undefined
        }
    );

    const [userGeoLocation, setUserGeolocation] = useState(
        {
        longitude: undefined,
        latitude: undefined
        }
    );

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
            zipCode: '',
            latitude: 0,
            longitude: 0
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

            setSelectedJobOfferGeoLocation({
                longitude: jobOfferResponse.address.longitude,
                latitude: jobOfferResponse.address.latitude
            });
        });

        JobOfferService.getJobOffers().then(res => {
            setJobOffers(res.data.response.data);
        });

        if (navigator.geolocation)
        {
            navigator.geolocation.getCurrentPosition(position => {
                setUserGeolocation({
                    longitude: position.coords.longitude,
                    latitude: position.coords.latitude
                });
            });
        }
    }, []);

    return (
        <div className="view-job-offer">
            <JobOfferDetailsComponent jobOffer={jobOffer} />
            
            {selectedJobOfferGeoLocation.longitude === undefined &&
                <p>Loading Map...</p>
            }

            {selectedJobOfferGeoLocation.longitude !== undefined &&
                <MapComponent jobOffers={jobOffers} 
                userGeoLocation={userGeoLocation} 
                selectedJobOfferGeoLocation={selectedJobOfferGeoLocation}/>
            }
        </div>
    );
}