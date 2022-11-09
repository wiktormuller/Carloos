import React, { useState } from 'react';
import CompanyService from '../services/CompanyService'

function CreateCompanyComponent()
{
    const [company, setCompany] = useState({
        name: '',
        shortName: '',
        description: '',
        numberOfPeople: '',
        city: ''
    });

    saveCompany= (e) => {
        e.preventDefault();
        let companyRequest = {
            name: company.name,
            shortName: company.shortName,
            description: company.description,
            numberOfPeople: company.numberOfPeople,
            city: company.city
        };

        CompanyService.createCompany(companyRequest).then(res => {
            this.props.history.push('/companies');
        });
    }

    changeNameHandler= (event) => {
        setCompany({
            name: event.target.value
        });
    }

    changeShortNameHandler= (event) => {
        setCompany({
            shortName: event.target.value
        });
    }

    changeDescriptionHandler= (event) => {
        setCompany({
            description: event.target.value
        });
    }

    changeNumberOfPeopleHandler= (event) => {
        setCompany({
            numberOfPeople: event.target.value
        });
    }

    changeCityHandler= (event) => {
        setCompany({
            city: event.target.value
        });
    }

    cancel= () => {
        this.props.history.push('/companies');
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
                                            value={company.name} onChange={this.changeNameHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>Short Name:</label>
                                        <input placeholder="Short Name" name="shortName" className="form-control" 
                                            value={company.shortName} onChange={this.changeShortNameHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>Description:</label>
                                        <input placeholder="Description" name="description" className="form-control" 
                                            value={company.description} onChange={this.changeDescriptionHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>Number of People:</label>
                                        <input placeholder="Number of People" name="numberOfPeople" className="form-control" 
                                            value={company.numberOfPeople} onChange={this.changeNumberOfPeopleHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>City Name:</label>
                                        <input placeholder="City Name" name="city" className="form-control" 
                                            value={company.city} onChange={this.changeCityHandler}/>
                                    </div>

                                    <button className="btn btn-success" onClick={this.saveCompany}>Save</button>
                                    <button className="btn btn-danger" onClick={this.cancel.bind(this)} style={{marginLeft: "10px"}}>Cancel</button>
                                </form>
                            </div>
                        </div>
                    </div>

                </div>
        </div>
    );
}

export default CreateCompanyComponent;