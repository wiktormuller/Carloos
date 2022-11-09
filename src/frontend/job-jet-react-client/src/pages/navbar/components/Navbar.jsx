import "./navbar-styles.css";
import { Icon } from "../../icon/Icon";
import { navbarLinks } from "./../../data/arrays";
import { Link } from "react-router-dom";

export const Navbar = (props) => {
    return (
      <div className="navbar">
        <Link
          key={navbarLinks[0].id}
          className="custom-link"
          to={
            navbarLinks[0].path
          }
        >
          <Icon
            iconName={navbarLinks[0].iconName}
            size={"2em"}
            color={"#FFFFFF"}
            className={"icon"}
          ></Icon>
        </Link>

        <Link
          key={navbarLinks[1].id}
          className="custom-link"
          to={
            navbarLinks[1].path
          }
        >
          <Icon
            iconName={navbarLinks[1].iconName}
            size={"2em"}
            color={"#FFFFFF"}
            className={"icon"}
          ></Icon>
        </Link>

        <Link
          key={navbarLinks[2].id}
          className="custom-link"
          to={
            navbarLinks[2].path
          }
        >
          <Icon
            iconName={navbarLinks[2].iconName}
            size={"2em"}
            color={"#FFFFFF"}
            className={"icon"}
          ></Icon>
        </Link>
      </div>
    );
};