import React from "react";

import { useState } from "react";
import '../styles/data-styles.css';

import * as DiIcons from "react-icons/di";
import * as BiIcons from "react-icons/bi";
import * as SiIcons from "react-icons/si";
import * as MdIcons from "react-icons/md";
import * as RiIcons from "react-icons/ri";
import * as BsIcons from "react-icons/bs";

const Icon = props => {
  const { iconName, size, color } = props;
  let icon;
  if(iconName[0] === 'D' && iconName[1] === 'i'){
    icon = React.createElement(DiIcons[iconName]);
  }
  else if(iconName[0] === 'B' && iconName[1] === 'i'){
    icon = React.createElement(BiIcons[iconName]);
  }
  else if(iconName[0] === 'S' && iconName[1] === 'i'){
    icon = React.createElement(SiIcons[iconName]);    
  }
  else if(iconName[0] === 'M' && iconName[1] === 'd'){
    icon = React.createElement(MdIcons[iconName]);    
  }
  else if(iconName[0] === 'R' && iconName[1] === 'i'){
    icon = React.createElement(RiIcons[iconName]);    
  }
  else if(iconName[0] === 'B' && iconName[1] === 's'){
    icon = React.createElement(BsIcons[iconName]);    
  }
  return <div style={{ fontSize: size, color: color }}>{icon}</div>;
};


export const FilterButton = (props) => {
    const [active, setActive] = useState(true);
    const [clicked, setClicked] = useState("");

    const handleClick = () => {
      setActive(!active);
      if (active === true) {
        setClicked("--clicked");
        setActive(false);
        props.searchedSkills.push(props.id);
        props.filterData();
      }
      else {
        setClicked("");
        const arr = props.searchedSkills;
        for( var i = 0; i < arr.length; i++){ 
          if ( arr[i] === props.id) { 
            props.searchedSkills.splice(i, 1);
            props.filterData();
          }
        }
      }
    };
  
    return (
      <button
        className={`data__filter-data--btn${clicked}`}
        onClick={handleClick}
      >
        <div className="fuck">
          <Icon iconName={props.icon} size={'2em'} color={props.color}/>
          <span>{props.name}</span>
        </div>
      </button>
    );
  };