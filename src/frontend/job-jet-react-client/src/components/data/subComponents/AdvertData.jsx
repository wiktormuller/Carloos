import "../styles/data-styles.css";
import { Link } from "react-router-dom";
import { AdvertSynopsis } from "./AdvertSynopsis";

export const AdvertData = (props) => {
  const renderedArray = props.adsArray.map((ad) => {
    return (
      <Link className="custom-link" to="/details">
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
