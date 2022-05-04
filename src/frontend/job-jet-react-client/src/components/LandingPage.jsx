import "../styles/main-styles.css";
import { useState /*, useEffect*/ } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Header } from "./header/Header.jsx";
import { Data } from "./data/Data.jsx";
import { AdvertDetails } from "./advertDetails/AdvertDetails.jsx";
import { Map } from "./map/Map.jsx";
import { RegistrationPanel } from "./registrationPanel/RegistrationPanel";
import { LoginPanel } from "./loginPanel/LoginPanel";

export const LandingPage = (props) => {
  const [advertDetails, setAdvertDetails] = useState({});
  const [geoLocation, setGeoLocation] = useState({});
  const [advertLocation, setAdvertLocation] = useState({});
  return (
    <div className="main-container">
      <Header
        userLogInState={props.userLogInState}
        setUserLogInState={props.setUserLogInState}
        setGeoLocation={setGeoLocation}
        setAdvertLocation={setAdvertLocation}
      ></Header>
      <div className="sub-container">
        <Routes>
          <Route
            path="/"
            element={
              <Data
                localizationArray={props.localizationArray}
                skillsArray={props.skillsArray}
                jobOffersArray={props.jobOffersArray}
                searchedSkills={props.searchedSkills}
                setSearchedInput={props.setSearchedInput}
                setSearchedLocalization={props.setSearchedLocalization}
                setSearchedSkills={props.setSearchedSkills}
                setAdvertDetails={setAdvertDetails}
                setAdvertLocation={setAdvertLocation}
              />
            }
          />
          <Route
            path="/details"
            element={
              <AdvertDetails
                advertDetails={advertDetails}
                setGeoLocation={setGeoLocation}
              />
            }
          />
          <Route path="/register" element={<RegistrationPanel />} />
          <Route path="/login" element={<LoginPanel />} />
        </Routes>

        <Map
          localizationArray={props.localizationArray}
          jobOffersArray={props.jobOffersArray}
          geoLocation={geoLocation}
          advertLocation={advertLocation}
        ></Map>
      </div>
    </div>
  );
};
