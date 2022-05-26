import "./navbar-styles.css";
import { Icon } from "../icon/Icon";
import { navbarLinksTable } from "./../../data/arrays";
import { Link } from "react-router-dom";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

export const Navbar = (props) => {
  const renderedArray = navbarLinksTable.map((link) => {
    return (
      <Link key={link.id} className="custom-link" to="/register">
        <Icon
          iconName={link.iconName}
          size={"3em"}
          color={"#FFFFFF"}
          className={"icon"}
        ></Icon>
      </Link>
    );
  });

  return <div className="navbar">{renderedArray}</div>;
};
