import '../styles/main-styles.css';
import React from 'react';
import L from 'leaflet';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import {adsArray} from '../data/arrays'

export const Map = () =>{
  const center = [52.006376,19.025167] ;
  const zoom = 6.8;
  var skill = 0;
  
  return (
    <MapContainer className="map" center={center} zoom={zoom} scrollWheelZoom={true}>
      <TileLayer
        attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />

      {adsArray.map(job => (
        skill=job.skills[0],
        <Marker icon={L.icon({
          iconUrl: require(`../data/icons/`+skill+`.svg`).default,
          iconSize: new L.Point(60, 75),
        })}
        key = {job.id} position={[job.lat, job.lng]}>
          <Popup position={[job.lat, job.lng]}>
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
