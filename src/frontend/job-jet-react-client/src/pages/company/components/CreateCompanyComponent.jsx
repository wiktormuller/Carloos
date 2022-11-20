import React, { useState } from 'react';
import CompanyService from '../services/CompanyService';
import { useNavigate } from 'react-router-dom';

export default function CreateCompanyComponent()
{
    const [company, setCompany] = useState({
        name: '',
        shortName: '',
        description: '',
        numberOfPeople: '',
        city: ''
    });

    const navigate = useNavigate();

    function saveCompany(e)
    {
        e.preventDefault();
        let companyRequest = {
            name: company.name,
            shortName: company.shortName,
            description: company.description,
            numberOfPeople: company.numberOfPeople,
            city: company.city
        };

        CompanyService.createCompany(companyRequest).then(res => {
            navigate('/companies');
        });
    }

    function changeNameHandler(event)
    {
        setCompany({
            name: event.target.value
        });
    }

    function changeShortNameHandler(event)
    {
        setCompany({
            shortName: event.target.value
        });
    }

    function changeDescriptionHandler(event)
    {
        setCompany({
            description: event.target.value
        });
    }

    function changeNumberOfPeopleHandler(event)
    {
        setCompany({
            numberOfPeople: event.target.value
        });
    }

    function changeCityHandler(event)
    {
        setCompany({
            city: event.target.value
        });
    }

    function cancel()
    {
        navigate('/companies');
    }

    return (
        <div>
            <br></br>
                <div className = "container">
                    <div className = "row">
                        <div className = "card col-md-6 offset-md-3 offset-md-3">
                            <h3 className="text-center">Add Company</h3>
                            <div className = "card-body">
                                <form>
                                    <div className = "form-group">
                                        <label>Name:</label>
                                        <input placeholder="Name" name="name" className="form-control" 
                                            value={company.name} onChange={changeNameHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>Short Name:</label>
                                        <input placeholder="Short Name" name="shortName" className="form-control" 
                                            value={company.shortName} onChange={changeShortNameHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>Description:</label>
                                        <input placeholder="Description" name="description" className="form-control" 
                                            value={company.description} onChange={changeDescriptionHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>Number of People:</label>
                                        <input placeholder="Number of People" name="numberOfPeople" className="form-control" 
                                            value={company.numberOfPeople} onChange={changeNumberOfPeopleHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>City Name:</label>
                                        <input placeholder="City Name" name="city" className="form-control" 
                                            value={company.city} onChange={changeCityHandler}/>
                                    </div>

                                    <button className="btn btn-success" onClick={saveCompany}>Save</button>
                                    <button className="btn btn-danger" onClick={cancel} style={{marginLeft: "10px"}}>Cancel</button>
                                </form>
                            </div>
                        </div>
                    </div>

                </div>
        </div>
    );
}