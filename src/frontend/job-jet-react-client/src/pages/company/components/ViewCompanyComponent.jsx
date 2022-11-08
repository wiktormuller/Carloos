import React, { useState } from 'react';
import CompanyService from '../services/CompanyService'

function CreateCompanyComponent(props)
{
    const[company, setCompany] = useState({
        id: this.props.match.params.id,
        company: {}
    });

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        CompanyService.getCompanyById(company.id).then((res) => {
            setCompany({
                company: res.data
            })
        });
    });

    return (
        <div>
            <br></br>
            <div className = "card col-md-6 offset-md-3">
                <h3 className = "text-center"> View Company Details</h3>
                <div className = "card-body">
                    <div className = "row">
                        <label>Id:</label>
                        <div> { company.id }</div>
                    </div>
                    <div className = "row">
                        <label>Name:</label>
                        <div> { company.name }</div>
                    </div>
                    <div className = "row">
                        <label>Short Name:</label>
                        <div> { company.shortName }</div>
                    </div>
                    <div className = "row">
                        <label>Description:</label>
                        <div> { company.description }</div>
                    </div>
                    <div className = "row">
                        <label>Number of People:</label>
                        <div> { company.numberOfPeople }</div>
                    </div>
                    <div className = "row">
                        <label>City Name:</label>
                        <div> { company.city }</div>
                    </div>
                </div>

            </div>
        </div>
    );
}

export default CreateCompanyComponent;