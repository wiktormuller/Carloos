import "./dashboard-styles.css";
import { React } from "react";
import { Chart } from 'react-chartjs-2';
import 'chart.js/auto';

export function Dashboard(props) {
    const chartData = {
        labels: ['Running', 'Swimming', 'Eating', 'Cycling'],
        datasets: [{
            data: [20, 10, 4, 2]
        }]
    }

    return (
        <div className="dashboard">
            <h1>Dashboard</h1>
            <Chart type="radar" data={chartData}/>
        </div>
    );
};
