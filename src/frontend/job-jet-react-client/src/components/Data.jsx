import '../styles/main-styles.css';
import { useState } from "react";
import { InputData } from "./InputData";
import { FilterData } from "./FilterData";
import { AdvertData } from './AdvertData';

export const Data = () => {

  const localizationArray = [{id:1, name:"Szukaj wszędzie"},
    {id:2, name:"Gdańsk"}, {id:3, name:"Gdynia"}, {id:4, name:"Sopot"}, 
    {id:5, name:"Warszawa"}, {id:6, name:"Łódź"}, {id:7, name:"Poznań"}, 
    {id:8, name:"Wrocław"}, {id:9, name:"Kraków"}, {id:10, name:"Szczecin"}
  ]

  const skillsArray = [
    {id:1, name:"HTML5", icon:"DiHtml5", color:"orange"}, {id:2, name:"CSS3", icon:"DiCss3", color:"#4169e1"}, {id:3, name:"JavaScript", icon:"DiJavascript", color:"#f5c71a"},{id:4, name:"TypeScript", icon:"SiTypescript", color:"#1e90ff"}, {id:5, name:"PHP", icon:"DiPhp", color:"#9370db"}, 
    {id:6, name:"C/C++", icon:"BiPlusCircle", color:"#9400d3"},{id:7, name:"C#", icon:"BsHash", color:"#228b22"},{id:8, name:"Java", icon:"DiJava", color:"#ff0000"}, {id:9, name:"Python", icon:"DiPython", color:"#555d50"}, {id:10, name:"Ruby", icon:"DiRuby", color:"#9b111e"},
    {id:11, name:"Mobile", icon:"BiMobile", color:"#3d0c02"},{id:12, name:"Game", icon:"BiGame", color:"orange"},{id:13, name:"Testing", icon:"BiTestTube", color:"#8bbe1b"}, {id:14, name:"Analytics", icon:"MdAnalytics", color:"#273be2"}, {id:15, name:"UX/UI", icon:"BiPaint", color:"#757575"},
    {id:16, name:"DevOps", icon:"SiAzuredevops", color:"#556b2f"},{id:17, name:"Admin", icon:"DiLinux", color:"black"},{id:18, name:"Security", icon:"MdSecurity", color:"#4166f5"}, {id:19, name:"Data", icon:"DiDatabase", color:"#ea3c53"}, {id:20, name:"Inne", icon:"RiPsychotherapyLine", color:"orange"}
  ];

  const adsArray = [
    {id:1, title:"Junior Web Developer Trójmiasto", localization:1, salary:"5000-7500 PLN", date:"20-10-2021", description:"Poszukiwany Junior Web Developer do pracy. Po okresie próbnym możliwa praca zdalna", skills:[1,2,3]}
    ,{id:2, title:"Senior security specialist", localization:4, salary:"15000-25000 PLN", date:"28-10-2021", description:"Poszukujemy specjalisty w dziedzinie bezpieczeństwa IT. Oferujemy bardzo atrakcyjne warunki pracy.", skills:[8,17,18,20]}
  ];

  const [searchedAds, setSearchedAds] = useState(adsArray);
  const [searchedInput, setSearchedInput] = useState("");
  const [searchedLocalization, setSearchedLocalization] = useState("");

  const filterData = () => {
    if(searchedInput !== ""){
      const filteredData = adsArray.filter((ad) => {
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
      setSearchedAds(filteredData);
    }
    if(searchedInput === ""){
      setSearchedAds(adsArray);
    }
    if(searchedLocalization !== 0){
      console.log(searchedLocalization)
    }
    if(searchedLocalization !== 0){
      console.log(searchedLocalization)    
    }
  };

  return (
    <div className="data">
      <InputData localizationArray={localizationArray} setSearchedInput={setSearchedInput} setSearchedLocalization={setSearchedLocalization} filterData={filterData}></InputData>
      <FilterData skillsArray={skillsArray}></FilterData>
      <AdvertData localizationArray={localizationArray} skillsArray={skillsArray} adsArray={searchedAds}></AdvertData>
    </div>
    );
}