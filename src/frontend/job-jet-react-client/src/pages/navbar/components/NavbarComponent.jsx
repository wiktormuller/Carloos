import "../navbar-styles.css";
import { Link } from "react-router-dom";
import { useContext } from 'react';
import { AuthenticationContext } from "../../../common/AuthenticationContext";
import { 
  AiOutlineHome, 
  AiOutlineDashboard, 
  AiOutlineMail, 
  AiFillSetting, 
  AiOutlineUsergroupAdd, 
  AiOutlineExperiment, 
  AiFillDatabase, 
  AiOutlineSafetyCertificate 
} from 'react-icons/ai';

export default function NavbarComponent()
{
  const [ currentUser, setCurrentUser ] = useContext(AuthenticationContext);

  const renderLinksForLoggedUser = () => {
    if (currentUser && currentUser.hasUserRole) {
      return (
        <div>
          <li>
            <Link className="nav-link text-white navbar-link" key={5} to="/profile">
              <AiFillSetting size={25} />
              <span className="navbar-text">Profile</span>
            </Link>
          </li>
        </div>
      );
    }
  };

  const renderLinksForAdministrator = () => {
    if (currentUser && currentUser.hasAdministratorRole) {
      return (
        <div>
          <li>
            <Link className="nav-link text-white navbar-link" key={5} to="/users">
              <AiOutlineUsergroupAdd size={25} />
              <span className="navbar-text">Users</span>
            </Link>
          </li>

          <li>
            <Link className="nav-link text-white navbar-link" key={5} to="/roles">
              <AiOutlineSafetyCertificate size={25} />
              <span className="navbar-text">Roles</span>
            </Link>
          </li>

          <li>
            <Link className="nav-link text-white navbar-link" key={5} to="/technology-types">
              <AiOutlineExperiment size={25} />
              <span className="navbar-text">Technology Types</span>
            </Link>
          </li>
        </div>
      );
    }
  }

  const renderLinksForEverybody = () => {
    return (
      <div>
        <li className="nav-item">
          <Link className="nav-link text-white navbar-link" key={5} to="/">
            <AiOutlineHome size={25} />
            <span className="navbar-text">JobOffers</span>
          </Link>
        </li>
        <li>
          <Link className="nav-link text-white navbar-link" key={5} to="/companies">
            <AiFillDatabase size={25} />
            <span className="navbar-text">Companies</span>
          </Link>
        </li>
        <li>
          <Link className="nav-link text-white navbar-link" key={5} to="/dashboards">
            <AiOutlineDashboard size={25} />
            <span className="navbar-text">Dashboards</span>
          </Link>
        </li>
        <li>
          <Link className="nav-link text-white navbar-link" key={5} to="/contact">
            <AiOutlineMail size={25} />
            <span className="navbar-text">Contact</span>
          </Link>
        </li>
      </div>
    );
  }

  return (
    <div className="navbar">
        <div className="d-flex flex-column flex-shrink-0 p-3 text-bg-dark navbar-content">
          <a href="/" className="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none">
            <svg className="bi pe-none me-2" width="40" height="32"><use xlinkHref="#bootstrap"></use></svg>
            <span className="fs-4">Menu</span>
          </a>
          <hr/>
          <ul className="nav nav-pills flex-column mb-auto">
            {/* Available for all */}
            {renderLinksForEverybody()}

            {/* Available only for logged user */}
            {renderLinksForLoggedUser()}

            {/* Available only for administrator */}
            {renderLinksForAdministrator()}
            <hr/>
          </ul>
        </div>
    </div>
  );
}