import React, { useState } from 'react';
import TechnologyTypeService from '../services/TechnologyTypeService'

function ViewTechnologyTypeComponent(props)
{
    const[technologyTypeState, setTechnologyTypeState] = useState({
        id: this.props.match.params.id,
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

export default ViewTechnologyTypeComponent;