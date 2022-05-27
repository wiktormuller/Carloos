//Docelowo te tabele będą w db

export const localizationArray = [
  {
    id: 6,
    name: "Poland",
    alpha2Code: "PL",
    lat: 52.006376,
    lng: 19.025167,
    zoom: 6.5,
  },
  {
    id: 7,
    name: "United Kingdom",
    alpha2Code: "GB",
    lat: 54.00366,
    lng: -2.547855,
    zoom: 5.5,
  },
  {
    id: 8,
    name: "Germany",
    alpha2Code: "DE",
    lat: 52.531677,
    lng: 13.381777,
    zoom: 5.7,
  },
  {
    id: 9,
    name: "Switzerland",
    alpha2Code: "CH",
    lat: 46.80111,
    lng: 8.22667,
    zoom: 7.0,
  },
  {
    id: 10,
    name: "Belgium",
    alpha2Code: "BE",
    lat: 50.503887,
    lng: 4.469936,
    zoom: 8.0,
  },
];

// export const jobOffersArray = [
//   {
//     id: 0,
//     name: "Junior Web Developer",
//     description:
//       "Poszukiwany Junior Web Developer do pracy. Po okresie próbnym możliwa praca zdalna",
//     salaryFrom: 4000,
//     salaryTo: 6000,
//     address: {
//       id: 0,
//       countryName: "Poland",
//       town: "Gdańsk",
//       street: "Długi Targ 43-44",
//       zipCode: "80-831",
//       latitude: 54.35,
//       longitude: 18.633,
//     },
//     technologyType: ["HTML", "CSS", "JavaScript"],
//     seniority: "Junior",
//     employmentType: "B2B",
//     workSpecification: "FullyRemote",
//     company: "IBPM S.A",
//     focus: "Produkt",
//     date: "20-10-2021",
//     numberOfEmployees: "90-120",
//   },
// ];

export const skillsArray = [
  { id: 1, name: "HTML5", icon: "DiHtml5", color: "orange" },
  { id: 2, name: "CSS3", icon: "DiCss3", color: "#4169e1" },
  { id: 3, name: "JavaScript", icon: "DiJavascript", color: "#f5c71a" },
  { id: 4, name: "TypeScript", icon: "SiTypescript", color: "#1e90ff" },
  { id: 5, name: "PHP", icon: "DiPhp", color: "#9370db" },
  { id: 6, name: "C/C++", icon: "BiPlusCircle", color: "#9400d3" },
  { id: 7, name: "C#", icon: "BsHash", color: "#228b22" },
  { id: 8, name: "Java", icon: "DiJava", color: "#ff0000" },
  { id: 9, name: "Python", icon: "DiPython", color: "#555d50" },
  { id: 10, name: "Ruby", icon: "DiRuby", color: "#9b111e" },
  { id: 11, name: "Mobile", icon: "BiMobile", color: "#3d0c02" },
  { id: 12, name: "Game", icon: "BiGame", color: "orange" },
  { id: 13, name: "Testing", icon: "BiTestTube", color: "#8bbe1b" },
  { id: 14, name: "Analytics", icon: "MdAnalytics", color: "#273be2" },
  { id: 15, name: "UX/UI", icon: "BiPaint", color: "#757575" },
  { id: 16, name: "DevOps", icon: "SiAzuredevops", color: "#556b2f" },
  { id: 17, name: "Admin", icon: "DiLinux", color: "black" },
  { id: 18, name: "Security", icon: "MdSecurity", color: "#4166f5" },
  { id: 19, name: "Data", icon: "DiDatabase", color: "#ea3c53" },
  { id: 20, name: "Inne", icon: "RiPsychotherapyLine", color: "orange" },
];

export const navbarLinksTable = [
  { id: 1, name: "Wyszukaj", iconName: "BsSearch", path: "/" },
  {
    id: 2,
    name: "Oferty",
    iconName: "IoDocumentTextOutline",
    path: "/register",
  },
  {
    id: 3,
    name: "Pogoda",
    iconName: "TiWeatherStormy",
    path: "/login",
  },
  {
    id: 4,
    name: "Analityka",
    iconName: "DiGoogleAnalytics",
    path: "/register",
  },
  { id: 5, name: "Opcje", iconName: "IoSettingsOutline", path: "/login" },
  {
    id: 6,
    name: "Biuletyn",
    iconName: "IoNewspaperOutline",
    path: "/register",
  },
];
