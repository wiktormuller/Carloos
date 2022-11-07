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
          { name: "Company:", value: jobOffer.companyName },
          { name: "Employment Type:", value: jobOffer.employmentType },
          { name: "Seniority", value: jobOffer.seniority },
          { name: "Work Specification", value: jobOffer.workSpecification },
        ],
      });
      props.setAdvertLocation({
        latitude: jobOffer.address.latitude,
        longitude: jobOffer.address.longitude,
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
          date={jobOffer.createdAt.substring(0, 10)}
          description={jobOffer.description}
          technologyTypesArray={jobOffer.technologyTypes}
          localizationArray={props.localizationArray}
        />
      </Link>
    );
  });

  return <div className="data__advert-data">{renderedArray}</div>;
};
