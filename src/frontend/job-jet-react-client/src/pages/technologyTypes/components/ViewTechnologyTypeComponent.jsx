import React, { useState, useEffect } from 'react';
import TechnologyTypeService from '../../../clients/TechnologyTypeService';
import { useParams } from 'react-router-dom';

export default function ViewTechnologyTypeComponent(props)
{
    const { id } = useParams();
    const[technologyTypeState, setTechnologyTypeState] = useState({
        technologyType: {}
    });

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        TechnologyTypeService.getTechnologyTypeById(id).then((res) => {
            setTechnologyTypeState({
                technologyType: res.data
            })
        });
    });

    return (
        <div className="technology-types">
            <h3 className = "text-center"> View Technology Type Details</h3>
            <div className = "card col-md-6 offset-md-3">
                <div className = "card-body">
                    <div className = "row">
                        <label>Id</label>
                        <p> { technologyTypeState.technologyType.id }</p>
                    </div>
                    <div className = "row">
                        <label>Name</label>
                        <p> { technologyTypeState.technologyType.name }</p>
                    </div>
                </div>

            </div>
        </div>
    );
}