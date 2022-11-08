import "./App.css";
import { useState, useEffect } from "react";
import { Navbar } from "./components/navbar/Navbar.jsx";
import { LandingPage } from "./components/LandingPage.jsx";
import { skillsToIconsMap } from "./data/arrays";
import { BrowserRouter as Router } from "react-router-dom";

function App() {
  const baseUrl = `https://jobjet.azurewebsites.net/api/v1`;

  const [userLogInState, setUserLogInState] = useState(false);
  const [token, setToken] = useState("");

  const [advertLocation, setAdvertLocation] = useState({});
  const [searchedInput, setSearchedInput] = useState("");
  const [searchedLocalization, setSearchedLocalization] = useState("6");
  const [searchedSkills, setSearchedSkills] = useState([]);

  const [jobOffers, setJobOffers] = useState([]);
  const [jobOffersUrl, setJobOffersUrl] = useState(
    baseUrl + `/job-offers/`
  );

  const [companies, setCompanies] = useState([]);
  const [companiesUrl, setCompaniesUrl] = useState(
    baseUrl + `/companies/`
  );

  const [currencies, setCurrencies] = useState([]);
  const [currenciesUrl, setCurrenciesUrl] = useState(
    baseUrl + `/currencies/`
  );

  const [seniority, setSeniority] = useState([]);
  const [seniorityUrl, setSeniorityUrl] = useState(
    baseUrl + `/seniority-levels/`
  );

  const [employmentType, setEmploymentType] = useState([]);
  const [employmentTypeUrl, setEmploymentTypeUrl] = useState(
    baseUrl + `/employment-types/`
  );

  const [countries, setCountries] = useState([]);
  const [countriesUrl, setCountriesUrl] = useState(
    baseUrl + `/countries`
  );

  const [technologyTypes, setTechnologyTypes] = useState([]);
  const [technologyTypesUrl, setTechnologyTypesUrl] = useState(
    baseUrl + `/technology-types`
  );

  useEffect(() => {
    if (searchedInput !== "") {
      setJobOffersUrl(
        baseUrl +
          `/job-offers?CountryId=${searchedLocalization}&GeneralSearchByText=${searchedInput}`
      );
    } else if (searchedInput === "") {
      setJobOffersUrl(baseUrl + `/job-offers/?CountryId=${searchedLocalization}`);
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

  useEffect(() => {
    fetch(countriesUrl)
      .then((response) => response.json())
      .then((data) => {
        setCountries(data);
      });
  }, [countriesUrl]);

  useEffect(() => {
    fetch(technologyTypesUrl)
    .then((response) => response.json())
    .then((data) => {
      setTechnologyTypes(data);
    });
  }, [technologyTypesUrl]);

  var technologyTypesWithIcons = [];
  technologyTypesWithIcons = technologyTypes.map(technologyType => (
  {
    id: technologyType.id,
    name: technologyType.name,
    icon: skillsToIconsMap.find((skillsToIcon) => skillsToIcon.id === technologyType.id).icon,
    color: skillsToIconsMap.find((skillsToIcon) => skillsToIcon.id === technologyType.id).color
  }));

  return (
    <div className="app">
      <Router>
        <ListCompaniesComponent></ListCompaniesComponent>
        <Navbar userLogInState={userLogInState}></Navbar>
        <LandingPage
          userLogInState={userLogInState}
          setUserLogInState={setUserLogInState}
          token={token}
          setToken={setToken}
          localizationArray={countries}
          technologyTypes={technologyTypesWithIcons}
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
          countries={countries}
        ></LandingPage>
      </Router>
    </div>
  );
}

export default App;
