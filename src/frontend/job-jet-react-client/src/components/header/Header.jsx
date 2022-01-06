import "./header-styles.css";
import { Link } from "react-router-dom";

export const Header = (props) => {
  const checkUserLogInState = () => {
    if (!props.userLogInState) {
      return (
        <div className="header__unregistered-user">
          <button className="header__btn">Rejestracja</button>
          <button className="header__btn">Logowanie</button>
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

  return (
    <div className="header">
      <Link className="custom-link" to="/">
        <p className="header__logo">JobJet</p>
      </Link>
      {checkUserLogInState()}
    </div>
  );
};
