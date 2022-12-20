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
            
            <div className="job-offer-name">{props.jobOffer.name}</div>
            <div className="job-info">
                <div className="job-tile">Work Specification: {props.jobOffer.workSpecification}</div>
                <div className="job-tile">Salary: {props.jobOffer.salaryFrom} - {props.jobOffer.salaryTo}</div>
                <div className="job-tile">Seniority: {props.jobOffer.seniority}</div>
                <div className="job-tile">Employment Type: {props.jobOffer.employmentType}</div>
            </div>

            <div>
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