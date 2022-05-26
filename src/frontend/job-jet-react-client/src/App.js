import "./styles/main-styles.css";
import { useState, useEffect } from "react";
import { Navbar } from "./components/navbar/Navbar.jsx";
import { LandingPage } from "./components/LandingPage.jsx";
import { localizationArray, jobOffersArray, skillsArray } from "./data/arrays";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  const url = `https://jobjet.azurewebsites.net/api/v1`;
  // let countriesUrl = url + `/countries/`;

  const [userLogInState, setUserLogInState] = useState(false);
  const [advertLocation, setAdvertLocation] = useState({});
  const [searchedInput, setSearchedInput] = useState("");
  const [searchedLocalization, setSearchedLocalization] = useState("6");
  const [searchedSkills, setSearchedSkills] = useState([]);
  let [jobOffers, setJobOffers] = useState([]);
  // let [countries, setCountries] = useState([]);
  const [jobOffersUrl, setJobOffersUrl] = useState(url + `/job-offers/`);

  //------------------------------------------------------------------------------------------------------------------------------
  /* INTEGRACJA Z API */

  console.log(jobOffersUrl);

  useEffect(() => {
    if (
      searchedInput !== "" ||
      searchedLocalization !== "" ||
      searchedSkills !== []
    ) {
      setJobOffersUrl(
        url +
          `/job-offers?CountryId=${searchedLocalization}&TechnologyId=${searchedSkills[0]}&GeneralSearchByText=${searchedInput}`
      );
    } else if (searchedInput === "" && searchedSkills === []) {
      setJobOffersUrl(url + `/job-offers/`);
    }
  }, [searchedInput, searchedLocalization, searchedSkills]);

  // useEffect(() => {
  //   fetch(countriesUrl)
  //     .then((response) => response.json())
  //     .then((data) => {
  //       setCountries(data);
  //     });
  // }, [countriesUrl]);

  useEffect(() => {
    fetch(jobOffersUrl)
      .then((response) => response.json())
      .then((data) => {
        setJobOffers(data.response.data);
      });
  }, [jobOffersUrl]);

  // Domyślnie countries zostanie przekazana do localizationArray. Najpierw muszą jednak zostać dodane własności lat, lng, zoom
  //console.log(countries);

  /* KONIEC INTEGRACJI Z API */
  //------------------------------------------------------------------------------------------------------------------------------

  return (
    <div className="app">
      <Router>
        <Navbar></Navbar>
        <LandingPage
          userLogInState={userLogInState}
          setUserLogInState={setUserLogInState}
          localizationArray={localizationArray}
          skillsArray={skillsArray}
          jobOffersArray={jobOffers}
          searchedSkills={searchedSkills}
          setSearchedInput={setSearchedInput}
          setSearchedLocalization={setSearchedLocalization}
          setSearchedSkills={setSearchedSkills}
          advertLocation={advertLocation}
          setAdvertLocation={setAdvertLocation}
        ></LandingPage>
      </Router>
    </div>
  );
}

export default App;
