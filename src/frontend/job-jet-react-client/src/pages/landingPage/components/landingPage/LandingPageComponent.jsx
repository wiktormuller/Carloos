//import JobOffersSummary from "../landingPage/JobOffersSummary";
import Map from "../map/MapComponent"
import JobOfferService from "../../../jobOffer/services/JobOfferService";
import { useEffect, useState } from 'react';

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
    <div>
      {/* <JobOffersSummary
          localizationArray={props.localizationArray}
          skillsArray={props.skillsArray}
          jobOffersArray={props.jobOffersArray}
          searchedSkills={props.searchedSkills}
          setSearchedInput={props.setSearchedInput}
          setSearchedLocalization={props.setSearchedLocalization}
          setSearchedSkills={props.setSearchedSkills}
          setAdvertDetails={props.setAdvertDetails}
          technologyTypes={props.technologyTypes} /> */}

      <Map 
        jobOffers={jobOffers}
        userGeoLocation={userGeoLocation}
        selectedJobOfferGeoLocation={selectedJobOfferGeoLocation}
      />
    </div>
  );
}