import '../styles/main-styles.css';
import { useState } from "react";
import { InputData } from "./InputData";
import { FilterData } from "./FilterData";
import { AdvertData } from './AdvertData';
import { localizationArray, skillsArray, adsArray } from "./../data/arrays"

export const Data = () => {

  const [searchedInput, setSearchedInput] = useState("");
  const [searchedLocalization, setSearchedLocalization] = useState("");
  const [searchedSkills, setSearchedSkills] = useState([]);

  let searchedAds = adsArray;
  let filteredData = [];
  if(searchedLocalization !== 0){
    console.log(searchedLocalization)
  }
  if(searchedLocalization === 0){
    console.log(searchedLocalization)    
  }
  if(searchedInput !== ""){
    filteredData = adsArray.filter((ad) => {
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
  if(searchedInput === ""){
    searchedAds = adsArray;
  }
  if(searchedSkills.length > 0){
    let checker = (arr, target) => target.every(v => arr.includes(v));
    filteredData = adsArray.filter(ad => checker(ad.skills,searchedSkills) === true);
    searchedAds = filteredData;
  }
  

  return (
    <div className="data">
      <InputData localizationArray={localizationArray} setSearchedInput={setSearchedInput} setSearchedLocalization={setSearchedLocalization}></InputData>
      <FilterData skillsArray={skillsArray} searchedSkills={searchedSkills} setSearchedSkills={setSearchedSkills}></FilterData>
      <AdvertData localizationArray={localizationArray} skillsArray={skillsArray} adsArray={searchedAds}></AdvertData>
    </div>
    );
}