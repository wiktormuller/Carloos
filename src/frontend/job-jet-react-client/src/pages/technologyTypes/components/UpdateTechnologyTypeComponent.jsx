import React, { useState } from 'react';
import TechnologyTypeService from '../services/TechnologyTypeService'

function UpdateTechnologyTypeComponent(props)
{
    const [technologyType, setTechnologyType] = useState({
        id: this.props.match.params.id,
        name: ''
    });

    function updateTechnologyType(e) {
        e.preventDefault();
        let technologyTypeRequest = {
            name: technologyType.name
        };

        TechnologyTypeService.updateTechnologyType(technologyTypeRequest, technologyType.id).then(res => {
            this.props.history.push('/technology-types');
        });
    }

    changeNameHandler= (event) => {
        setTechnologyType({name: event.target.value});
    };

    cancel= () => {
        this.props.history.push('/technology-types');
    }

    // Similar to componentDidMount and componentDidUpdate
    useEffect(() => {
        TechnologyTypeService.getTechnologyTypeById(technologyType.id).then((res) => {
            let technologyTypeResponse = res.data;
            setTechnologyType({
                name: technologyTypeResponse.name
            });
        });
    });

    return (
        <div>
            <br></br>
                <div className = "container">
                    <div className = "row">
                        <div className = "card col-md-6 offset-md-3 offset-md-3">
                            <h3 className="text-center">Update Technology Type</h3>
                            <div className = "card-body">
                                <form>
                                    <div className = "form-group">
                                        <label>Name</label>
                                        <input placeholder="Name" name="name" className="form-control" 
                                            value={technologyType.name} />
                                    </div>

                                    <button className="btn btn-success" onClick={this.updateTechnologyType}>Save</button>
                                    <button className="btn btn-danger" onClick={this.cancel.bind(this)} style={{marginLeft: "10px"}}>Cancel</button>
                                </form>
                            </div>
                        </div>
                    </div>

                </div>
        </div>
    );
}

export default UpdateTechnologyTypeComponent;