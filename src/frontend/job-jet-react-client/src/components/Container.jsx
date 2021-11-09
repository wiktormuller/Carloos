import '../styles/main-styles.css';
import { Header } from "./Header.jsx";
import { Data } from  "./Data.jsx";
import { Map } from  "./Map.jsx";
// import { useState /*, useEffect*/ } from "react";

export const Container = (props) => {
    return (
      <div className="main-container">
        <Header
          userLogInState={props.userLogInState}
          setUserLogInState={props.setUserLogInState}
        ></Header>
        <div className="sub-container">
          <Data localizationArray={props.localizationArray} 
                skillsArray={props.skillsArray}
                adsArray={props.adsArray}
                searchedSkills={props.searchedSkills} 
                setSearchedInput={props.setSearchedInput} 
                setSearchedLocalization={props.setSearchedLocalization}
                setSearchedSkills={props.setSearchedSkills}
          ></Data>
          <Map localizationArray={props.localizationArray} adsArray={props.adsArray}></Map>
        </div>
      </div>
      );
}