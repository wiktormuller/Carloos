import React, { useState } from 'react';
import CompanyService from '../services/CompanyService'

function UpdateCompanyComponent(props)
{
    const [company, setCompany] = useState({
        id: this.props.match.params.id,
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
            this.props.history.push('/companies');
        });
    }

    changeDescriptionHandler= (event) => {
        setCompany({description: event.target.value});
    };

    changeNumberOfPeopleHandler= (event) => {
        setCompany({numberOfPeople: e.target.value});
    }

    cancel= () => {
        this.props.history.push('/companies');
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
                                            value={company.id} readonly />
                                    </div>
                                    <div className = "form-group">
                                        <label>Name</label>
                                        <input placeholder="Name" name="name" className="form-control" 
                                            value={company.name} readonly />
                                    </div>
                                    <div className = "form-group">
                                        <label>Short Name</label>
                                        <input placeholder="Short Name" name="shortName" className="form-control" 
                                            value={company.shortName} readonly />
                                    </div>
                                    <div className = "form-group">
                                        <label>Description</label>
                                        <input placeholder="Description" name="description" className="form-control" 
                                            value={company.description} onChange={this.changeDescriptionHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>Number of People</label>
                                        <input placeholder="Number of People" name="numberOfPeople" className="form-control" 
                                            value={company.numberOfPeople} onChange={this.changeNumberOfPeopleHandler}/>
                                    </div>
                                    <div className = "form-group">
                                        <label>City Name</label>
                                        <input placeholder="City Name" name="city" className="form-control" 
                                            value={company.city} readonly/>
                                    </div>

                                    <button className="btn btn-success" onClick={this.updateCompany}>Save</button>
                                    <button className="btn btn-danger" onClick={this.cancel.bind(this)} style={{marginLeft: "10px"}}>Cancel</button>
                                </form>
                            </div>
                        </div>
                    </div>

                </div>
        </div>
    );
}

export default UpdateCompanyComponent;