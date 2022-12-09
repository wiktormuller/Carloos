import React, { useState, useEffect } from 'react';
import ProfileService from '../../../clients/ProfileService';
import CompanyService from '../../../clients/CompanyService';
import JobOfferService from '../../../clients/JobOfferService';
import { useNavigate } from 'react-router-dom';
import '../profile-styles.css';

export default function ProfileComponent()
{
    const [profile, setProfile] = useState({
        userId: 0,
        name: '',
        email: '',
        profileCompanies: []
    });
    const navigate = useNavigate();

    function deleteCompany(id) {
        CompanyService.deleteCompany(id).then(re => {
            ProfileService.getProfile().then(res => {
                setProfile(res.data);
            })
        });
    }

    function viewCompany(id) {
        navigate(`/companies/${id}`);
    }

    function editCompany(id) {
        navigate(`/companies/update/${id}`);
    }

    function addCompany(event) {
        event.preventDefault();
        navigate(`/companies/create`);
    }

    function deleteJobOffer(id) {
        JobOfferService.deleteJobOffer(id).then(re => {
            ProfileService.getProfile().then(res => {
                setProfile({
                    userId: res.data.userId,
                    name: res.data.name,
                    email: res.data.email,
                    profileCompanies: res.data.profileCompanies
                });
            })
        });
    }

    function viewJobOffer(id) {
        navigate(`/job-offers/${id}`);
    }

    function editJobOffer(id) {
        navigate(`/job-offers/update/${id}`);
    }

    function addJobOffer(event) {
        event.preventDefault();
        navigate(`/job-offers/create`);
    }

    function getMergedJobOffers()
    {
        var mergedJobOffers = [];

        for (var i = 0; i < profile.profileCompanies.length; i++)
        {
            for (var j = 0; j < profile.profileCompanies[i].companyJobOffers.length; j++)
            {
                mergedJobOffers.push({
                    companyId: profile.profileCompanies[i].companyId,
                    companyName: profile.profileCompanies[i].name,
                    jobOfferId: profile.profileCompanies[i].companyJobOffers[j].jobOfferId,
                    jobOfferName: profile.profileCompanies[i].companyJobOffers[j].name
                })
            }
        }

        console.log(mergedJobOffers);

        return mergedJobOffers;
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        ProfileService.getProfile().then(res => {
            setProfile(res.data);
        });
    }, []);

    return (
        <div className="profile">
            <div className="user-info">
                {
                    <div>
                        <h1>User Info:</h1>
                        <p>UserId: {profile.userId}</p>
                        <p>Username: {profile.name}</p>
                        <p>Email: {profile.email}</p>
                    </div>
                }
            </div>
            <div className="companies">
                <h2 className="text-center">Companies List</h2>
                <div className = "row">
                    <button className="btn btn-primary" onClick={addCompany}>Add Company</button>
                </div>
                <br></br>
                <div className = "row">
                        <table className = "table table-striped table-bordered">

                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Name</th>
                                    <th>Email</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    profile.profileCompanies.map(
                                        company => 
                                        <tr key = {company.companyId}>
                                            <td> {company.companyId} </td>
                                            <td> {company.name} </td>
                                            <td> {company.email} </td>
                                            <td>
                                                <button style={{marginBottom: "5px"}} onClick={ () => editCompany(company.companyId)} className="btn btn-info">Update</button>
                                                <button style={{marginBottom: "5px"}} onClick={ () => deleteCompany(company.companyId)} className="btn btn-danger">Delete</button>
                                                <button style={{marginBottom: "5px"}} onClick={ () => viewCompany(company.companyId)} className="btn btn-info">View</button>
                                            </td>
                                        </tr>
                                    )
                                }
                            </tbody>
                        </table>

                </div>

            </div>

            <div className="job-offers">
                <h2 className="text-center">Job Offers List</h2>
                <div className = "row">
                    <button className="btn btn-primary" onClick={addJobOffer}>Add Job Offer</button>
                </div>
                <br></br>
                <div className = "row">
                        <table className = "table table-striped table-bordered">

                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Name</th>
                                    <th>Company Id</th>
                                    <th>Company Name</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    getMergedJobOffers().map(
                                        jobOffer => 
                                        <tr key = {jobOffer.jobOfferId}>
                                            <td> {jobOffer.jobOfferId} </td>
                                            <td> {jobOffer.jobOfferName} </td>
                                            <td> {jobOffer.companyId} </td>
                                            <td> {jobOffer.companyName} </td>
                                            <td>
                                                <button style={{marginBottom: "5px"}} onClick={ () => editJobOffer(jobOffer.jobOfferId)} className="btn btn-info">Update Job Offer</button>
                                                <button style={{marginBottom: "5px"}} onClick={ () => deleteJobOffer(jobOffer.jobOfferId)} className="btn btn-danger">Delete Job Offer</button>
                                                <button style={{marginBottom: "5px"}} onClick={ () => viewJobOffer(jobOffer.jobOfferId)} className="btn btn-info">View Job Offer</button>
                                            </td>
                                        </tr>
                                    )
                                }
                            </tbody>
                        </table>

                </div>

            </div>
        </div>
    );
}