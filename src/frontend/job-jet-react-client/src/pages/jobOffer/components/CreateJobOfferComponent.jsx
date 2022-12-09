import "../add-job-offer-styles.css";
import JobOfferService from '../../../clients/JobOfferService';
import CompanyService from '../../../clients/CompanyService';
import CountryService from '../../../clients/CountryService';
import TechnologyTypeService from '../../../clients/TechnologyTypeService';
import SeniorityLevelService from '../../../clients/SeniorityLevelService';
import EmploymentTypeService from '../../../clients/EmploymentTypeService';
import CurrencyService from '../../../clients/CurrencyService';
import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import Select from 'react-select'

export default function CreateJobOfferComponent()
{
  const [companyId, setCompanyId] = useState(0);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [salaryFrom, setSalaryFrom] = useState(0);
  const [salaryTo, setSalaryTo] = useState(0);

  const [town, setTown] = useState("");
  const [street, setStreet] = useState("");
  const [zipCode, setZipCode] = useState("");
  const [countryId, setCountryId] = useState(0);
  const [technologyTypesIds, setTechnologyTypesIds] = useState([]);

  const [seniorityId, setSeniorityId] = useState(0);
  const [employmentTypeId, setEmploymentTypeId] = useState(0);
  const [workSpecification, setWorkSpecification] = useState('');
  const [currencyId, setCurrencyId] = useState(0);

  const availableWorkSpecifications = [
    {
      "value": "1",
      "label": "Hybrid"
    },
    {
      "value": "2",
      "label": "Office"
    },
    {
      "value": "3",
      "label": "FullyRemote"
    }
  ];
  const [ availableCompanies, setAvailableCompanies ] = useState([]);
  const [ availableCountries, setAvailableCountries ] = useState([]);
  const [ availableTechnologyTypes, setAvailableTechnologyTypes ] = useState([]);
  const [ availableSeniorityLevels, setAvailableSeniorityLevels] = useState([]);
  const [ availableEmploymentTypes, setAvailableEmploymentTypes ] = useState([]);
  const [ availableCurrencies, setAvailableCurrencies ] = useState([]);
  
  // Similar to componentDidMount and componentDidUpdate
  useEffect(() => {
      CountryService.getCountries().then(res => {
        setAvailableCountries(res.data.map(country => (
          {
            "value": country.id,
            "label": country.name
          }
        ))); 
      });

      CompanyService.getCompanies().then(res => {
        setAvailableCompanies(res.data.response.data.map(company => (
          {
            "value": company.id,
            "label": company.name
          }
        ))); 
      });

      TechnologyTypeService.getTechnologyTypes().then(res => {
        setAvailableTechnologyTypes(res.data.map(technologyType => (
          {
            "value": technologyType.id,
            "label": technologyType.name
          }
        ))); 
      });

      SeniorityLevelService.getSeniorityLevels().then(res => {
        setAvailableSeniorityLevels(res.data.map(seniorityLevel => (
          {
            "value": seniorityLevel.id,
            "label": seniorityLevel.name
          }
        )));
      });

      EmploymentTypeService.getEmploymentTypes().then(res => {
        setAvailableEmploymentTypes(res.data.map(employmentType => (
          {
            "value": employmentType.id,
            "label": employmentType.name
          }
        )));
      });

      CurrencyService.getCurrencies().then(res => {
        setAvailableCurrencies(res.data.map(currency => (
          {
            "value": currency.id,
            "label": currency.name
          }
        )));
      });
  }, []);

  const navigate = useNavigate();

  const style = {
    width: "242.5px",
    height: "15px",
    fontSize: "0.75rem",
    margin: "2.5px",
    padding: "5px 10px",
  };

  function saveJobOffer()
  {
    let jobOfferRequest = {
      companyId: companyId,
      name: `${name}`,
      description: `${description}`,
      salaryFrom: salaryFrom,
      salaryTo: salaryTo,
      address: {
        town: town,
        street: street,
        zipCode: zipCode,
        countryIsoId: countryId,
      },
      seniorityId: seniorityId,
      employmentTypeId: employmentTypeId,
      currencyId: currencyId,
      technologyTypeIds: technologyTypesIds,
      workSpecification: `${workSpecification}`,
    };

    JobOfferService.createJobOffer(jobOfferRequest).then(res => {
        navigate(`/`);
    });
  };

  function cancel()
  {
    navigate(`/`);
  }

  function selectName(event)
  {
    setName(event.value);
  }

  function selectDescription(event)
  {
    setDescription(event.value);
  }

  function selectSalaryFrom(event)
  {
    setSalaryFrom(event.value);
  }

  function selectSalaryTo(event)
  {
    setSalaryTo(event.value);
  }

  function selectTown(event)
  {
    setTown(event.value);
  }

  function selectStreet(event)
  {
    setStreet(event.value);
  }

  function selectZipCode(event)
  {
    setZipCode(event.value);
  }

  function selectCompany(event)
  {
    setCompanyId(event.value);
  }

  function selectCountry(event)
  {
    setCountryId(event.value);
  }

  function selectTechnologyTypesIds(event)
  {
    setTechnologyTypesIds(event.map(item => {return item.value}));
  }

  function selectSeniorityLevel(event)
  {
    setSeniorityId(event.value);
  }

  function selectEmploymentType(event)
  {
    setEmploymentTypeId(event.value);
  }

  function selectCurrency(event)
  {
    setCurrencyId(event.value);
  }

  function selectWorkSpecification(event)
  {
    setWorkSpecification(event.label) // *Here label is the value sent to the API
  }

  return (
    <div className="create-job-offer">
      <form>
        <div className="form-group">
          <label>Name</label>
          <input type="text" className="form-control" id="name" onChange={selectName} placeholder="Insert Name of Job Offer" />
        </div>
        <div className="form-group">
          <label>Description</label>
          <textarea className="form-control description-job-offer-area" id="description" onChange={selectDescription} placeholder="Insert Description of Job Offer"></textarea>
        </div>
        <div className="form-group">
          <label>Salary From</label>
          <input type="number" className="form-control" id="salaryFrom" onChange={selectSalaryFrom} placeholder="Insert Salary From" />
        </div>
        <div className="form-group">
          <label>Salary To</label>
          <input type="number" className="form-control" id="salaryTo" onChange={selectSalaryTo} placeholder="Insert Salary To" />
        </div>
        <div className="form-group">
          <label>Town</label>
          <input type="text" className="form-control" id="town" onChange={selectTown} placeholder="Insert Town" />
        </div>
        <div className="form-group">
          <label>Street</label>
          <input type="text" className="form-control" id="street" onChange={selectStreet} placeholder="Insert Street" />
        </div>
        <div className="form-group">
          <label>ZipCode</label>
          <input type="text" className="form-control" id="zipCode" onChange={selectZipCode} placeholder="Insert ZipCode" />
        </div>
        <div className="form-group">
          <label>Country</label>
          <Select className="form-control" options={availableCountries} onChange={selectCountry} value={availableCountries[countryId-1]} placeholder="Select Country"/>
        </div>
        <div className="form-group">
          <label>Company</label>
          <Select className="form-control" options={availableCompanies} onChange={selectCompany} value={availableCompanies[companyId-1]} placeholder="Select Company"/>
        </div>
        <div className="form-group">
          <label>Technology Types</label>
          <Select className="form-control" options={availableTechnologyTypes} onChange={selectTechnologyTypesIds} values={availableTechnologyTypes.filter(available => {return technologyTypesIds.some(technologyTypesId => { return technologyTypesId.value === available.value } )})} placeholder="Select Technologies" isMulti/>
        </div>
        <div className="form-group">
          <label>Seniority Level</label>
          <Select className="form-control" options={availableSeniorityLevels} onChange={selectSeniorityLevel} value={availableSeniorityLevels[seniorityId-1]} placeholder="Select Seniority Level"/>
        </div>
        <div className="form-group">
          <label>Employment Type</label>
          <Select className="form-control" options={availableEmploymentTypes} onChange={selectEmploymentType} value={availableEmploymentTypes[employmentTypeId-1]} placeholder="Select Employment Type"/>
        </div>
        <div className="form-group">
          <label>Currency</label>
          <Select className="form-control" options={availableCurrencies} onChange={selectCurrency} value={availableCurrencies[currencyId-1]} placeholder="Select Currency"/>
        </div>
        <div className="form-group">
          <label>Work Specification</label>
          <Select className="form-control" options={availableWorkSpecifications} onChange={selectWorkSpecification} value={availableWorkSpecifications.find(element => {return element.label === workSpecification})} placeholder="Select Work Specification"/>
        </div>
        <button type="submit" className="btn btn-primary">Submit</button>
      </form>
    </div>
  );
}