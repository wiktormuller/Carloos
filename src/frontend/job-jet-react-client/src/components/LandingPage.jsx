import "../styles/main-styles.css";
import { useState /*, useEffect*/ } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Header } from "./header/Header.jsx";
import { Data } from "./data/Data.jsx";
import { AdvertDetails } from "./advertDetails/AdvertDetails.jsx";
import { Map } from "./map/Map.jsx";

export const LandingPage = (props) => {
  const [advertDetails, setAdvertDetails] = useState({});
  const [geoLocation, setGeoLocation] = useState({});
  const [advertLocation, setAdvertLocation] = useState({});
  return (
    <div className="main-container">
      <Router>
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
                  adsArray={props.adsArray}
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
          </Routes>

          <Map
            localizationArray={props.localizationArray}
            adsArray={props.adsArray}
            geoLocation={geoLocation}
            advertLocation={advertLocation}
          ></Map>
        </div>
      </Router>
    </div>
  );
};
