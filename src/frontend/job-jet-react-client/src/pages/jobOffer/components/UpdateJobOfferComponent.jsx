import React, { useState, useEffect } from 'react';
import JobOfferService from '../../../clients/JobOfferService';
import { Navigate, useParams } from 'react-router-dom';
import '../update-job-offer-styles.css';

export default function UpdateJobOfferComponent()
{
    let { id } = useParams();

    const [redirect, setRedirect] = useState(false);

    const [jobOffer, setJobOffer] = useState({
        id: id,
        name: '',
        description: '',
        salaryFrom: 0,
        salaryTo: 0
    });

    function updateJobOffer(event) {
        event.preventDefault();
        let jobOfferRequest = {
            name: jobOffer.name,
            description: jobOffer.description,
            salaryFrom: jobOffer.salaryFrom,
            salaryTo: jobOffer.salaryTo
        };

        JobOfferService.updateJobOffer(jobOfferRequest, jobOffer.id).then(res => {
            setRedirect(true);
        });
    }

    function changeNameHandler(event)
    {
        event.preventDefault();
        setJobOffer({...jobOffer, name: event.target.value});
    }

    function changeDescriptionHandler(event)
    {
        event.preventDefault();
        setJobOffer({...jobOffer, description: event.target.value});
    }

    function changeSalaryFrom(event)
    {
        event.preventDefault();
        setJobOffer({...jobOffer, salaryFrom: event.target.value});
    }

    function changeSalaryTo(event)
    {
        event.preventDefault();
        setJobOffer({...jobOffer, salaryTo: event.target.value});
    }

    function cancel(event)
    {
        event.preventDefault();
        setRedirect(true);
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        JobOfferService.getJobOfferById(jobOffer.id).then((res) => {
            let jobOfferResponse = res.data;
            setJobOffer({
                id: jobOfferResponse.id,
                name: jobOfferResponse.name,
                description: jobOfferResponse.description,
                salaryFrom: jobOfferResponse.salaryFrom,
                salaryTo: jobOfferResponse.salaryTo
            });
        });
    }, []);

    const renderRedirected = () => {
        if (redirect) {
            return <Navigate to='/' />
        }
    }

    return (
        <div className="update-job-offer">
            {renderRedirected()}
            <div className = "card col-md-6 offset-md-3 offset-md-3">
                <h3 className="text-center">Update Job Offer</h3>
                <div className = "card-body">
                    <form onSubmit={updateJobOffer}>
                        <div className = "form-group">
                            <label>Id</label>
                            <input placeholder="Id" name="id" className="form-control" 
                                value={jobOffer.id} disabled />
                        </div>
                        <div className = "form-group">
                            <label>Name</label>
                            <input placeholder="Name" name="name" className="form-control" 
                                value={jobOffer.name} onChange={changeNameHandler}/>
                        </div>
                        <div className = "form-group">
                            <label>Description</label>
                            <input placeholder="Description" name="description" className="form-control" 
                                value={jobOffer.description} onChange={changeDescriptionHandler} />
                        </div>
                        <div className = "form-group">
                            <label>Salary From</label>
                            <input placeholder="Salary From" name="salaryFrom" className="form-control" 
                                value={jobOffer.salaryFrom} onChange={changeSalaryFrom}/>
                        </div>
                        <div className = "form-group">
                            <label>Salary To</label>
                            <input placeholder="Salary To" name="salaryTo" className="form-control" 
                                value={jobOffer.salaryTo} onChange={changeSalaryTo} />
                        </div>

                        <button type="submit" className="btn btn-success">Save</button>
                        <button className="btn btn-danger" onClick={cancel} style={{marginLeft: "10px"}}>Cancel</button>
                    </form>
                </div>
            </div>
        </div>
    );
}