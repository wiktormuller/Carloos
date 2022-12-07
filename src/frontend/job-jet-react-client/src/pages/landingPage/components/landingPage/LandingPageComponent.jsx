import MapComponent from "../map/MapComponent"
import JobOffersListComponent from '../jobOffersList/JobOffersListComponent';
import SearchBarComponent from '../searchBar/SearchBarComponent';
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

  // State needed for search bar
  const [searchText, setSearchText] = useState('');
  const [selectedSeniorityLevelId, setSelectedSeniorityLevelId] = useState();
  const [selectedWorkSpecification, setSelectedWorkSpecification] = useState();
  const [selectedEmploymentTypeId, setSelectedEmploymentTypeId] = useState();
  const [selectedTechnologyTypesId, setSelectedTechnologyTypesId] = useState();

  function setSearchTextProxy(event) {
    console.log(event);
    setSearchText(event.value);
  }

  function setSelectedSeniorityLevelProxy(event) {
    setSelectedSeniorityLevelId(event.value);
  }

  function setSelectedWorkSpecificationProxy(event) {
    setSelectedWorkSpecification(event.label);
  }

  function setSelectedTechnologyTypeProxy(event) {
    setSelectedTechnologyTypesId(event.value);
  }

  function setSelectedEmploymentTypeProxy(event) {
    setSelectedEmploymentTypeId(event.value);
  }

  // Similar to componentDidMount and componentDidUpdate
  useEffect(() => {
    JobOfferService.getJobOffers(searchText, selectedSeniorityLevelId, selectedWorkSpecification, selectedEmploymentTypeId, selectedTechnologyTypesId)
    .then(res => {
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
  }, [searchText, selectedSeniorityLevelId, selectedWorkSpecification, selectedEmploymentTypeId, selectedTechnologyTypesId]);

  return (
    <div>
      <SearchBarComponent 
        setSearchText={setSearchTextProxy}
        setSelectedSeniorityLevel={setSelectedSeniorityLevelProxy}
        setSelectedWorkSpecification={setSelectedWorkSpecificationProxy}
        setSelectedTechnologyType={setSelectedTechnologyTypeProxy}
        setSelectedEmploymentType={setSelectedEmploymentTypeProxy}
        selectedSeniorityLevelId={selectedSeniorityLevelId}
        selectedWorkSpecification={selectedWorkSpecification}
        selectedEmploymentTypeId={selectedEmploymentTypeId}
        selectedTechnologyTypesId={selectedTechnologyTypesId}
        searchText={searchText}
        />

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
    </div>
  );
}