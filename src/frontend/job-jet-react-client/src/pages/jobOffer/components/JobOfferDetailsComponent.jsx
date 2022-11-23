import '../job-offer-details-styles.css';

export default function JobOfferDetailsComponent(props) // TODO: Select job offer geolocation and list of job offers to render map
{
    return (
        <div className="job-offer-details">
            <div className="company-details">
                <p>{props.jobOffer.companyName}</p>
                <p>{props.jobOffer.address.countryName}</p>
                <p>{props.jobOffer.address.town}</p>
                <p>{props.jobOffer.address.zipCode}</p>
            </div>

            <div className="technology-types-details">
                {props.jobOffer.technologyTypes.map(technologyType => {
                    return <p>{technologyType.name}</p>
                })}
            </div>

            <div className="description-details">
                <p>{props.jobOffer.name}</p>
                <p>{props.jobOffer.description}</p>
                <p>{props.jobOffer.salaryFrom}</p>
                <p>{props.jobOffer.salaryTo}</p>
                <p>{props.jobOffer.seniority}</p>
                <p>{props.jobOffer.employmentType}</p>
                <p>{props.jobOffer.workSpecification}</p>
                <p>{props.jobOffer.createdAt}</p>
            </div>
        </div>
    );
}