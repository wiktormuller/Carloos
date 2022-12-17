import axios from 'axios';

const PROFILE_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/profiles/me";

class ProfileService
{
    getProfile()
    {
        let config = {
            headers: {
                'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwibmFtZSI6IkNFTyIsImp0aSI6IjliNGU2ZjU3LTNlZDAtNDdjNS1hNWFiLTQzMTE1M2Y2OWU5YSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMSIsImVtYWlsIjoiY2VvQGpvYmpldC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW5pc3RyYXRvciIsIlVzZXIiXSwiZXhwIjoxNjcxMjg3MzEyLCJpc3MiOiJodHRwczovL2pvYmpldC5henVyZXdlYnNpdGVzLm5ldCIsImF1ZCI6Imh0dHBzOi8vam9iamV0LmF6dXJld2Vic2l0ZXMubmV0In0.sij5oFcEXNq9Xmb3g8cgZe0g8XpWX_lDg6w671m7po4'
            }
        }

        return axios.get(PROFILE_API_BASE_URL, config);
    }
}

export default new ProfileService();