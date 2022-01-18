import "./map-styles.css";
// import React from "react";
import { React, useState, useEffect } from "react";
import L from "leaflet";
import { MapContainer, TileLayer, Marker, Popup,Polyline,useMap } from "react-leaflet";
import "leaflet/dist/leaflet.css";

export const Map = (props) => {
  let center = [52.006376, 19.025167];
  let zoom = 6.8;
  let skill = 0;

  //informacje o geolokacji użytkownika:
  console.log(props.geoLocation);
  //informacje o geolokacji miejsca pracy:
  console.log(props.advertLocation);
  let coordinates = `${props.geoLocation.lng}%2C${props.geoLocation.lat}%3B${props.advertLocation.lng}%2C${props.advertLocation.lat}`
  console.log(coordinates)
  let url =`https://jobjet.azurewebsites.net/api/v1/roads/`+coordinates;
  const [options, setOptions] = useState([]);
  useEffect(() => {
    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setOptions(data);
      });
  }, [url]);
  
  console.log(options);

const FlyToCoords=()=>{
  const map = useMap();
  if(props.geoLocation.lng!==undefined)
  {
    map.flyTo([((props.advertLocation.lat+props.geoLocation.lat)/2),((props.advertLocation.lng+props.geoLocation.lng)/2)],(11-(options.length*0.0009)))
    return null;
  }
  else if(props.advertLocation.lng!==undefined)
  {
  map.flyTo([props.advertLocation.lat,props.advertLocation.lng],14)
  return null;
  }
  else return null;
}
  const filteredLocalization = props.localizationArray.filter(
    (loc) => loc.id === 1
  );
  filteredLocalization.map(
    // eslint-disable-next-line no-sequences
    (loc) => ((center = [loc.lat, loc.lng]), (zoom = loc.zoom))
  );

  
  const filteredAdverts = props.adsArray.map(
    (ad) => (
      // eslint-disable-next-line no-sequences
      (skill = ad.skills[0]),
      (
        <Marker
          key={ad.id}
          icon={L.icon({
            iconUrl: require(`../../data/icons/` + skill + `.svg`).default,
            iconSize: new L.Point(60, 75),
          })}
          position={[ad.lat, ad.lng]}
          
        >
          <Popup position={[ad.lat, ad.lng]}>
            <div>
              <h2>{ad.title}</h2>
              <p>{ad.description}</p>
            </div>
          </Popup>
        </Marker>
      )
    )
  );

  return (
    <MapContainer
      className="map"
      center={center}
      zoom={zoom}
      scrollWheelZoom={true}
    >
      <FlyToCoords/>
      <TileLayer
        attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      {filteredAdverts}
      <Polyline positions={options.map((loc) => {return [loc.latitude, loc.longitude];})}></Polyline>
    </MapContainer>
  );
};