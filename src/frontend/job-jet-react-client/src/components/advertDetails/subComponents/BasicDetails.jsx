import "./../styles/advert-details-styles.css";

export const BasicDetails = () => {
  return (
    <div className="advert-details__basic">
      <div className="advert-details__basic--container">
        <div className="basic-details__photo"></div>
        <div className="basic-details__data">
          <div className="basic-details__data--header">
            <span className="header__primary">Python Developer</span>
            <span className="header__secondary">
              ul. Bajkowa 4, 01-121, Smolary
            </span>
          </div>
          <span className="basic-details__data--footer">
            6000 PLN netto/miesięcznie, UOP
          </span>
        </div>
      </div>
      <div className="advert-details__basic--buttons">
        <div className="buttons">
          <button className="btn primary-btn">Aplikuj</button>
          <button className="btn highlighted-btn">Pokaż drogę do pracy</button>
        </div>
      </div>
    </div>
  );
};
