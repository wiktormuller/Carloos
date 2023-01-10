import MapComponent from "../landingPage/map/MapComponent";
import JobOffersListComponent from '../landingPage/jobOffersList/JobOffersListComponent';
import SearchBarComponent from '../landingPage/searchBar/SearchBarComponent';
import JobOfferService from "../../../../clients/JobOfferService";
import { useEffect, useState } from 'react';
import './landing-page-styles.css';

export default function LandingPageComponent()
{
  const [jobOffers, setJobOffers] = useState([]);

  const [userGeoLocation, setUserGeolocation] = useState(
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
  const [selectedRadiusInKilometers, setSelectedRadiusInKilometers] = useState();

  function setSearchTextProxy(event) {
    setSearchText(event.value);
  }

  function setSelectedRadiusInKilometersProxy(event) {
    console.log(event.value);
    setSelectedRadiusInKilometers(event.value);
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
    if (navigator.geolocation)
    {
        navigator.geolocation.getCurrentPosition(position => {
            setUserGeolocation({
                longitude: position.coords.longitude,
                latitude: position.coords.latitude
            });
        });
    }

    JobOfferService.getJobOffers(searchText, selectedSeniorityLevelId, selectedWorkSpecification, selectedEmploymentTypeId, selectedTechnologyTypesId, userGeoLocation, selectedRadiusInKilometers)
    .then(res => {
      setJobOffers(res.data.response.data);
    });
  }, [searchText, selectedSeniorityLevelId, selectedWorkSpecification, selectedEmploymentTypeId, selectedTechnologyTypesId, selectedRadiusInKilometers]);

  return (
    <div className="landing-page">
      <SearchBarComponent 
        setSearchText={setSearchTextProxy}
        setSelectedSeniorityLevel={setSelectedSeniorityLevelProxy}
        setSelectedWorkSpecification={setSelectedWorkSpecificationProxy}
        setSelectedTechnologyType={setSelectedTechnologyTypeProxy}
        setSelectedEmploymentType={setSelectedEmploymentTypeProxy}
        setSelectedRadiusInKilometers={setSelectedRadiusInKilometersProxy}

        selectedSeniorityLevelId={selectedSeniorityLevelId}
        selectedWorkSpecification={selectedWorkSpecification}
        selectedEmploymentTypeId={selectedEmploymentTypeId}
        selectedTechnologyTypesId={selectedTechnologyTypesId}
        searchText={searchText}
        selectedRadiusInKilometers={selectedRadiusInKilometers}
        />

      <div className="job-offers-with-map">
        <JobOffersListComponent
          jobOffers={jobOffers}
        />

        <MapComponent
          jobOffers={jobOffers}
          userGeoLocation={userGeoLocation}
          radiusInKilometers={selectedRadiusInKilometers}
        />
      </div>
    </div>
  );
}