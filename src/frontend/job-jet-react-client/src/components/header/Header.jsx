import "./header-styles.css";
import { Link } from "react-router-dom";

export const Header = (props) => {
  const checkUserLogInState = () => {
    if (!props.userLogInState) {
      return (
        <div className="header__unregistered-user">
          <Link className="custom-link" to="/register">
            <button className="header__btn">Rejestracja</button>
          </Link>
          <Link className="custom-link" to="/login">
            <button className="header__btn">Logowanie</button>
          </Link>
        </div>
      );
    } else if (!!props.userLogInState) {
      return (
        <div className="header__registered-user">
          <button className="header__btn">Profil</button>
          <button className="header__btn">Wyloguj się</button>
        </div>
      );
    }
  };

  const handleClick = () => {
    props.setGeoLocation({
      lat: undefined,
      lng: undefined,
    });
    props.setAdvertLocation({
      lat: 52.006376,
      lng: 19.025167,
    });
  };

  return (
    <div className="header">
      <Link className="custom-link" to="/" onClick={handleClick}>
        <p className="header__logo">JobJet</p>
      </Link>
      {checkUserLogInState()}
    </div>
  );
};
