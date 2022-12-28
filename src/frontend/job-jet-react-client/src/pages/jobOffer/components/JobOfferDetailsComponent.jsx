import '../job-offer-details-styles.css';
import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import JobOfferService from '../../../clients/JobOfferService';
import { useNavigate } from 'react-router-dom';

export default function JobOfferDetailsComponent(props) // TODO: Select job offer geolocation and list of job offers to render map
{
    const [show, setShow] = useState(false);

    const [userEmail, setUserEmail] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [file, setFile] = useState('');

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const navigate = useNavigate();

    function handleSendApplication(event)
    {
        event.preventDefault();

        JobOfferService.sendJobOfferApplication(props.jobOffer.id, userEmail, phoneNumber, file)
        .then(res => {
            handleClose();
        });
    }

    function handleSetUserEmail(event)
    {
        event.preventDefault();

        setUserEmail(event.target.value);
    }

    function handleSetPhoneNumber(event)
    {
        event.preventDefault();

        setPhoneNumber(event.target.value);
    }

    function handleSetFile(event)
    {
        event.preventDefault();

        setFile(event.target.files[0]);
    }

    return (
        <div className="job-offer-details">
            <div className="job-offer-header">
                <div className="job-offer-header-top">
                    <div className="job-offer-name">{props.jobOffer.name}</div>

                    <div className="company-address">
                        {props.jobOffer.address.street}, {props.jobOffer.address.zipCode} {props.jobOffer.address.town} - {props.jobOffer.address.countryName}
                    </div>
                </div>

                <div className="job-offer-header-bottom">
                    <div className="salary-info">
                        {props.jobOffer.salaryFrom} - {props.jobOffer.salaryTo}, {props.jobOffer.employmentType}
                    </div>

                    <div className="job-offer-apply-button-with-modal">
                        <Button variant="warning" onClick={handleShow}>
                            Apply for the Job!
                        </Button>

                        <Modal show={show} onHide={handleClose}>
                            <Modal.Header closeButton>
                            <Modal.Title>Your application for the job offer</Modal.Title>
                            </Modal.Header>
                            <Modal.Body>
                                <p>It's the opportunity for new start!</p>

                                <Form onSubmit={handleSendApplication}>
                                    <Form.Group className="mb-3" controlId="formBasicEmail">
                                        <Form.Label>Email Address</Form.Label>
                                        <Form.Control type="email" placeholder="Enter email" onChange={handleSetUserEmail} />
                                        <Form.Text className="text-muted">
                                            We'll never share your email with anyone else.
                                        </Form.Text>
                                    </Form.Group>

                                    <Form.Group className="mb-3" controlId="formBasicEmail">
                                        <Form.Label>Phone Number</Form.Label>
                                        <Form.Control type="text" placeholder="Enter phone number" onChange={handleSetPhoneNumber}/>
                                        <Form.Text className="text-muted">
                                            We'll never share your email with anyone else.
                                        </Form.Text>
                                    </Form.Group>

                                    <Form.Group controlId="formFile" className="mb-3">
                                        <Form.Label>Your job application in .pdf or .doc format</Form.Label>
                                        <Form.Control type="file" onChange={handleSetFile} accept=".pdf, .doc, .docx, image/png, image/jpe" />
                                    </Form.Group>

                                    <Button variant="primary" type="submit">
                                        Apply
                                    </Button>
                                </Form>
                                
                            </Modal.Body>
                            <Modal.Footer>
                            <Button variant="secondary" onClick={handleClose}>
                                Close
                            </Button>
                            </Modal.Footer>
                        </Modal>
                    </div>
                </div>
            </div>

            <div className="job-offer-extra-info">
                <div class="container">
                    <div class="row">
                        <div class="col">
                            <p className="col-row-header">Company</p>
                            <p className="col-row-content">{props.jobOffer.companyName}</p>
                        </div>
                        <div class="col">
                            <p className="col-row-header">Created At</p>
                            <p className="col-row-content">{props.jobOffer.createdAt.substring(0, 10)}</p>
                        </div>
                        <div class="w-100"></div>
                        <div class="col">
                            <p className="col-row-header">Level of Experience</p>
                            <p className="col-row-content">{props.jobOffer.seniority}</p>
                        </div>
                        <div class="col">
                            <p className="col-row-header">Work Specification</p>
                            <p className="col-row-content">{props.jobOffer.workSpecification}</p>
                        </div>
                    </div>
                </div>
            </div>

            <div className="job-offer-technologies">
                <p>Technology Types</p>
                <div className="technology-types-section">
                    {props.jobOffer.technologyTypes.map(technologyType => (
                        <div className="technology-type-marker">
                            <img className="technology-type-marker-image" src={require(`../../../assets/icons/${technologyType.id}.svg`)} alt="Technology Type Img" />
                            <span className="technology-type-marker-label">{technologyType.name}</span>
                        </div>
                    ))}
                </div>
            </div>

            <div className="job-offer-description">
                <p>{props.jobOffer.description}</p>
            </div>
        </div>
    );
}