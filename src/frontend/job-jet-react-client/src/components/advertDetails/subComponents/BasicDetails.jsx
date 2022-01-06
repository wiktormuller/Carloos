import "./../styles/advert-details-styles.css";

export const BasicDetails = (props) => {
  return (
    <div className="advert-details__basic">
      <div className="advert-details__basic--container">
        <div className="basic-details__photo"></div>
        <div className="basic-details__data">
          <div className="basic-details__data--header">
            <span className="header__primary">{props.title}</span>
            <span className="header__secondary">{props.address}</span>
          </div>
          <span className="basic-details__data--footer">
            {props.conditions}
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
