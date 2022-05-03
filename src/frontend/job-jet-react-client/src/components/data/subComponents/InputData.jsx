import "../../../styles/main-styles.css";
import "../styles/data-styles.css";

export const InputData = (props) => {
  const updateSearchedInput = (value) => {
    props.setSearchedInput(value);
  };

  const updateSearchedLocalizations = (value) => {
    props.setSearchedLocalization(value);
  };

  const renderedArray = props.localizationArray.map((country) => {
    return (
      <option key={country.id} value={country.id}>
        {country.name}
      </option>
    );
  });

  return (
    <div className="data__input-data">
      <form className="data__input-data--form">
        <input
          type="text"
          id="text"
          name="text"
          placeholder="Wyszukaj"
          onChange={(e) => {
            updateSearchedInput(e.target.value);
          }}
        />
        <select
          id="search"
          className="custom-select"
          onChange={(e) => {
            console.log(e.target.value);
            updateSearchedLocalizations(e.target.value);
          }}
        >
          {renderedArray}
        </select>
      </form>
    </div>
  );
};
