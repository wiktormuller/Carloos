import React from "react";

import { useState } from "react";
import "../styles/data-styles.css";
import { Icon } from "../../icon/Icon";

export const FilterButton = (props) => {
  const [active, setActive] = useState(true);
  const [clicked, setClicked] = useState("");

  const handleClick = () => {
    setActive(!active);
    if (active === true) {
      setClicked("--clicked");
      setActive(false);
      props.setSearchedSkills((old) => [...old, props.id]);
    } else {
      setClicked("");
      props.setSearchedSkills((old) => old.filter((id) => id !== props.id));
    }
  };

  return (
    <button
      className={`data__filter-data--btn${clicked}`}
      onClick={handleClick}
    >
      <div className="data__filter-data--btn--icon">
        <Icon iconName={props.icon} size={"2em"} color={props.color} />
        <span>{props.name}</span>
      </div>
    </button>
  );
};
