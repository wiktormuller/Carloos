import React, { useState } from 'react';
import TechnologyTypeService from '../services/TechnologyTypeService'

function CreateTechnologyTypeComponent()
{
    const [technologyType, setTechnologyType] = useState({
        name: ''
    });

    saveTechnologyType= (e) => {
        e.preventDefault();
        let technologyTypeRequest = {
            name: technologyType.name
        };

        TechnologyTypeService.createTechnologyType(technologyTypeRequest).then(res => {
            this.props.history.push('/technology-types');
        });
    }

    changeNameHandler= (event) => {
        setTechnologyType({
            name: event.target.value
        });
    }

    function cancel() {
        this.props.history.push('/technology-types');
    }

    return (
        <div>
            <br></br>
                <div className = "container">
                    <div className = "row">
                        <div className = "card col-md-6 offset-md-3 offset-md-3">
                            <h3 className="text-center">Add Technology Type</h3>
                            <div className = "card-body">
                                <form>
                                    <div className = "form-group">
                                        <label>Name:</label>
                                        <input placeholder="Name" name="name" className="form-control" 
                                            value={technologyType.name} onChange={this.changeNameHandler}/>
                                    </div>

                                    <button className="btn btn-success" onClick={this.saveTechnologyType}>Save</button>
                                    <button className="btn btn-danger" onClick={this.cancel.bind(this)} style={{marginLeft: "10px"}}>Cancel</button>
                                </form>
                            </div>
                        </div>
                    </div>

                </div>
        </div>
    );
}

export default CreateTechnologyTypeComponent;