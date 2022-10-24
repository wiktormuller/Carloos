import "./styles/main-styles.css";
import { useState, useEffect } from "react";
import { Navbar } from "./components/navbar/Navbar.jsx";
import { LandingPage } from "./components/LandingPage.jsx";
import { localizationArray, skillsArray } from "./data/arrays";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  const url = `https://jobjet.azurewebsites.net/api/v1`;

  const [userLogInState, setUserLogInState] = useState(false);
  const [advertLocation, setAdvertLocation] = useState({});
  const [searchedInput, setSearchedInput] = useState("");
  const [searchedLocalization, setSearchedLocalization] = useState("6");
  const [searchedSkills, setSearchedSkills] = useState([]);
  let [jobOffers, setJobOffers] = useState([]);
  const [jobOffersUrl, setJobOffersUrl] = useState(url + `/job-offers/`);

  useEffect(() => {
    if (searchedInput !== "") {
      setJobOffersUrl(
        url +
          `/job-offers?CountryId=${searchedLocalization}&GeneralSearchByText=${searchedInput}`
      );
    } else if (searchedInput === "") {
      setJobOffersUrl(url + `/job-offers/?CountryId=${searchedLocalization}`);
    }
  }, [searchedInput, searchedLocalization]);

  useEffect(() => {
    fetch(jobOffersUrl)
      .then((response) => response.json())
      .then((data) => {
        setJobOffers(data.response.data);
      });
  }, [jobOffersUrl]);

  console.log(userLogInState);

  return (
    <div className="app">
      <Router>
        <Navbar userLogInState={userLogInState}></Navbar>
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
