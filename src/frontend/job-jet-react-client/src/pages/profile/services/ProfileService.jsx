import axios from 'axios';

const PROFILE_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/profiles/me";

class ProfileService
{
    async getProfile()
    {
        let config = {
            headers: {
                'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwibmFtZSI6IkNFTyIsImp0aSI6ImZmMGVjNGM2LWQ5YzctNGVjMy05NzU1LTA5YTkxNTU0NWJjNSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMSIsImVtYWlsIjoiY2VvQGpvYmpldC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW5pc3RyYXRvciIsIlVzZXIiXSwiZXhwIjoxNjcwMTU1NjM5LCJpc3MiOiJodHRwczovL2pvYmpldC5henVyZXdlYnNpdGVzLm5ldCIsImF1ZCI6Imh0dHBzOi8vam9iamV0LmF6dXJld2Vic2l0ZXMubmV0In0.0aDoHTy_cB_elrXfXwb12Seml0_CVKPwSo9k_XACJvA',
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS"
            }
        }

        const response = axios.get(PROFILE_API_BASE_URL, config);

        return response.data;
    }
}

export default new ProfileService();