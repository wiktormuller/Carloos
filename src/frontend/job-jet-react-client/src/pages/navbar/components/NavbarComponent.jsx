import "../navbar-styles.css";
import { Icon } from "../../icon/Icon";
import { Link } from "react-router-dom";
import { useContext } from 'react';
import { AuthenticationContext } from "../../../common/AuthenticationContext";

export default function NavbarComponent()
{
  const [ currentUser, setCurrentUser ] = useContext(AuthenticationContext);

  const renderLinksForLoggedUser = () => {
    if (currentUser && currentUser.hasUserRole) {
      return (
        <Link
          key={ 4 }
          className="custom-link"
          to='/profile' >
          <Icon
            iconName='IoDocumentTextOutline'
            size={"2em"}
            color={"#FFFFFF"}
            className={"icon"} />
        </Link>
      );
    }
  };

  console.log(currentUser);

  const renderLinksForAdministrator = () => {
    if (currentUser && currentUser.hasAdministratorRole) {
      return (
        <div>
          <Link
          key={ 5 }
          className="custom-link"
          to='/users' >
            <Icon
              iconName='IoDocumentTextOutline'
              size={"2em"}
              color={"#FFFFFF"}
              className={"icon"} />
          </Link>
          <Link
            key={ 5 }
            className="custom-link"
            to='/technology-types' >
            <Icon
              iconName='IoDocumentTextOutline'
              size={"2em"}
              color={"#FFFFFF"}
              className={"icon"} />
          </Link>
        </div>
      );
    }
  }

  return (
    <div className="navbar">
      {/* Available for all */}
      <Link
        key={ 1 }
        className="custom-link"
        to='/' >
        <Icon
          iconName='BsSearch'
          size={"2em"}
          color={"#FFFFFF"}
          className={"icon"} />
      </Link>

      <Link
        key={ 2 }
        className="custom-link"
        to='/dashboards' >
        <Icon
          iconName='IoSettingsOutline'
          size={"2em"}
          color={"#FFFFFF"}
          className={"icon"} />
      </Link>

      <Link
        key={ 3 }
        className="custom-link"
        to='/companies' >
        <Icon
          iconName='IoDocumentTextOutline'
          size={"2em"}
          color={"#FFFFFF"}
          className={"icon"} />
      </Link>

      <Link
        key={ 3 }
        className="custom-link"
        to='/contact' >
        <Icon
          iconName='IoDocumentTextOutline'
          size={"2em"}
          color={"#FFFFFF"}
          className={"icon"} />
      </Link>

      {/* Available only for logged user - users' companies, job offers and account details */}
      {renderLinksForLoggedUser()}

      {/* Available only for administrator */}
      {renderLinksForAdministrator()}
    </div>
  );
}