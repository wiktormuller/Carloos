import { useState } from "react";

import '../styles/data-styles.css';

export const FilterButton = (props) => {
    const [active, setActive] = useState(true);
    const [clicked, setClicked] = useState("");
    
    const handleClick = () => {
      setActive(!active);
      if (active === true) {
        setClicked("--clicked");
        setActive(false);
      }
      else {
        setClicked("");
      }
    };
  
    return (
      <button
        className={`data__filter-data--btn${clicked}`}
        onClick={handleClick}
      >
        {props.name}
      </button>
    );
  };