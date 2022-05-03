import "./styles/main-styles.css";
import { useState, useEffect } from "react";
import { Navbar } from "./components/navbar/Navbar.jsx";
import { LandingPage } from "./components/LandingPage.jsx";
import { localizationArray, skillsArray, adsArray } from "./data/arrays";

function App() {
  /* INTEGRACJA Z API */

  let url = `https://jobjet.azurewebsites.net/api/v1/countries/`;
  let [countries, setCountries] = useState([]);
  useEffect(() => {
    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setCountries(data);
      });
  }, [url]);

  // Domyślnie countries zostanie przekazana do localizationArray. Najpierw muszą jednak zostać dodane własności lat, lng, zoom
  //console.log(countries);

  /* KONIEC INTEGRACJI Z API */

  const [advertLocation, setAdvertLocation] = useState({});

  const [userLogInState, setUserLogInState] = useState(false);
  const [searchedInput, setSearchedInput] = useState("");
  const [searchedLocalization, setSearchedLocalization] = useState("1");
  const [searchedSkills, setSearchedSkills] = useState([]);

  let searchedAds;
  let searchedAdsByLocalization;
  let searchedAdsClone;
  let filteredData = [];
  if (searchedLocalization !== "1") {
    filteredData = adsArray.filter(
      (ad) => String(ad.localization) === searchedLocalization
    );
    searchedAds = filteredData;
  }
  if (searchedLocalization === "1") {
    searchedAds = adsArray;
  }
  searchedAdsByLocalization = [...searchedAds];
  if (searchedSkills.length > 0) {
    let checker = (arr, target) => target.every((v) => arr.includes(v));
    filteredData = searchedAdsByLocalization.filter(
      (ad) => checker(ad.skills, searchedSkills) === true
    );
    searchedAds = filteredData;
  }
  searchedAdsClone = [...searchedAds];
  if (searchedInput !== "") {
    filteredData = searchedAds.filter((ad) => {
      return (
        ad.title
          .toLowerCase()
          .trim()
          .includes(searchedInput.toLowerCase().trim()) ||
        ad.description
          .toLowerCase()
          .trim()
          .includes(searchedInput.toLowerCase().trim())
      );
    });
    searchedAds = filteredData;
  }
  if (searchedInput === "") {
    searchedAds = searchedAdsClone;
  }

  return (
    <div className="app">
      <Navbar></Navbar>
      <LandingPage
        userLogInState={userLogInState}
        setUserLogInState={setUserLogInState}
        localizationArray={localizationArray}
        skillsArray={skillsArray}
        adsArray={searchedAds}
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
