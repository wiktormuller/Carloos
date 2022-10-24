import "./styles/data-styles.css";
import { InputData } from "./subComponents/InputData";
import { FilterData } from "./subComponents/FilterData";
import { AdvertData } from "./subComponents/AdvertData";
import { Map } from "./../map/Map.jsx";
import 'leaflet/dist/leaflet.css';

export const Data = (props) => {
  return (
    <div className="data">
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

      <Map
        localizationArray={props.localizationArray}
        jobOffersArray={props.jobOffersArray}
        geoLocation={props.geoLocation}
        advertLocation={props.advertLocation}
      />

      <AdvertData
        localizationArray={props.localizationArray}
        skillsArray={props.skillsArray}
        jobOffersArray={props.jobOffersArray}
        setAdvertDetails={props.setAdvertDetails}
        setAdvertLocation={props.setAdvertLocation}
      />
    </div>
  );
};
