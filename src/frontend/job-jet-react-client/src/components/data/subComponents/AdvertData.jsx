import "../styles/data-styles.css";
import { Link } from "react-router-dom";
import { AdvertSynopsis } from "./AdvertSynopsis";

export const AdvertData = (props) => {
  const renderedArray = props.jobOffersArray.map((jobOffer) => {
    const handleClick = () => {
      props.setAdvertDetails({
        title: jobOffer.name,
        address: jobOffer.address.town,
        conditions: `${jobOffer.salaryFrom}-${jobOffer.salaryTo} PLN`,
        description: jobOffer.description,
        advancedDataArray: [
          { name: "Firma:", value: jobOffer.company },
          { name: "Praca skupiona na:", value: jobOffer.focus },
          { name: "Poziom doświadczenia", value: jobOffer.seniority },
          { name: "Liczba pracowników", value: jobOffer.numberOfEmployees },
        ],
      });
      props.setAdvertLocation({
        lat: jobOffer.address.latitude,
        lng: jobOffer.address.longitude,
      });
    };
    return (
      <Link
        key={jobOffer.id}
        className="custom-link"
        to="/details"
        onClick={handleClick}
      >
        <AdvertSynopsis
          title={jobOffer.name}
          localization={jobOffer.address.town}
          salary={`${jobOffer.salaryFrom}-${jobOffer.salaryTo} PLN`}
          date={jobOffer.date}
          description={jobOffer.description}
          technologyTypesArray={jobOffer.technologyType}
          localizationArray={props.localizationArray}
        />
      </Link>
    );
  });

  return <div className="data__advert-data">{renderedArray}</div>;
};
