import "./styles/main-styles.css";
import { useState, useEffect } from "react";
import { Navbar } from "./components/navbar/Navbar.jsx";
import { LandingPage } from "./components/LandingPage.jsx";
import { localizationArray, jobOffersArray, skillsArray } from "./data/arrays";

function App() {
  //------------------------------------------------------------------------------------------------------------------------------
  /* INTEGRACJA Z API */

  let url = `https://jobjet.azurewebsites.net/api/v1`;
  let countriesUrl = url + `/countries/`;
  let jobOffersUrl = url + `/job-offers/`;

  let [countries, setCountries] = useState([]);
  let [jobOffers, setJobOffers] = useState([]);

  useEffect(() => {
    fetch(countriesUrl)
      .then((response) => response.json())
      .then((data) => {
        setCountries(data);
      });
  }, [countriesUrl]);

  useEffect(() => {
    fetch(jobOffersUrl)
      .then((response) => response.json())
      .then((data) => {
        setJobOffers(data);
      });
  }, [jobOffersUrl]);

  // Domyślnie countries zostanie przekazana do localizationArray. Najpierw muszą jednak zostać dodane własności lat, lng, zoom
  console.log(countries);
  console.log(jobOffers);

  /* KONIEC INTEGRACJI Z API */
  //------------------------------------------------------------------------------------------------------------------------------

  const [userLogInState, setUserLogInState] = useState(false);
  const [advertLocation, setAdvertLocation] = useState({});
  const [searchedInput, setSearchedInput] = useState("");
  const [searchedLocalization, setSearchedLocalization] = useState("1");
  const [searchedSkills, setSearchedSkills] = useState([]);

  return (
    <div className="app">
      <Navbar></Navbar>
      <LandingPage
        userLogInState={userLogInState}
        setUserLogInState={setUserLogInState}
        localizationArray={localizationArray}
        skillsArray={skillsArray}
        jobOffersArray={jobOffersArray}
        searchedSkills={searchedSkills}
        setSearchedInput={setSearchedInput}
        setSearchedLocalization={setSearchedLocalization}
        setSearchedSkills={setSearchedSkills}
        advertLocation={advertLocation}
        setAdvertLocation={setAdvertLocation}
      ></LandingPage>
    </div>
  );
}

export default App;
