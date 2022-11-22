import React, { useState, useEffect } from 'react';
import TechnologyTypeService from '../services/TechnologyTypeService';
import { Navigate, useParams } from "react-router-dom";
import '../update-technology-type-styles.css';

export default function UpdateTechnologyTypeComponent(props)
{
    const { id } = useParams();
    
    const [redirect, setRedirect] = useState(false);

    const [technologyType, setTechnologyType] = useState({
        id: id,
        name: ''
    });

    function updateTechnologyType(e) {
        e.preventDefault();
        let technologyTypeRequest = {
            name: technologyType.name
        };

        TechnologyTypeService.updateTechnologyType(technologyTypeRequest, technologyType.id).then(res => {
            setRedirect(true);
        });
    }

    function changeNameHandler(event)
    {
        event.preventDefault();
        setTechnologyType({...technologyType, name: event.target.value});
    };

    function cancel(event)
    {
        event.preventDefault();
        setRedirect(true);
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        TechnologyTypeService.getTechnologyTypeById(technologyType.id).then((res) => {
            let technologyTypeResponse = res.data;
            setTechnologyType({
                name: technologyTypeResponse.name
            });
        });
    }, []);

    const renderRedirected = () => {
        if (redirect) {
            return <Navigate to='/technology-types' />
        }
    }

    return (
        <div>
            {renderRedirected()}
            <div className = "update-technology-type">
                <div className = "card col-md-6 offset-md-3 offset-md-3">
                    <h3 className="text-center">Update Technology Type</h3>
                    <div className = "card-body">
                        <form onSubmit={updateTechnologyType}>
                            <div className = "form-group">
                                <label>Name</label>
                                <input placeholder="Name" name="name" className="form-control" 
                                    value={technologyType.name} onChange={changeNameHandler}/>
                            </div>

                            <button type="submit" className="btn btn-success">Save</button>
                            <button className="btn btn-danger" onClick={cancel} style={{marginLeft: "10px"}}>Cancel</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
}