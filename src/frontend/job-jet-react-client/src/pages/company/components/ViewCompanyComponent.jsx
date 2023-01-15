import React, { useState, useEffect } from 'react';
import CompanyService from '../../../clients/CompanyService'
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
        <div className="companies">
            <h3 className = "text-center"> View Company Details</h3>
            <div className = "card col-md-6 offset-md-3">
                <div className = "card-body">
                    <div className = "row">
                        <label>Id</label>
                        <p>{ companyState.company.id }</p>
                    </div>
                    <div className = "row">
                        <label>Name</label>
                        <p>{ companyState.company.name }</p>
                    </div>
                    <div className = "row">
                        <label>Short Name</label>
                        <p>{ companyState.company.shortName }</p>
                    </div>
                    <div className = "row">
                        <label>Description</label>
                        <p>{ companyState.company.description }</p>
                    </div>
                    <div className = "row">
                        <label>Number of People</label>
                        <p>{ companyState.company.numberOfPeople }</p>
                    </div>
                    <div className = "row">
                        <label>City Name</label>
                        <p>{ companyState.company.cityName }</p>
                    </div>
                </div>

            </div>
        </div>
    );
}