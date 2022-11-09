import "./dashboard-styles.css";
import { React, useState } from "react";
import { Chart } from 'react-chartjs-2';
import 'chart.js/auto';
import DashboardService from '../services/DashboardService'

function DashboardViewComponent(props) {
    const [averageSalariesForCountries, setAverageSalariesForCountries] = useState({});
    const [averageSalariesForSeniorityLevels, setAverageSalariesForSeniorityLevels] = useState({});
    const [averageSalariesForTechnologies, setAverageSalariesForTechnologies] = useState({});

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
    });

    function mapAverageSalariesForCountries(arr) {
        var labels = arr.map((item) => {item.countryName});
        var dataSetFrom = arr.map((item) => {item.averageSalaryFrom});
        var dataSetTo = arr.map((item) => {item.averageSalaryTo});
        var dataSetAverage = arr.map((item) => {item.averageSalary});

        return {
            labels: labels,
            datasets: [
                {
                    data: dataSetFrom
                },
                {
                    data: dataSetTo
                },
                {
                    data: dataSetAverage
                }
            ]
        }
    }

    function mapAverageSalariesForSeniorityLevels(arr) {
        var labels = arr.map((item) => {item.seniorityLevelName});
        var dataSetFrom = arr.map((item) => {item.averageSalaryFrom});
        var dataSetTo = arr.map((item) => {item.averageSalaryTo});
        var dataSetAverage = arr.map((item) => {item.averageSalary});

        return {
            labels: labels,
            datasets: [
                {
                    data: dataSetFrom
                },
                {
                    data: dataSetTo
                },
                {
                    data: dataSetAverage
                }
            ]
        }
    }

    function mapAverageSalariesForTechnologies(arr) {
        var labels = arr.map((item) => {item.technologyTypeName});
        var dataSetFrom = arr.map((item) => {item.averageSalaryFrom});
        var dataSetTo = arr.map((item) => {item.averageSalaryTo});
        var dataSetAverage = arr.map((item) => {item.averageSalary});

        return {
            labels: labels,
            datasets: [
                {
                    data: dataSetFrom
                },
                {
                    data: dataSetTo
                },
                {
                    data: dataSetAverage
                }
            ]
        }
    }

    return (
        <div className="dashboards">
            <div className="dashboard-row">
                <h1>Average Salaries For Countries</h1>
                <Chart type="radar" data={mapAverageSalariesForCountries(averageSalariesForCountries)} />
            </div>
            <div className="dashboard-row">
                <h1>Average Salaries For Seniority Levels</h1>
                <Chart type="radar" data={mapAverageSalariesForSeniorityLevels(averageSalariesForSeniorityLevels)} />
            </div>
            <div className="dashboard-row">
                <h1>Average Salaries For Technologies</h1>
                <Chart type="radar" data={mapAverageSalariesForTechnologies(averageSalariesForTechnologies)} />
            </div>
        </div>
    );
};
