import './job-offers-list-styles.css';
import { Link } from 'react-router-dom';

export default function JobOffersListComponent(props)
{
  return (
    <div className="job-offers">
        <ol className="list-group job-offers-list">

            {props.jobOffers.map(jobOffer => (
                <Link to={`/job-offers/${jobOffer.id}`}>
                    <li className="list-group-item d-flex justify-content-between align-items-start job-offers-list-item" key={jobOffer.id}>
                        <div className="ms-2 me-auto">
                            <div className="fw-bold">{jobOffer.name}</div>
                            {jobOffer.description}
                            <br />
                            <span className="badge bg-info rounded-pill job-offer-badge">{jobOffer.address.town}</span>
                            <span className="badge bg-danger rounded-pill job-offer-badge">{jobOffer.workSpecification}</span>
                        </div>
                        <span className="badge bg-success rounded-pill job-offer-badge">{jobOffer.salaryFrom} - {jobOffer.salaryTo}</span>
                        {
                            jobOffer.technologyTypes.map(technologyType => (  
                                <span className="badge bg-warning rounded-pill job-offer-badge">
                                    {technologyType.name}
                                </span>
                            ))
                        }
                    </li>
                </Link>
            ))}
        </ol>
    </div>
  );
}