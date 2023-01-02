import React, { useState } from 'react';
import TechnologyTypeService from '../../../clients/TechnologyTypeService';
import { useNavigate } from "react-router-dom";

export default function CreateTechnologyTypeComponent()
{
    const [technologyType, setTechnologyType] = useState({
        name: ''
    });

    const navigate = useNavigate();

    function saveTechnologyType()
    {
        let technologyTypeRequest = {
            name: technologyType.name
        };

        TechnologyTypeService.createTechnologyType(technologyTypeRequest).then(res => {
            navigate(`/technology-types`);
        });
    }

    function changeNameHandler(event)
    {
        setTechnologyType({
            name: event.target.value
        });
    }

    function cancel(event)
    {
        event.preventDefault();
        navigate(`/technology-types`);
    }

    return (
        <div className="technology-types">
            <div className = "container">
                <div className = "row">
                    <div className = "card col-md-6 offset-md-3 offset-md-3">
                        <h3 className="text-center">Add Technology Type</h3>
                        <div className = "card-body">
                            <form>
                                <div className = "form-group">
                                    <label>Name:</label>
                                    <input placeholder="Name" name="name" className="form-control" 
                                        value={technologyType.name} onChange={changeNameHandler}/>
                                </div>

                                <button className="btn btn-success" onClick={saveTechnologyType()}>Save</button>
                                <button className="btn btn-danger" onClick={cancel} style={{marginLeft: "10px"}}>Cancel</button>
                            </form>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    );
}