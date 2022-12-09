import React, { useState, useEffect } from 'react';
import CompanyService from '../../../clients/CompanyService'
import { useParams, Navigate } from 'react-router-dom';
import '../update-company-styles.css';

export default function UpdateCompanyComponent()
{
    let { id } = useParams();

    const [redirect, setRedirect] = useState(false);

    const [company, setCompany] = useState({
        id: id,
        name: '',
        shortName: '',
        description: '',
        numberOfPeople: 0,
        cityName: ''
    });

    function updateCompany(event) {
        event.preventDefault();
        let companyRequest = {
            description: company.description,
            numberOfPeople: company.numberOfPeople
        };

        CompanyService.updateCompany(companyRequest, company.id).then(res => {
            setRedirect(true);
        });
    }

    function changeDescriptionHandler(event)
    {
        event.preventDefault();
        setCompany({...company, description: event.target.value});
    };

    function changeNumberOfPeopleHandler(event)
    {
        event.preventDefault();
        setCompany({...company, numberOfPeople: event.target.value});
    }

    function cancel(event)
    {
        event.preventDefault();
        setRedirect(true);
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        CompanyService.getCompanyById(company.id).then((res) => {
            let companyResponse = res.data;
            setCompany({
                id: companyResponse.id,
                name: companyResponse.name,
                shortName: companyResponse.shortName,
                description: companyResponse.description,
                numberOfPeople: companyResponse.numberOfPeople,
                cityName: companyResponse.cityName
            });
        });
    }, []);

    const renderRedirected = () => {
        if (redirect) {
            return <Navigate to='/companies' />
        }
    }

    return (
        <div>
            {renderRedirected()}
            <div className = "update-company">
                <div className = "card col-md-6 offset-md-3 offset-md-3">
                    <h3 className="text-center">Update Company</h3>
                    <div className = "card-body">
                        <form onSubmit={updateCompany}>
                            <div className = "form-group">
                                <label>Id</label>
                                <input placeholder="Id" name="id" className="form-control" 
                                    value={company.id} disabled />
                            </div>
                            <div className = "form-group">
                                <label>Name</label>
                                <input placeholder="Name" name="name" className="form-control" 
                                    value={company.name} disabled />
                            </div>
                            <div className = "form-group">
                                <label>Short Name</label>
                                <input placeholder="Short Name" name="shortName" className="form-control" 
                                    value={company.shortName} disabled />
                            </div>
                            <div className = "form-group">
                                <label>Description</label>
                                <input placeholder="Description" name="description" className="form-control" 
                                    value={company.description} onChange={changeDescriptionHandler} />
                            </div>
                            <div className = "form-group">
                                <label>Number of People</label>
                                <input placeholder="Number of People" name="numberOfPeople" className="form-control" 
                                    value={company.numberOfPeople} onChange={changeNumberOfPeopleHandler} />
                            </div>
                            <div className = "form-group">
                                <label>City Name</label>
                                <input placeholder="City Name" name="city" className="form-control" 
                                    value={company.cityName} disabled />
                            </div>

                            <button type="submit" className="btn btn-success">Save</button>
                            <button className="btn btn-danger" onClick={cancel} style={{marginLeft: "10px"}}>Cancel</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
}