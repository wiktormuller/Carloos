import '../styles/main-styles.css';
import '../styles/data-styles.css';

import { FilterButton } from "./FilterButton"

export const FilterData = (props) => {

    const renderedArray = props.skillsArray.map(
      (skill) => {
        return <FilterButton key={skill.id} name={skill.name} />
      }
    )

    return (
      <div className="data__filter-data">
        {renderedArray}
      </div>
      );
}