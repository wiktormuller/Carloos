import React, { useState, useEffect } from 'react';
import CompanyService from '../services/CompanyService'
import { useParams, useNavigate } from 'react-router-dom';

export default function UpdateCompanyComponent()
{
    const { id } = useParams();
    const navigate = useNavigate();
    const [company, setCompany] = useState({
        id: id,
        name: '',
        shortName: '',
        description: null,
        numberOfPeople: 0,
        city: ''
    });

    function updateCompany(e) {
        e.preventDefault();
        let companyRequest = {
            description: company.description,
            numberOfPeople: company.numberOfPeople
        };

        CompanyService.updateCompany(companyRequest, company.id).then(res => {
            navigate('/companies');
        });
    }

    function changeDescriptionHandler(event)
    {
        setCompany({description: event.target.value});
    };

    function changeNumberOfPeopleHandler(event)
    {
        setCompany({numberOfPeople: event.target.value});
    }

    function cancel()
    {
        navigate('/companies');
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        CompanyService.getCompanyById(company.id).then((res) => {
            let companyResponse = res.data;
            setCompany({
                name: companyResponse.name,
                shortName: companyResponse.shortName,
                description: companyResponse.description,
                numberOfPeople: companyResponse.numberOfPeople,
                city: companyResponse.city
            });
        });
    });

    return (
        <div>
            <br></br>
                <div className = "container">
                    <div className = "row">
                        <div className = "card col-md-6 offset-md-3 offset-md-3">
                            <h3 className="text-center">Update Company</h3>
                            <div className = "card-body">
                                <form>
                                    <div className = "form-group">
                                        <label>Description</label>
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
                                            value={company.description} onChange={changeDescriptionHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>Number of People</label>
                                        <input placeholder="Number of People" name="numberOfPeople" className="form-control" 
                                            value={company.numberOfPeople} onChange={changeNumberOfPeopleHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>City Name</label>
                                        <input placeholder="City Name" name="city" className="form-control" 
                                            value={company.city} disabled/>
                                    </div>

                                    <button className="btn btn-success" onClick={updateCompany}>Save</button>
                                    <button className="btn btn-danger" onClick={cancel()} style={{marginLeft: "10px"}}>Cancel</button>
                                </form>
                            </div>
                        </div>
                    </div>

                </div>
        </div>
    );
}