# JobJet
**Interactive web application integrated with map where we can find any job in IT sector.**

![image](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![image](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![image](https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB)
![image](https://img.shields.io/badge/Azure_DevOps-0078D7?style=for-the-badge&logo=azure-devops&logoColor=white)
![image](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=Swagger&logoColor=white)
![image](https://img.shields.io/badge/Leaflet-199900?style=for-the-badge&logo=Leaflet&logoColor=white)
![image](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![image](https://img.shields.io/badge/JavaScript-323330?style=for-the-badge&logo=javascript&logoColor=F7DF1E)
![image](https://img.shields.io/badge/OpenStreetMap-7EBC6F?style=for-the-badge&logo=OpenStreetMap&logoColor=white)

---

## Project
This repository is divided for two main projects. It is frontend Single Page Application made in React.js and backend REST API made in ASP.NET Core.

---

## The reason for the creation
This project is my production ready thesis project for University engineering project.

---

## Graphical presentation of most important parts
https://imgur.com/a/NhfDCcl

---

## Healthchecks
The local healthchecks are available at those addresses `http://localhost:5002/health-checks` and `http://localhost:5002/health-checks-ui#/healthchecks`<br />
The production healthchecks are available at those addresses `https://jobjet.azurewebsites.net/health-checks` and `https://jobjet.azurewebsites.net/health-checks-ui#/healthchecks`

## External dependencies
We use APIs like those:<br />
    - Google Geocoding API - https://developers.google.com/maps/documentation/geocoding/overview<br />
    - Project OSRM - https://project-osrm.org/<br />
    - Open Street Map - https://www.openstreetmap.org/#map=6/52.018/19.137<br />
    
The backend part of application was hosted on Azure App Services, the frontend part was hosted on Netflify.
The building and publishing process was handled by GitHub Actions where the newest changes from main branch were instantly deployed to App Services/Netlify.
Internally the app uses Azure SQL database.

---

## Run the backend
Go to `JobJet/src/backend/JobJetRestApi`<br />
Run `dotnet restore`

Go to `JobJet/src/backend/JobJetRestApi.Web`<br />
Run `dotnet ef database update --context JobJetDbContext --project ../JobJetRestApi.Infrastructure --connection "Data Source=(LocalDb)\MSSQLLocalDB;Database=JobJet-Development;Trusted_Connection=True;"`

Go to Environment Variables in your system and set Connection String for the application. Where key is: `JobJetVariables_ConnectionStrings__DefaultConnection` and value is `Server=(LocalDb)\MSSQLLocalDB;Database=JobJet-Development;Trusted_Connection=True;`

Go to Environment Variables in your system and set JWT Secret for the application, Where key is: `JobJetVariables_Jwt__Secret` and value is `TopSecretKey123TopSecretKey123` - the value must be equals to 30 characters

Go to Environment Variables in your system and set JWT Secret for the application, Where key is: `JobJetVariables_Geocoding_ApiKey` and value is `YourRealGeocodingApiKey` - it's API key from Google Geocoding Services.

Go to `JobJet/src/backend/JobJetRestApi.Web`<br />
Run `dotner run`

You can find Swagger document at this address `https://localhost:5003/swagger/index.html`

---

## Run the frontend
Go to `JobJet/src/frontend/job-jet-react-client/src`<br />
Run `npm start`

#### Frontend by default use the Production version of REST API.

You can find the app at this address `http://localhost:3000/`

---

## To add new database migrations
Go to `JobJet/src/backend/JobJetRestApi.Web`<br />
Run `dotnet ef migrations add SomeMigrationName --context JobJetDbContext --project ../JobJetRestApi.Infrastructure -o ../JobJetRestApi.Infrastructure/Persistence/Migrations`
