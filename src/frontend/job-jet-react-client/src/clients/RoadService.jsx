import axios from 'axios';
import Environment from './Environment';

const ROADS_API_BASE_URL = `${Environment.getEnvironment()}/api/v1/roads`;

class RoadService {

    getRoad(coordinates)
    {
        return axios.get(`${ROADS_API_BASE_URL}?sourceLongitude=${coordinates.sourceLongitude}&sourceLatitude=${coordinates.sourceLatitude}&destinationLongitude=${coordinates.destinationLongitude}&destinationLatitude=${coordinates.destinationLatitude}`);
    }
}

export default new RoadService();