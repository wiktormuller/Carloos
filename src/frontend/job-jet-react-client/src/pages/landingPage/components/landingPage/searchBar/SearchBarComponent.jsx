import './search-bar-styles.css';
import { useEffect, useState } from 'react';
import TechnologyTypeService from '../../../../../clients/TechnologyTypeService';
import SeniorityLevelService from '../../../../../clients/SeniorityLevelService';
import EmploymentTypeService from '../../../../../clients/EmploymentTypeService';
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

    function clearAllFilters()
    {
        props.setSearchText({
            value: ''
        });

        props.setSelectedSeniorityLevel({
            "value": 0,
            "label": ''
          });

        props.setSelectedEmploymentType({
            "value": 0,
            "label": ''
          });

        props.setSelectedWorkSpecification({
            "value": 0,
            "label": ''
          });

        props.setSelectedTechnologyType({
            "value": 0,
            "label": ''
          });

        props.setSelectedRadiusInKilometers({
            value: ''
        });
    }

    return (
        <div className="search-bar">
            <div className="general-section">
                <div className="form-group form-group-extended" onChange={(event) => props.setSearchText(event.target)}>
                    <input type="text" className="form-control" id="name" value={props.searchText} placeholder="General Search" />
                </div>

                <div className="form-group form-group-extended">
                    <Select className="form-control" options={availableSeniorityLevels} onChange={props.setSelectedSeniorityLevel} value={availableSeniorityLevels[props.selectedSeniorityLevelId-1]} placeholder="Seniority Level"/>
                </div>

                <div className="form-group form-group-extended">
                    <Select className="form-control" options={availableEmploymentTypes} onChange={props.setSelectedEmploymentType} value={availableEmploymentTypes[props.selectedEmploymentTypeId-1]} placeholder="Employment Type"/>
                </div>

                <div className="form-group form-group-extended">
                    <Select className="form-control" options={availableWorkSpecifications} onChange={props.setSelectedWorkSpecification} value={availableWorkSpecifications.find(element => {return element.label === props.selectedWorkSpecification})} placeholder="Work Specification"/>
                </div>

                <div className="form-group form-group-extended" onChange={(event) => props.setSelectedRadiusInKilometers(event.target)}>
                    <input type="number" className="form-control" id="name" value={props.selectedRadiusInKilometers} placeholder="Radius" />
                </div>

                <button type="button" className="btn btn-primary" onClick={clearAllFilters}>
                    Clear filters
                </button>
            </div>

            <div className="technology-types-section">
                {availableTechnologyTypes.map(technologyType => (
                    <div className="technology-type-marker" onClick={function() {props.setSelectedTechnologyType(technologyType)}}>
                        <img className="technology-type-marker-image" src={require(`../../../../../assets/icons/${technologyType.value}.svg`)} alt="Technology Type Img" />
                        <span className="technology-type-marker-label">{technologyType.label}</span>
                    </div>
                ))}
            </div>
        </div>
    );
}