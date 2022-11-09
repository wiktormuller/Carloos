import React, { useState } from 'react';
import TechnologyTypeService from '../services/TechnologyTypeService'

function ListTechnologyTypesComponent(props)
{
    const [technologyTypes, setTechnologyTypes] = useState([]);

    function deleteTechnologyType(id) {
        TechnologyTypeService.deleteTechnologyType(id).then(re => {
            setTechnologyTypes(TechnologyTypeService.getTechnologyTypes())
        });
    }

    function viewTechnologyType(id) {
        this.props.history.push(`/view-technology-types/${id}`);
    }

    function editTechnologyType(id) {
        this.props.history.push(`/edit-technology-types/${id}`);
    }

    function addTechnologyType() {
        this.props.history.push(`/add-technology-types`);
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
                <button className="btn btn-primary" onClick={this.addTechnologyType}>Add technology type</button>
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
                                         <td> {technologyType.name} </td>
                                         <td>
                                             <button onClick={ () => this.editTechnologyType(technologyType.id)} className="btn btn-info">Update</button>
                                             <button style={{marginLeft: "10px"}} onClick={ () => this.deleteTechnologyType(technologyType.id)} className="btn btn-danger">Delete</button>
                                             <button style={{marginLeft: "10px"}} onClick={ () => this.viewTechnologyType(technologyType.id)} className="btn btn-info">View</button>
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

export default ListTechnologyTypesComponent;