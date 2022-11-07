import "./map-styles.css";
import { React, useState, useEffect } from "react";
import L from "leaflet";
import {
  MapContainer,
  TileLayer,
  Marker,
  Popup,
  Polyline,
  useMap,
} from "react-leaflet";
import "leaflet/dist/leaflet.css";

export const Map = (props) => {
  let center = [52.006376, 19.025167];
  let zoom = 5.5;
  let technologyTypeId = 0;
  let coordinates;

  if (
    props.geoLocation.longitude === undefined &&
    props.advertLocation.longitude === undefined
  )
    coordinates = `${center[1]}%2C${center[0]}%3B${center[1]}%2C${center[0]}`;
  else if (props.geoLocation.longitude === undefined)
    coordinates = `${props.advertLocation.longitude}%2C${props.advertLocation.latitude}%3B${props.advertLocation.longitude}%2C${props.advertLocation.latitude}`;
  else
    coordinates = `${props.geoLocation.longitude}%2C${props.geoLocation.latitude}%3B${props.advertLocation.longitude}%2C${props.advertLocation.latitude}`;

  let url = `https://jobjet.azurewebsites.net/api/v1/roads/` + coordinates;
  let [options, setOptions] = useState([]);

  useEffect(() => {
    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setOptions(data);
      });
  }, [url]);

  if (options.length === 2) {
    options = [];
  }

  const FlyToCoords = () => {
    console.log(props.geoLocation);
    const map = useMap();
    if (props.advertLocation.latitude === center[0]) {
      map.flyTo([props.advertLocation.latitude, props.advertLocation.longitude], zoom);
    } 
    else if (props.geoLocation.longitude !== undefined) {
      map.flyTo(
        [
          (props.advertLocation.latitude + props.geoLocation.latitude) / 2,
          (props.advertLocation.longitude + props.geoLocation.longitude) / 2,
        ],
        11 - options.length * 0.0009
      );
    } 
    else if (props.advertLocation.longitude !== undefined) {
      map.flyTo(
        [props.advertLocation.latitude, props.advertLocation.longitude],
        !!props.advertLocation.zoom ? props.advertLocation.zoom : 14
      );
    }

    return null;
  };
  const filteredLocalization = props.localizationArray.filter(
    (loc) => loc.id === 1
  );
  filteredLocalization.map(
    (loc) => ((center = [loc.latitude, loc.longitude]), (zoom = loc.zoom))
  );

  const filteredAdverts = props.jobOffersArray.map(
    (jobOffer) => (
      (technologyTypeId = props.technologyTypes.find((technologyType) => technologyType.name === jobOffer.technologyTypes[0]).id),
      (
        <Marker
          key={jobOffer.id}
          icon={L.icon({
            iconUrl: require(`../../data/icons/${technologyTypeId}.svg`),
            iconSize: new L.Point(60, 75)
        })}
          position={[jobOffer.address.latitude, jobOffer.address.longitude]}
        >
          <Popup
            position={[jobOffer.address.latitude, jobOffer.address.longitude]}
          >
            <div>
              <h2>{jobOffer.name}</h2>
              <p>{jobOffer.description}</p>
            </div>
          </Popup>
        </Marker>
      )
    )
  );

  return (
    <MapContainer
      center={center}
      zoom={zoom}
      className="map"
      scrollWheelZoom={true}
    >
      {/* <FlyToCoords /> */}
      <TileLayer
        attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      {filteredAdverts}
      <Polyline
        positions={options.map((loc) => {
          return [loc.latitude, loc.longitude];
        })}
      />
    </MapContainer>
  );
};
