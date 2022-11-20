import React, { useState, useEffect } from 'react';
import TechnologyTypeService from '../services/TechnologyTypeService';
import { useNavigate } from "react-router-dom";

export default function ListTechnologyTypesComponent()
{
    const [technologyTypes, setTechnologyTypes] = useState([]);
    const navigate = useNavigate();

    function deleteTechnologyType(id) {
        TechnologyTypeService.deleteTechnologyType(id).then(re => {
            setTechnologyTypes(TechnologyTypeService.getTechnologyTypes())
        });
    }

    function viewTechnologyType(id) {
        navigate(`/technology-types/${id}`);
    }

    function editTechnologyType(id) {
        navigate(`/technology-types/update/${id}`);
    }

    function addTechnologyType() {
        navigate(`/technology-types/create`);
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        TechnologyTypeService.getTechnologyTypes().then(res => {
            setTechnologyTypes(res.data);
        });
    });

    return (
        <div>
             <h2 className="text-center">Technology Types List</h2>
             <div className = "row">
                <button className="btn btn-primary" onClick={addTechnologyType}>Add technology Type</button>
             </div>
             <br></br>
             <div className = "row">
                    <table className = "table table-striped table-bordered">

                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Name</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                technologyTypes.map(
                                    technologyType => 
                                    <tr key = {technologyType.id}>
                                         <td> {technologyType.id} </td>
                                         <td> {technologyType.name} </td>
                                         <td>
                                             <button onClick={ () => editTechnologyType(technologyType.id)} className="btn btn-info">Update</button>
                                             <button style={{marginLeft: "10px"}} onClick={ () => deleteTechnologyType(technologyType.id)} className="btn btn-danger">Delete</button>
                                             <button style={{marginLeft: "10px"}} onClick={ () => viewTechnologyType(technologyType.id)} className="btn btn-info">View</button>
                                         </td>
                                    </tr>
                                )
                            }
                        </tbody>
                    </table>

             </div>

        </div>
    )
}