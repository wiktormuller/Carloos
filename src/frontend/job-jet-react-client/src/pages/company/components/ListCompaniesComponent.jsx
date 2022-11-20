import React, { useState, useEffect } from 'react';
import CompanyService from '../services/CompanyService'
import { useNavigate } from 'react-router-dom';

export default function ListCompaniesComponent(props)
{
    const [companies, setCompanies] = useState([]);
    const navigate = useNavigate();

    function deleteCompany(id) {
        CompanyService.deleteCompany(id).then(re => {
            setCompanies(CompanyService.getCompanies())
        });
    }

    function viewCompany(id) {
        navigate(`companies/${id}`);
    }

    function editCompany(id) {
        navigate(`companies/update/${id}`);
    }

    function addCompany(event) {
        event.preventDefault();
        navigate(`companies/create`);
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        CompanyService.getCompanies().then(res => {
            setCompanies(res.data.response.data);
        });
        
        console.log(companies);
    });

    return (
        <div>
             <h2 className="text-center">Companies List</h2>
             <div className = "row">
                <button className="btn btn-primary" onClick={addCompany}>Add company</button>
             </div>
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
                                         <td> {company.shotName}</td>
                                         <td> {company.description}</td>
                                         <td> {company.numberOfPeople} </td>  
                                         <td> {company.city} </td>
                                         <td>
                                             <button onClick={ () => editCompany(company.id)} className="btn btn-info">Update</button>
                                             <button style={{marginLeft: "10px"}} onClick={ () => this.deleteCompany(company.id)} className="btn btn-danger">Delete</button>
                                             <button style={{marginLeft: "10px"}} onClick={ () => this.viewCompany(company.id)} className="btn btn-info">View</button>
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