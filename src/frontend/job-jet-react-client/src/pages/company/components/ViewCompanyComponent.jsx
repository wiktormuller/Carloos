import React, { useState, useEffect } from 'react';
import CompanyService from '../services/CompanyService'
import { useParams } from 'react-router-dom';

export default function ViewCompanyComponent()
{
    const { id } = useParams();

    const[companyState, setCompanyState] = useState({
        id: id,
        company: {}
    });

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        CompanyService.getCompanyById(companyState.id).then((res) => {
            setCompanyState({
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
                        <div> { companyState.company.id }</div>
                    </div>
                    <div className = "row">
                        <label>Name:</label>
                        <div> { companyState.company.name }</div>
                    </div>
                    <div className = "row">
                        <label>Short Name:</label>
                        <div> { companyState.company.shortName }</div>
                    </div>
                    <div className = "row">
                        <label>Description:</label>
                        <div> { companyState.company.description }</div>
                    </div>
                    <div className = "row">
                        <label>Number of People:</label>
                        <div> { companyState.company.numberOfPeople }</div>
                    </div>
                    <div className = "row">
                        <label>City Name:</label>
                        <div> { companyState.company.city }</div>
                    </div>
                </div>

            </div>
        </div>
    );
}