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
      props.setAdvertLocation({
        lat: ad.lat,
        lng: ad.lng,
      });
    };
    return (
      <Link
        key={ad.id}
        className="custom-link"
        to="/details"
        onClick={handleClick}
      >
        <AdvertSynopsis
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
