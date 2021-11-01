import '../styles/main-styles.css';
import '../styles/data-styles.css';

export const InputData = (props) => {


  const updateSearchedInput = (value) => {
    props.setSearchedInput(value);
  };

  const updateSearchedLocalizations = (value) => {
    props.setSearchedLocalization(value)
  }

  const renderedArray = props.localizationArray.map(
    (city) => {
      return <option key={city.id} value={city.name}>{city.name}</option>
    }
  )

  return (
    <div className="data__input-data">
      <form className="data__input-data--form">
        <input
          type='text'
          id="text"
          name="text"
          placeholder="Wyszukaj"
          onChange={(e) => {
            updateSearchedInput(e.target.value);
            props.filterData()
            }
          }
          
        />
        <select id="search" className="custom-select" onChange={(e) => {
            updateSearchedLocalizations(e.target.value);
            props.filterData()
            }
        }>
          {renderedArray}
        </select>
      </form>
    </div>
    );
}