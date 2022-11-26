import '../job-offer-details-styles.css';

export default function JobOfferDetailsComponent(props) // TODO: Select job offer geolocation and list of job offers to render map
{
    return (
        <div className="job-offer-details">
            
            <div className="job-offer-name">{props.jobOffer.name}</div>
            <div className="job-info">
                <div className="job-tile">Work Specification: {props.jobOffer.workSpecification}</div>
                <div className="job-tile">Salary: {props.jobOffer.salaryFrom} - {props.jobOffer.salaryTo}</div>
                <div className="job-tile">Seniority: {props.jobOffer.seniority}</div>
                <div className="job-tile">Employment Type: {props.jobOffer.employmentType}</div>
            </div>

            <div className="other-info">
                <div className="other-tile">{props.jobOffer.companyName}</div>
                <div className="other-tile">{props.jobOffer.address.countryName}</div>
                <div className="other-tile">{props.jobOffer.address.town}</div>
                <div className="other-tile">{props.jobOffer.address.zipCode}</div>
            </div>

            <h1>Technology Types</h1>
            <div className="other-info">
                {props.jobOffer.technologyTypes.map(technologyType => {
                    return <div className="other-tile">{technologyType.name}</div>
                })}
            </div>

            <h1>Job Offer Details</h1>
            <div className="other-info">
                <p>{props.jobOffer.description}</p>
                <p>{props.jobOffer.createdAt.substring(0, 10)}</p>
            </div>
        </div>
    );
}