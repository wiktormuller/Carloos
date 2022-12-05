import './search-bar-styles.css';
import { useEffect, useState } from 'react';
import TechnologyTypeService from '../../../technologyTypes/services/TechnologyTypeService';
import SeniorityLevelService from '../../../seniorityLevels/services/SeniorityLevelService';
import EmploymentTypeService from '../../../employmentTypes/services/EmploymentTypeService';
import Select from 'react-select'

export default function SearchBarComponent(props)
{
    const availableWorkSpecifications = [
        {
            "value": "1",
            "label": "Hybrid"
        },
        {
            "value": "2",
            "label": "Office"
        },
        {
            "value": "3",
            "label": "FullyRemote"
        }
    ];
    const [ availableTechnologyTypes, setAvailableTechnologyTypes ] = useState([]);
    const [ availableSeniorityLevels, setAvailableSeniorityLevels] = useState([]);
    const [ availableEmploymentTypes, setAvailableEmploymentTypes ] = useState([]);

      // Similar to componentDidMount and componentDidUpdate
  useEffect(() => {
    TechnologyTypeService.getTechnologyTypes().then(res => {
      setAvailableTechnologyTypes(res.data.map(technologyType => (
        {
          "value": technologyType.id,
          "label": technologyType.name
        }
      ))); 
    });

    SeniorityLevelService.getSeniorityLevels().then(res => {
      setAvailableSeniorityLevels(res.data.map(seniorityLevel => (
        {
          "value": seniorityLevel.id,
          "label": seniorityLevel.name
        }
      )));
    });

    EmploymentTypeService.getEmploymentTypes().then(res => {
      setAvailableEmploymentTypes(res.data.map(employmentType => (
        {
          "value": employmentType.id,
          "label": employmentType.name
        }
      )));
    });
}, []);

    return (
        <div className="search-bar">
            <h1>Search Bar</h1>
            <div className="form-group">
                <input type="text" className="form-control" id="name" onChange={props.setSearchTextProxy} placeholder="Search" />
            </div>

            <div className="form-group">
                <Select className="form-control" options={availableSeniorityLevels} onChange={props.setSelectedSeniorityLevelProxy} value={availableSeniorityLevels[props.selectedSeniorityLevelId-1]} placeholder="Seniority Level"/>
            </div>

            <div className="form-group">
                <Select className="form-control" options={availableEmploymentTypes} onChange={props.setSelectedEmploymentTypeProxy} value={availableEmploymentTypes[props.selectedEmploymentTypeId-1]} placeholder="Employment Type"/>
            </div>

            <div className="form-group">
                <Select className="form-control" options={availableWorkSpecifications} onChange={props.setSelectedWorkSpecificationProxy} value={availableWorkSpecifications.find(element => {return element.label === props.selectedWorkSpecification})} placeholder="Work Specification"/>
            </div>

            {availableTechnologyTypes.map(technologyType => (
                <button>{technologyType.label} </button>
            ))}
        </div>
    );
}