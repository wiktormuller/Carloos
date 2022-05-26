import "../styles/data-styles.css";
import { GoLocation } from "react-icons/go";
import { FcMoneyTransfer } from "react-icons/fc";

export const AdvertSynopsis = (props) => {
  const renderArray = props.technologyTypesArray.map((technologyTypes) => {
    return (
      <div
        key={technologyTypes.index}
        className="data__advert-data--synopsis--skill"
      >
        {technologyTypes}
      </div>
    );
  });

  return (
    <div className="data__advert-data--synopsis">
      <div className="data__advert-data--synopsis--header">
        <b>{props.title}</b>
        <span>
          <b>Data dodania: </b>
          {props.date}
        </span>
      </div>
      <div className="data__advert-data--synopsis--body">
        <span>
          <GoLocation />
          {props.localization}
        </span>
        <span>
          <FcMoneyTransfer />
          <b style={{ color: "limegreen" }}>{props.salary}</b>
        </span>
        <span>{props.description}</span>
      </div>
      <div className="data__advert-data--synopsis--skills">{renderArray}</div>
    </div>
  );
};
