import React, {Component} from 'react';
import L from 'leaflet';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import {adsArray} from '../data/arrays'
import { Data } from "./Data"





class Map extends Component {
  states = {
    center:{
      lat: 52.006376,
      lng: 19.025167,
    },
    zoom: 6.8,
    layer:[]
  }
 
  
 
  render(){
    
    return (
      
      <MapContainer className="map" center={this.states.center} zoom={this.states.zoom}>
        <TileLayer
          attribution='&amp;copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
         {adsArray.map(job => (
           this.skill=job.skills[0],
           
        <Marker
          icon={L.icon({
            iconUrl: require(`../data/icons/`+this.skill+`.svg`).default,
            iconSize: new L.Point(60, 75),
          })}
          key={job.id}
          position={[

            job.lat,
            job.lng
          ]} 
           >
            <Popup
          position={[
            job.lat,
            job.lng
          ]}
          
        >
          <div>
            <h2>{job.title}</h2>
            <p>{job.description}</p>
          </div>
        </Popup>
            </Marker>
            
        
        
        
      ))}
     
      </MapContainer>
      
    );
  }
  
}

export default Map;