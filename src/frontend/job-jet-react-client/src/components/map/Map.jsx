import "./map-styles.css";
import React from "react";
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

  const url = 'https://jobjet.azurewebsites.net/api/v1/roads/17.9666195%2C54.1228639%3B17.968999%2C54.118802';
  /*function myfunction(x)
  {road = x.map(
    (r)=>(
      <Marker 
      position ={[r.longitude,r.latitude]}
      ></Marker>
    )
  );}*/

 /* async function getRandomUser() {
    const response = await fetch(url);
    const data = await response.json();
    return data;
  }
  let datas=getRandomUser();
  console.log(datas)*/
//   let positionArray=fetch(url)
//   .then(response => response.json())
//   .then(data => {
//     return data[0];
// })
// console.log(positionArray)
let obj;
let Array=fetch(url)
  .then(res => res.json())
  .then(data => obj = data)
  .then(() => { return obj })
console.log(Array)


  const filteredLocalization = props.localizationArray.filter(
    (loc) => loc.id === 1
  );
  filteredLocalization.map(
    (loc) => ((center = [loc.lat, loc.lng]), (zoom = loc.zoom))
  );

  const filteredAdverts = props.adsArray.map(
    (ad) => (
      (skill = ad.skills[0]),
      (
        <Marker
          icon={L.icon({
            iconUrl: require(`../../data/icons/` + skill + `.svg`).default,
            iconSize: new L.Point(60, 75),
          })}
          key={ad.id}
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

  const arr = [{ longitude: 17.96664, latitude: 54.122832 }, { longitude: 17.967721, latitude: 54.123033 }, { longitude: 17.96799, latitude: 54.123077 }]
  const renderedArray = arr.map((obj) => {
    return (
      <Marker
        position={[obj.latitude, obj.longitude]}
      />
    );
  });

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
      {renderedArray}


    </MapContainer>
  );
};
