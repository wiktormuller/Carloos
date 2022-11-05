import "./addJobOfferPanel-styles.css";
import { useState } from "react";

export const AddJobOfferPanel = (props) => {
  const addLogin = `https://jobjet.azurewebsites.net/api/v1/job-offers`;

  const style = {
    width: "242.5px",
    height: "15px",
    fontSize: "0.75rem",
    margin: "2.5px",
    padding: "5px 10px",
  };

  const handleClick = () => {
    let body = {
      companyId: companyId,
      name: `${name}`,
      description: `${description}`,
      salaryFrom: salaryFrom,
      salaryTo: salaryTo,
      address: {
        town: `${town}`,
        street: `${street}`,
        zipCode: `${zipCode}`,
        countryIsoId: countryIsoId,
      },
      seniorityId: seniorityId,
      employmentTypeId: employmentTypeId,
      currencyId: currencyId,
      technologyTypeIds: skills,
      workSpecification: `${workSpecification}`,
    };

    let strBody = JSON.stringify(body);

    console.log(strBody);

    fetch(addLogin, {
      method: "POST",
      headers: new Headers({
        accept: "text/plain",
        Authorization: "Bearer " + `${props.token}`,
        "Content-Type": "application/json",
      }),
      body: strBody,
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
      });
  };

  const [companyId, setCompanyId] = useState(1);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [salaryFrom, setSalaryFrom] = useState(0);
  const [salaryTo, setSalaryTo] = useState(0);

  const [town, setTown] = useState("");
  const [street, setStreet] = useState("");
  const [zipCode, setZipCode] = useState("");
  const [countryIsoId, setCountryIsoId] = useState(6);
  const [skills, setSkills] = useState(["1"]);

  const [seniorityId, setSeniorityId] = useState(1);
  const [employmentTypeId, setEmploymentTypeId] = useState(1);
  const [currencyId, setCurrencyId] = useState(1);
  const [workSpecification, setWorkSpecification] = useState("");

  const renderedArrayCountries = props.countries.map((country) => {
    return (
      <option key={country.id} value={country.id}>
        {country.name}
      </option>
    );
  });

  const renderedArrayCompanies = props.companies.map((company) => {
    return (
      <option key={company.id} value={company.id}>
        {company.name}
      </option>
    );
  });

  const renderedArraySkills = props.technologyTypes.map((skill) => {
    return (
      <option key={skill.id} value={skill.id}>
        {skill.name}
      </option>
    );
  });

  const renderedArrayCurrencies = props.currencies.map((currency) => {
    return (
      <option key={currency.id} value={currency.id}>
        {currency.isoCode}
      </option>
    );
  });

  const renderedArraySeniority = props.seniority.map((seniority) => {
    return (
      <option key={seniority.id} value={seniority.id}>
        {seniority.name}
      </option>
    );
  });

  const renderedArrayEmploymentType = props.employmentTypes.map(
    (employmentType) => {
      return (
        <option key={employmentType.id} value={employmentType.id}>
          {employmentType.name}
        </option>
      );
    }
  );

  const renderedArrayWorkSpecification = [
    { id: 1, name: "Hybrid"},
    { id: 2, name: "Office"},
    { id: 3, name: "FullyRemote"}
  ].map((specification) => {
    return (
      <option key={specification.id} value={specification.name}>
        {specification.name}
      </option>
    );
  });

  const handleOnChangeMultiSelect = (value) => {
    setSkills([value]);
  };

  return (
    <div className="panel">
      <form className="form">
        <h2>Add job offer</h2>
        <input
          type="text"
          placeholder="Job name"
          name="name"
          id="name"
          style={style}
          required
          onChange={(e) => {
            setName(e.target.value);
          }}
        ></input>
        <textarea
          id="description"
          name="description"
          rows="5"
          cols="33"
          onChange={(e) => {
            setDescription(e.target.value);
          }}
        >
          Description
        </textarea>
        <select
          className="custom-select"
          name="company"
          id="company"
          onChange={(e) => {
            setCompanyId(e.target.value);
          }}
          required
        >
          {renderedArrayCompanies}
        </select>
        <select
          className="custom-select"
          name="skills"
          id="skills"
          required
          onChange={(e) => {
            handleOnChangeMultiSelect(e.target.value);
          }}
        >
          {renderedArraySkills}
        </select>
        <input
          type="text"
          placeholder="Salary from"
          name="salaryFrom"
          id="salaryFrom"
          style={style}
          onChange={(e) => {
            setSalaryFrom(e.target.value);
          }}
          required
        ></input>
        <input
          type="text"
          placeholder="Salary to"
          name="salaryTo"
          id="salaryTo"
          style={style}
          onChange={(e) => {
            setSalaryTo(e.target.value);
          }}
          required
        ></input>
        <select
          className="custom-select"
          name="currency"
          id="currency"
          onChange={(e) => {
            setCurrencyId(e.target.value);
          }}
          required
        >
          {renderedArrayCurrencies}
        </select>
        <select
          className="custom-select"
          name="seniority"
          id="seniority"
          onChange={(e) => {
            setSeniorityId(e.target.value);
          }}
          required
        >
          {renderedArraySeniority}
        </select>
        <select
          className="custom-select"
          name="employmentType"
          id="employmentType"
          onChange={(e) => {
            setEmploymentTypeId(e.target.value);
          }}
          required
        >
          {renderedArrayEmploymentType}
        </select>
        <select
          className="custom-select"
          name="workSpecification"
          id="workSpecification"
          onChange={(e) => {
            setWorkSpecification(e.target.value);
          }}
          required
        >
          {renderedArrayWorkSpecification}
        </select>
        <select
          className="custom-select"
          name="countries"
          id="countries"
          required
          onChange={(e) => {
            setCountryIsoId(e.target.value);
          }}
        >
          {renderedArrayCountries}
        </select>
        <input
          type="text"
          placeholder="City"
          name="town"
          id="town"
          style={style}
          onChange={(e) => {
            setTown(e.target.value);
          }}
          required
        ></input>
        <input
          type="text"
          placeholder="Street"
          name="street"
          id="street"
          style={style}
          onChange={(e) => {
            setStreet(e.target.value);
          }}
          required
        ></input>
        <input
          type="text"
          placeholder="Post code"
          name="zipCode"
          id="zipCode"
          style={style}
          onChange={(e) => {
            setZipCode(e.target.value);
          }}
          required
        ></input>
        <br />
        <button type="button" onClick={handleClick}>
          Publish
        </button>
      </form>
    </div>
  );
};
