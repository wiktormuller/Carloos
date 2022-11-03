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
  let skill = 0;
  let coordinates;

  if (
    props.geoLocation.lng === undefined &&
    props.advertLocation.lng === undefined
  )
    coordinates = `${center[1]}%2C${center[0]}%3B${center[1]}%2C${center[0]}`;
  else if (props.geoLocation.lng === undefined)
    coordinates = `${props.advertLocation.lng}%2C${props.advertLocation.lat}%3B${props.advertLocation.lng}%2C${props.advertLocation.lat}`;
  else
    coordinates = `${props.geoLocation.lng}%2C${props.geoLocation.lat}%3B${props.advertLocation.lng}%2C${props.advertLocation.lat}`;

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
    const map = useMap();
    if (props.advertLocation.lat === center[0]) {
      map.flyTo([props.advertLocation.lat, props.advertLocation.lng], zoom);
    } else if (props.geoLocation.lng !== undefined) {
      map.flyTo(
        [
          (props.advertLocation.lat + props.geoLocation.lat) / 2,
          (props.advertLocation.lng + props.geoLocation.lng) / 2,
        ],
        11 - options.length * 0.0009
      );
    } else if (props.advertLocation.lng !== undefined) {
      map.flyTo(
        [props.advertLocation.lat, props.advertLocation.lng],
        !!props.advertLocation.zoom ? props.advertLocation.zoom : 14
      );
    }

    return null;
  };
  const filteredLocalization = props.localizationArray.filter(
    (loc) => loc.id === 1
  );
  filteredLocalization.map(
    (loc) => ((center = [loc.lat, loc.lng]), (zoom = loc.zoom))
  );

  const filteredAdverts = props.jobOffersArray.map(
    (jobOffer) => (
      (skill = jobOffer.technologyTypes[0]),
      (
        <Marker
          key={jobOffer.id}
          icon={L.icon({
            iconUrl: require(`../../data/icons/${skill}.svg`),
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
      <FlyToCoords />
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
