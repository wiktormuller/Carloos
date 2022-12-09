import axios from 'axios';

const PROFILE_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/profiles/me";

class ProfileService
{
    getProfile()
    {
        let config = {
            headers: {
                'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwibmFtZSI6IkNFTyIsImp0aSI6IjgxODc3YjE2LTM1OWMtNDE1OC1hZGNkLTdjMWVmN2Y5ZjQ0MCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMSIsImVtYWlsIjoiY2VvQGpvYmpldC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW5pc3RyYXRvciIsIlVzZXIiXSwiZXhwIjoxNjcwMjY3MjM1LCJpc3MiOiJodHRwczovL2pvYmpldC5henVyZXdlYnNpdGVzLm5ldCIsImF1ZCI6Imh0dHBzOi8vam9iamV0LmF6dXJld2Vic2l0ZXMubmV0In0.ymthqNrF5OMnQYS4iXG3FhQVcU6EplnIkyYyYgAK9Dw'
            }
        }

        return axios.get(PROFILE_API_BASE_URL, config);
    }
}

export default new ProfileService();