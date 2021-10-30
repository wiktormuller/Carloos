import '../styles/main-styles.css';
import '../styles/data-styles.css';

import { FilterButton } from "./FilterButton"

export const FilterData = () => {

    const skillsArray = [
      {id:1, name:"HTML5"}, {id:2, name:"CSS3"}, {id:3, name:"JavaScript"},{id:4, name:"TypeScript"}, {id:5, name:"PHP"}, 
      {id:6, name:"C/C++"},{id:7, name:"Java"},{id:8, name:"Python"}, {id:9, name:"Ruby"}, {id:10, name:"Scala"},
      {id:11, name:"Mobile"},{id:12, name:"DevOps"},{id:13, name:"Testing"}, {id:14, name:"Admin"}, {id:15, name:"UX/UI"}
    ];

    const renderedArray = skillsArray.map(
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