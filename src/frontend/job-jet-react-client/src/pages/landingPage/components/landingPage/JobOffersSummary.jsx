import InputData from "../../../data/subComponents/InputData";
import FilterData from "../../../data/subComponents/FilterData";
import AdvertData from "../../../data/subComponents/AdvertData";
import 'leaflet/dist/leaflet.css';

export default function JobOffersSummary(props)
{
  return (
    <div className="data">
      <div className="content-top-section">
        <InputData
          localizationArray={props.localizationArray}
          setSearchedInput={props.setSearchedInput}
          setSearchedLocalization={props.setSearchedLocalization}
          setAdvertLocation={props.setAdvertLocation}
        />

        <FilterData
          skillsArray={props.skillsArray}
          searchedSkills={props.searchedSkills}
          setSearchedSkills={props.setSearchedSkills}
        />
      </div>

      <div className="content-bottom-section">
        <AdvertData
          localizationArray={props.localizationArray}
          skillsArray={props.skillsArray}
          jobOffersArray={props.jobOffersArray}
          setAdvertDetails={props.setAdvertDetails}
          setAdvertLocation={props.setAdvertLocation}
        />
      </div>
    </div>
  );
}