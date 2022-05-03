import "./styles/data-styles.css";
import { InputData } from "./subComponents/InputData";
import { FilterData } from "./subComponents/FilterData";
import { AdvertData } from "./subComponents/AdvertData";

export const Data = (props) => {
  return (
    <div className="data">
      <InputData
        localizationArray={props.localizationArray}
        setSearchedInput={props.setSearchedInput}
        setSearchedLocalization={props.setSearchedLocalization}
        setAdvertLocation={props.setAdvertLocation}
      ></InputData>
      <FilterData
        skillsArray={props.skillsArray}
        searchedSkills={props.searchedSkills}
        setSearchedSkills={props.setSearchedSkills}
      ></FilterData>
      <AdvertData
        localizationArray={props.localizationArray}
        skillsArray={props.skillsArray}
        jobOffersArray={props.jobOffersArray}
        setAdvertDetails={props.setAdvertDetails}
        setAdvertLocation={props.setAdvertLocation}
      ></AdvertData>
    </div>
  );
};
