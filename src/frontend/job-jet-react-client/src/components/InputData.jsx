import '../styles/main-styles.css';
import '../styles/data-styles.css';

export const InputData = (props) => {

  const renderedArray = props.localizationArray.map(
    (city) => {
      return <option key={city.id} value={city.id}>{city.name}</option>
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
        />
        <select id="search" className="custom-select">
          {renderedArray}
        </select>
      </form>
    </div>
    );
}