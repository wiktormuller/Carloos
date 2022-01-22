import "./navbar-styles.css";
import { Icon } from "../icon/Icon";
import { navbarLinksTable } from "./../../data/arrays";

export const Navbar = (props) => {
  const renderedArray = navbarLinksTable.map((link) => {
    return (
      <Icon
        key={link.id}
        iconName={link.iconName}
        size={"3em"}
        color={"#FFFFFF"}
        className={"icon"}
      ></Icon>
    );
  });

  return <div className="navbar">{renderedArray}</div>;
};
