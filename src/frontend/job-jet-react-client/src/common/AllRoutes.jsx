import React from 'react';
import { Routes, Route } from 'react-router-dom';
import './routes-styles.css';

import LandingPageComponent from '../pages/landingPage/components/landingPage/LandingPageComponent';
import PageNotFoundComponent from '../pages/pageNotFound/components/PageNotFoundComponent';
import AccessDeniedComponent from '../pages/accessDenied/components/AccessDeniedComponent';

import LoginComponent from '../pages/login/components/LoginComponent';
import RegisterComponent from '../pages/register/components/RegisterComponent';
import ContactComponent from '../pages/contact/components/ContactComponent';
import ProfileComponent from '../pages/profile/components/ProfileComponent';

import DashboardsComponent from '../pages/dashboards/components/DashboardsComponent';

import CreateTechnologyTypeComponent from '../pages/technologyTypes/components/CreateTechnologyTypeComponent';
import ListTechnologyTypesComponent from '../pages/technologyTypes/components/ListTechnologyTypesComponent';
import ViewTechnologyTypeComponent from '../pages/technologyTypes/components/ViewTechnologyTypeComponent';
import UpdateTechnologyTypeComponent from '../pages/technologyTypes/components/UpdateTechnologyTypeComponent';

import CreateCompanyComponent from '../pages/company/components/CreateCompanyComponent';
import ListCompaniesComponent from '../pages/company/components/ListCompaniesComponent';
import ViewCompanyComponent from '../pages/company/components/ViewCompanyComponent';
import UpdateCompanyComponent from '../pages/company/components/UpdateCompanyComponent';

import CreateJobOfferComponent from '../pages/jobOffer/components/CreateJobOfferComponent';
import ViewJobOfferComponent from '../pages/jobOffer/components/ViewJobOfferComponent';
import UpdateJobOfferComponent from '../pages/jobOffer/components/UpdateJobOfferComponent';

import CreateRoleComponent from '../pages/roles/components/CreateRoleComponent';
import ListRolesComponent from '../pages/roles/components/ListRolesComponent';
import ViewRoleComponent from '../pages/roles/components/ViewRoleComponent';

import ListUsersComponent from '../pages/users/components/ListUsersComponent';
import ViewUserComponent from '../pages/users/components/ViewUserComponent';
import UpdateUserComponent from '../pages/users/components/UpdateUserComponent';

import NavbarComponent from '../pages/navbar/components/NavbarComponent';
import HeaderComponent from '../pages/header/components/HeaderComponent';
import FooterComponent from '../pages/footer/components/FooterComponent';

export const AllRoutes = () => {
	return (
        <div>
            <HeaderComponent />
            
            <div className="content">
                <NavbarComponent />
                <Routes className="routes">
                    <Route path='/' exact element={<LandingPageComponent />} />
                    <Route path='*' exact element={<PageNotFoundComponent />} />
                    <Route path="/access-denied" exact element={<AccessDeniedComponent />} />

                    <Route path='/login' exact element={<LoginComponent />} />
                    <Route path='/register' exact element={<RegisterComponent />} />
                    <Route path='/contact' exact element={<ContactComponent />} />

                    <Route path='/technology-types/create' exact element={<CreateTechnologyTypeComponent />} />
                    <Route path='/technology-types' exact element={<ListTechnologyTypesComponent />} />
                    <Route path='/technology-types/:id' exact element={<ViewTechnologyTypeComponent />} />
                    <Route path='/technology-types/update/:id' exact element={<UpdateTechnologyTypeComponent />} />

                    <Route path='/companies/create' exact element={<CreateCompanyComponent />} />
                    <Route path='/companies' exact element={<ListCompaniesComponent />} />
                    <Route path='/companies/:id' exact element={<ViewCompanyComponent />} />
                    <Route path='/companies/update/:id' exact element={<UpdateCompanyComponent />} />

                    <Route path='/dashboards' exact element={<DashboardsComponent />} />

                    <Route path='/job-offers/create' exact element={<CreateJobOfferComponent />} />
                    <Route path='/job-offers/:id' exact element={<ViewJobOfferComponent />} />
                    <Route path='/job-offers/update/:id' exact element={<UpdateJobOfferComponent />} />

                    <Route path='/roles/create' exact element={<CreateRoleComponent />} />
                    <Route path='/roles' exact element={<ListRolesComponent />} />
                    <Route path='/roles/:id' exact element={<ViewRoleComponent />} />

                    <Route path='/users' exact element={<ListUsersComponent />} />
                    <Route path='/users/update/:id' exact element={<UpdateUserComponent />} />
                    <Route path='/users/:id' exact element={<ViewUserComponent />} />

                    <Route path='/profile' exact element={<ProfileComponent />} />
                </Routes>
            </div>
            <FooterComponent />
        </div>
	);
};