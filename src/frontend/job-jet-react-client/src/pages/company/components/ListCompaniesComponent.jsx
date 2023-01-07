import React, { useState, useEffect } from 'react';
import CompanyService from '../../../clients/CompanyService';
import { useNavigate } from 'react-router-dom';
import "../company-styles.css";

export default function ListCompaniesComponent(props)
{
    const [companies, setCompanies] = useState([]);
    const navigate = useNavigate();

    function viewCompany(id) {
        navigate(`/companies/${id}`);
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        CompanyService.getCompanies().then(res => {
            setCompanies(res.data.response.data);
        });
    }, []);

    return (
        <div className="companies">
             <h2 className="text-center">Companies List</h2>
             <br></br>
             <div className = "row">
                    <table className = "table table-striped table-bordered">

                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Name</th>
                                <th>Short Name</th>
                                <th>Description</th>
                                <th>Number of People</th>
                                <th>City Name</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                companies.map(
                                    company => 
                                    <tr key = {company.id}>
                                         <td> {company.id} </td>
                                         <td> {company.name} </td>   
                                         <td> {company.shortName}</td>
                                         <td> {company.description}</td>
                                         <td> {company.numberOfPeople} </td>  
                                         <td> {company.cityName} </td>
                                         <td>
                                             <button style={{marginBottom: "5px"}} onClick={ () => viewCompany(company.id)} className="btn btn-info">View</button>
                                         </td>
                                    </tr>
                                )
                            }
                        </tbody>
                    </table>

             </div>

        </div>
    )
}