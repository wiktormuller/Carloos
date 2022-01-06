import "../styles/data-styles.css";
import { Link } from "react-router-dom";
import { AdvertSynopsis } from "./AdvertSynopsis";

export const AdvertData = (props) => {
  const renderedArray = props.adsArray.map((ad) => {
    const handleClick = () => {
      props.setAdvertDetails({
        title: ad.title,
        address: ad.address,
        conditions: ad.conditions,
        description: ad.description,
        advancedDataArray: ad.advancedDataArray,
      });
    };
    return (
      <Link className="custom-link" to="/details" onClick={handleClick}>
        <AdvertSynopsis
          key={ad.id}
          title={ad.title}
          localization={ad.localization}
          salary={ad.salary}
          date={ad.date}
          description={ad.description}
          skills={ad.skills}
          skillsArray={props.skillsArray}
          localizationArray={props.localizationArray}
        />
      </Link>
    );
  });

  return <div className="data__advert-data">{renderedArray}</div>;
};
