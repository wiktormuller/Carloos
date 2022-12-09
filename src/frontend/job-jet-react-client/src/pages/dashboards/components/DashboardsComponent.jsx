import "../dashboard-styles.css";
import React, { useState, useEffect } from 'react';
import { Chart } from 'react-chartjs-2';
import 'chart.js/auto';
import DashboardService from '../../../clients/DashboardService';

export default function DashboardsComponent(props) {
    const [averageSalariesForCountries, setAverageSalariesForCountries] = useState([]);
    const [averageSalariesForSeniorityLevels, setAverageSalariesForSeniorityLevels] = useState([]);
    const [averageSalariesForTechnologies, setAverageSalariesForTechnologies] = useState([]);

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        DashboardService.getAverageSalariesForCountries().then((res) => {
            setAverageSalariesForCountries(
                res.data
            )
        });

        DashboardService.getAverageSalariesForSeniorityLevels().then((res) => {
            setAverageSalariesForSeniorityLevels(
                res.data
            )
        });

        DashboardService.getAverageSalariesForTechnologies().then((res) => {
            setAverageSalariesForTechnologies(
                res.data
            )
        });
    }, []);

    function mapAverageSalariesForCountries(arr) {
        var labels = arr.map((item) => (item.countryName));
        var dataSetFrom = arr.map((item) => (item.averageSalaryFrom));
        var dataSetTo = arr.map((item) => (item.averageSalaryTo));
        var dataSetAverage = arr.map((item) => (item.averageSalary));

        return {
            labels: labels,
            datasets: [
                {
                    id: 1,
                    label: 'Average Salary From',
                    data: dataSetFrom,
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1
                },
                {
                    id: 2,
                    label: 'Average Salary To',
                    data: dataSetTo,
                    backgroundColor: 'rgba(23, 109, 195, 0.2)',
                    borderColor: 'rgba(53, 109, 195, 1)',
                    borderWidth: 1
                },
                {
                    id: 3,
                    label: 'Average Salary',
                    data: dataSetAverage,
                    backgroundColor: 'rgba(255, 169, 0, 0.2)',
                    borderColor: 'rgba(220, 100, 0, 0.2)',
                    borderWidth: 1
                }
            ]
        }
    }

    function mapAverageSalariesForSeniorityLevels(arr) {
        var labels = arr.map((item) => (item.seniorityLevelName));
        var dataSetFrom = arr.map((item) => (item.averageSalaryFrom));
        var dataSetTo = arr.map((item) => (item.averageSalaryTo));
        var dataSetAverage = arr.map((item) => (item.averageSalary));

        return {
            labels: labels,
            datasets: [
                {
                    id: 1,
                    label: 'Average Salary From',
                    data: dataSetFrom,
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1
                },
                {
                    id: 2,
                    label: 'Average Salary To',
                    data: dataSetTo,
                    backgroundColor: 'rgba(23, 109, 195, 0.2)',
                    borderColor: 'rgba(53, 109, 195, 1)',
                    borderWidth: 1
                },
                {
                    id: 3,
                    label: 'Average Salary',
                    data: dataSetAverage,
                    backgroundColor: 'rgba(255, 169, 0, 0.2)',
                    borderColor: 'rgba(220, 100, 0, 0.2)',
                    borderWidth: 1
                }
            ]
        }
    }

    function mapAverageSalariesForTechnologies(arr) {
        var labels = arr.map((item) => (item.technologyTypeName));
        var dataSetFrom = arr.map((item) => (item.averageSalaryFrom));
        var dataSetTo = arr.map((item) => (item.averageSalaryTo));
        var dataSetAverage = arr.map((item) => (item.averageSalary));

        return {
            labels: labels,
            datasets: [
                {
                    id: 1,
                    label: 'Average Salary From',
                    data: dataSetFrom,
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1
                },
                {
                    id: 2,
                    label: 'Average Salary To',
                    data: dataSetTo,
                    backgroundColor: 'rgba(23, 109, 195, 0.2)',
                    borderColor: 'rgba(53, 109, 195, 1)',
                    borderWidth: 1
                },
                {
                    id: 3,
                    label: 'Average Salary',
                    data: dataSetAverage,
                    backgroundColor: 'rgba(255, 169, 0, 0.2)',
                    borderColor: 'rgba(220, 100, 0, 0.2)',
                    borderWidth: 1
                }
            ]
        }
    }

    return (
        <div className="dashboards">
            <div className="dashboard-row">
                <h1 className="dashboard-header">Average Salaries For Countries</h1>
                <Chart className="dashboard-chart" type="radar" data={mapAverageSalariesForCountries(averageSalariesForCountries)} />
            </div>
            <div className="dashboard-row">
                <h1 className="dashboard-header">Average Salaries For Seniority Levels</h1>
                <Chart className="dashboard-chart" type="radar" data={mapAverageSalariesForSeniorityLevels(averageSalariesForSeniorityLevels)} />
            </div>
            <div className="dashboard-row">
                <h1 className="dashboard-header">Average Salaries For Technologies</h1>
                <Chart className="dashboard-chart" type="radar" data={mapAverageSalariesForTechnologies(averageSalariesForTechnologies)} />
            </div>
        </div>
    );
}