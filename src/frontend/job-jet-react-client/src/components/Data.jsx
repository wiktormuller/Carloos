import '../styles/main-styles.css';
import { InputData } from "./InputData";
import { FilterData } from "./FilterData";
import { AdvertData } from './AdvertData';



export const Data = () => {

  const localizationArray = [
    {id:1, name:"Gdańsk"}, {id:2, name:"Gdynia"}, {id:3, name:"Sopot"}, 
    {id:4, name:"Warszawa"}, {id:5, name:"Łódź"}, {id:6, name:"Poznań"}, 
    {id:7, name:"Wrocław"}, {id:8, name:"Kraków"}, {id:9, name:"Szczecin"}
  ]

  const skillsArray = [
    {id:1, name:"HTML5"}, {id:2, name:"CSS3"}, {id:3, name:"JavaScript"},{id:4, name:"TypeScript"}, {id:5, name:"PHP"}, 
    {id:6, name:"C/C++"},{id:7, name:"C#"},{id:8, name:"Java"}, {id:9, name:"Python"}, {id:10, name:"Ruby"},
    {id:11, name:"Mobile"},{id:12, name:"Game"},{id:13, name:"Testing"}, {id:14, name:"Analytics"}, {id:15, name:"UX/UI"},
    {id:16, name:"DevOps"},{id:17, name:"Admin"},{id:18, name:"Security"}, {id:19, name:"Data"}, {id:20, name:"Inne"}
  ];

  const adsArray = [
    {id:1, title:"Praca Trójmiasto", localization:1, salary:"5000-7500", date:"20-10-2021", description:"Poszukiwany Junior Web Developer do pracy na pół etatu", skills:[1,2,3]}
  ];

  return (
    <div className="data">
      <InputData localizationArray={localizationArray}></InputData>
      <FilterData skillsArray={skillsArray}></FilterData>
      <AdvertData localizationArray={localizationArray} skillsArray={skillsArray} adsArray={adsArray}></AdvertData>
    </div>
    );
}