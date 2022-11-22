import MapComponent from "../map/MapComponent"
import JobOffersListComponent from '../jobOffersList/JobOffersListComponent';
import JobOfferService from "../../../jobOffer/services/JobOfferService";
import { useEffect, useState } from 'react';
import './landing-page-styles.css';
export default function LandingPageComponent()
{
  const [jobOffers, setJobOffers] = useState([]);
  
  // Get it from global context
  const [userGeoLocation, setUserGeolocation] = useState(
    {
      longitude: undefined,
      latitude: undefined
    }
  );

  // Get it from global context
  const [selectedJobOfferGeoLocation, setSelectedJobOfferGeoLocation] = useState(
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
    <div className="landing-page">
      <JobOffersListComponent
        jobOffers={jobOffers}
      />

      <MapComponent
        jobOffers={jobOffers}
        userGeoLocation={userGeoLocation}
        selectedJobOfferGeoLocation={selectedJobOfferGeoLocation}
      />
    </div>
  );
}