import "./map-styles.css";
// import React from "react";
import { React, useState, useEffect } from "react";
import L from "leaflet";
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import "leaflet/dist/leaflet.css";

export const Map = (props) => {
  let center = [52.006376, 19.025167];
  let zoom = 6.8;
  let skill = 0;

  //informacje o geolokacji użytkownika:
  console.log(props.geoLocation);
  //informacje o geolokacji miejsca pracy:
  console.log(props.advertLocation);

  const url =
    "https://jobjet.azurewebsites.net/api/v1/roads/17.9666195%2C54.1228639%3B17.968999%2C54.118802";

  const [options, setOptions] = useState({});
  useEffect(() => {
    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setOptions(data);
      });
  }, []);

  console.log(options);

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
      <TileLayer
        attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      {filteredAdverts}
    </MapContainer>
  );
};
