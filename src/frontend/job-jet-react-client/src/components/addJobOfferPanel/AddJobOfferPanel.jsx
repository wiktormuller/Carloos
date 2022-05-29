import "./addJobOfferPanel-styles.css";
import { useState /*, useEffect*/ } from "react";

export const AddJobOfferPanel = (props) => {
  const style = {
    width: "242.5px",
    height: "15px",
    fontSize: "0.75rem",
    margin: "2.5px",
    padding: "5px 10px",
  };

  const [skills, setSkills] = useState([]);

  // console.log(skills);

  const renderedArrayCompanies = props.companies.map((company) => {
    return (
      <option key={company.id} value={company.id}>
        {company.name}
      </option>
    );
  });

  const renderedArraySkills = props.skills.map((skill) => {
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
    "Stationary",
    "Partially Remote",
    "Fully Remote",
  ].map((specification) => {
    return (
      <option key={specification.index} value={specification}>
        {specification}
      </option>
    );
  });

  //Na razie działa jak dla singleSelecta
  const handleOnChangeMultiSelect = (value) => {
    setSkills([value]);
  };

  return (
    <div className="panel">
      <form className="form">
        <h2>Dodaj ofertę pracy</h2>
        <input
          type="text"
          placeholder="Nazwa stanowiska"
          name="name"
          id="name"
          style={style}
          required
        ></input>
        <textarea id="description" name="description" rows="5" cols="33">
          Opis
        </textarea>
        <select className="custom-select" name="company" id="company" required>
          {renderedArrayCompanies}
        </select>
        <select
          className="custom-select"
          name="skills"
          id="skills"
          required
          // multiple
          onChange={(e) => {
            handleOnChangeMultiSelect(e.target.value);
          }}
        >
          {renderedArraySkills}
        </select>
        <input
          type="text"
          placeholder="Zarobki od"
          name="salaryFrom"
          id="salaryFrom"
          style={style}
          required
        ></input>
        <input
          type="text"
          placeholder="Zarobki do"
          name="salaryTo"
          id="salaryTo"
          style={style}
          required
        ></input>
        <select
          className="custom-select"
          name="currency"
          id="currency"
          required
        >
          {renderedArrayCurrencies}
        </select>
        <select
          className="custom-select"
          name="seniority"
          id="seniority"
          required
        >
          {renderedArraySeniority}
        </select>
        <select
          className="custom-select"
          name="employmentType"
          id="employmentType"
          required
        >
          {renderedArrayEmploymentType}
        </select>
        <select
          className="custom-select"
          name="workSpecification"
          id="workSpecification"
          required
        >
          {renderedArrayWorkSpecification}
        </select>
        <input
          type="text"
          placeholder="Kraj"
          name="country"
          id="country"
          style={style}
          required
        ></input>
        <input
          type="text"
          placeholder="Miasto"
          name="town"
          id="town"
          style={style}
          required
        ></input>
        <input
          type="text"
          placeholder="Ulica"
          name="street"
          id="street"
          style={style}
          required
        ></input>
        <input
          type="text"
          placeholder="Kod pocztowy"
          name="zipCode"
          id="zipCode"
          style={style}
          required
        ></input>
        <input
          type="text"
          placeholder="Latitude"
          name="street"
          id="street"
          style={style}
          required
        ></input>
        <input
          type="text"
          placeholder="Longitude"
          name="zipCode"
          id="zipCode"
          style={style}
          required
        ></input>
        <button>Opublikuj ofertę</button>
      </form>
    </div>
  );
};
