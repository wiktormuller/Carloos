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
import RoadService from '../../../../../clients/RoadService';

export default function MapComponent(props)
{
  const centrumOfPolandCoordinates = [52.006376, 19.025167];
  const defaultZoomForPolandCountry = 5.0;
  const defaultZoomForJobOffer = 14;

  // It's road from point (0,0) to (0,0) which is no road at all
  const zeroToZeroCoordinates = {
    sourceLongitude: 0.000000,
    sourceLatitude: 0.000000,
    destinationLongitude: 0.000000,
    destinationLatitude: 0.000000
  };
  const [roadCoordinatesPoints, setRoadCoordinatesPoints] = useState([]);

  useEffect(() => {

    // Road data
    if (props.userGeoLocation !== undefined &&
      props.selectedJobOfferGeoLocation !== undefined &&
      props.userGeoLocation.longitude !== undefined &&
      props.selectedJobOfferGeoLocation.longitude !== undefined)
    {
      var coordinatesBetweenTwoPoints = {
        sourceLongitude: props.userGeoLocation.longitude,
        sourceLatitude: props.userGeoLocation.latitude,
        destinationLongitude: props.selectedJobOfferGeoLocation.longitude,
        destinationLatitude: props.selectedJobOfferGeoLocation.latitude
      };
      
        RoadService.getRoad(coordinatesBetweenTwoPoints).then(res => {
        setRoadCoordinatesPoints(res.data); 
      });
    }
    else
    {
      RoadService.getRoad(zeroToZeroCoordinates).then(res => {
        setRoadCoordinatesPoints(res.data); 
      });
    }
  }, []);


  const DrawRadiusCircle = () => {
    const map = useMap();

    map.eachLayer((layer) => {
      if (layer.options.radius != undefined)
      {
        map.removeLayer(layer);
      }
    })
    if(props.radiusInKilometers != null && props.radiusInKilometers != undefined)
    {
       L.circle([props.userGeoLocation.latitude, props.userGeoLocation.longitude], props.radiusInKilometers * 1000)
        .addTo(map);
    }
  }

  const FlyToCoords = () => {
    const map = useMap();

    // In a half way from user to job offer
    if (props.userGeoLocation !== undefined &&
        props.selectedJobOfferGeoLocation !== undefined &&
        props.userGeoLocation.longitude !== undefined && 
        props.selectedJobOfferGeoLocation.longitude !== undefined)
    {
      if (roadCoordinatesPoints.length !== 0) // Wait to get the coordinates from API
      {
        map.flyTo(
          [
            (props.selectedJobOfferGeoLocation.latitude + props.userGeoLocation.latitude) / 2,
            (props.selectedJobOfferGeoLocation.longitude + props.userGeoLocation.longitude) / 2
          ]
        );

        map.fitBounds([
          [roadCoordinatesPoints[0].latitude, roadCoordinatesPoints[0].longitude],
          [roadCoordinatesPoints[roadCoordinatesPoints.length-1].latitude, roadCoordinatesPoints[roadCoordinatesPoints.length-1].longitude]
        ]);
      }
    }
    // When job offer is selected but user geo location is unknown
    else if (props.userGeoLocation !== undefined &&
            props.selectedJobOfferGeoLocation !== undefined &&
            props.userGeoLocation.longitude === undefined && 
            props.selectedJobOfferGeoLocation.longitude !== undefined)
    {
      map.flyTo(
        [
          props.selectedJobOfferGeoLocation.latitude, props.selectedJobOfferGeoLocation.longitude
        ],
        defaultZoomForJobOffer
      );
    }
    // When job offer is not selected and user geolocation is unknown
    else if (props.userGeoLocation !== undefined &&
            props.selectedJobOfferGeoLocation !== undefined &&
            props.userGeoLocation.longitude === undefined && 
            props.selectedJobOfferGeoLocation.longitude !== undefined)
    {
      map.flyTo(
        [
          centrumOfPolandCoordinates.latitude, centrumOfPolandCoordinates.longitude
        ],
        defaultZoomForPolandCountry
      );
    }
    else
    {
      return null;
    }
  };

  var technologyTypeId = 0;
  const jobOffersMarkers = props.jobOffers.map(jobOffer => {
      
      technologyTypeId = jobOffer.technologyTypes[0].id;
      
      return (
        <Marker
          key={jobOffer.id}
          icon={L.icon({
            iconUrl: require(`../../../../../assets/icons/${technologyTypeId}.svg`),
            iconSize: new L.Point(40, 40)
          })}
          position={[jobOffer.address.latitude, jobOffer.address.longitude]} >

          <Popup
            position={[jobOffer.address.latitude, jobOffer.address.longitude]} >
            <div>
              <h2>{jobOffer.name}</h2>
              <p>{jobOffer.description}</p>
            </div>
          </Popup>
        </Marker>
      );
    }
  );

  return (
    <MapContainer
      center={centrumOfPolandCoordinates}
      zoom={defaultZoomForPolandCountry}
      className="map"
      scrollWheelZoom={true} >

      <FlyToCoords />

      <TileLayer
        attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" 
      />

      <DrawRadiusCircle />

      {jobOffersMarkers}

      <Polyline
        positions={roadCoordinatesPoints.map(roadCoordinatesPoint =>
        {
          return [roadCoordinatesPoint.latitude, roadCoordinatesPoint.longitude];
        })}
      />
    </MapContainer>
  );
}