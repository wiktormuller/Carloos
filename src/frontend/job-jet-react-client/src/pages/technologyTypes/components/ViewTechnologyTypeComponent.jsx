import React, { useState, useEffect } from 'react';
import TechnologyTypeService from '../../../clients/TechnologyTypeService';
import { useParams } from 'react-router-dom';

export default function ViewTechnologyTypeComponent(props)
{
    const { id } = useParams();
    const[technologyTypeState, setTechnologyTypeState] = useState({
        id: id,
        technologyType: {}
    });

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        TechnologyTypeService.getTechnologyTypeById(technologyTypeState.id).then((res) => {
            setTechnologyTypeState({
                technologyType: res.data
            })
        });
    });

    return (
        <div>
            <br></br>
            <div className = "card col-md-6 offset-md-3">
                <h3 className = "text-center"> View Technology Type Details</h3>
                <div className = "card-body">
                    <div className = "row">
                        <label>Id:</label>
                        <div> { technologyTypeState.id }</div>
                    </div>
                    <div className = "row">
                        <label>Name:</label>
                        <div> { technologyTypeState.technologyType.name }</div>
                    </div>
                </div>

            </div>
        </div>
    );
}