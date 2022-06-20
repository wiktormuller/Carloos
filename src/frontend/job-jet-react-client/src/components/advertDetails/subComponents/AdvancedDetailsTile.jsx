import "./../styles/advert-details-styles.css";

export const AdvancedDetailsTile = (props) => {
  return (
    <div className="advert-details__advanced--tile">
      <span className="header__secondary">{props.details.name}</span>
      <span className="header__primary">{props.details.value}</span>
    </div>
  );
};
