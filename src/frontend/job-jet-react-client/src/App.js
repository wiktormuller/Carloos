import "./styles/main-styles.css";
import { useState, useEffect } from "react";
import { Navbar } from "./components/navbar/Navbar.jsx";
import { LandingPage } from "./components/LandingPage.jsx";
import { localizationArray, skillsArray } from "./data/arrays";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
  const url = `https://jobjet.azurewebsites.net/api/v1`;
  // let countriesUrl = url + `/countries/`;

  const [userLogInState, setUserLogInState] = useState(false);
  const [token, setToken] = useState("");
  const [advertLocation, setAdvertLocation] = useState({});
  const [searchedInput, setSearchedInput] = useState("");
  const [searchedLocalization, setSearchedLocalization] = useState("6");
  const [searchedSkills, setSearchedSkills] = useState([]);
  const [jobOffers, setJobOffers] = useState([]);
  const [jobOffersUrl, setJobOffersUrl] = useState(url + `/job-offers/`);

  const [companies, setCompanies] = useState([]);
  const [companiesUrl, setCompaniesUrl] = useState(url + `/companies/`);

  const [currencies, setCurrencies] = useState([]);
  const [currenciesUrl, setCurrenciesUrl] = useState(url + `/currencies/`);

  const [seniority, setSeniority] = useState([]);
  const [seniorityUrl, setSeniorityUrl] = useState(url + `/seniority-levels/`);

  const [employmentType, setEmploymentType] = useState([]);
  const [employmentTypeUrl, setEmploymentTypeUrl] = useState(
    url + `/employment-types/`
  );

  // const [countries, setCountries] = useState([]);
  // const [countriesUrl, setCountriesUrl] = useState(url + `/countries/`);

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

  useEffect(() => {
    fetch(companiesUrl)
      .then((response) => response.json())
      .then((data) => {
        setCompanies(data.response.data);
      });
  }, [companiesUrl]);

  useEffect(() => {
    fetch(currenciesUrl)
      .then((response) => response.json())
      .then((data) => {
        setCurrencies(data);
      });
  }, [currenciesUrl]);

  useEffect(() => {
    fetch(seniorityUrl)
      .then((response) => response.json())
      .then((data) => {
        setSeniority(data);
      });
  }, [seniorityUrl]);

  useEffect(() => {
    fetch(employmentTypeUrl)
      .then((response) => response.json())
      .then((data) => {
        setEmploymentType(data);
      });
  }, [employmentTypeUrl]);

  // useEffect(() => {
  //   fetch(countriesUrl)
  //     .then((response) => response.json())
  //     .then((data) => {
  //       setCountries(data);
  //     });
  // }, [countriesUrl]);
  // Domyślnie countries zostanie przekazana do localizationArray. Najpierw muszą jednak zostać dodane własności lat, lng, zoom
  // console.log(countries);

  return (
    <div className="app">
      <Router>
        <Navbar userLogInState={userLogInState}></Navbar>
        <LandingPage
          userLogInState={userLogInState}
          setUserLogInState={setUserLogInState}
          token={token}
          setToken={setToken}
          localizationArray={localizationArray}
          skillsArray={skillsArray}
          jobOffersArray={jobOffers}
          searchedSkills={searchedSkills}
          setSearchedInput={setSearchedInput}
          setSearchedLocalization={setSearchedLocalization}
          setSearchedSkills={setSearchedSkills}
          advertLocation={advertLocation}
          setAdvertLocation={setAdvertLocation}
          companies={companies}
          currencies={currencies}
          seniority={seniority}
          employmentType={employmentType}
        ></LandingPage>
      </Router>
    </div>
  );
}

export default App;
