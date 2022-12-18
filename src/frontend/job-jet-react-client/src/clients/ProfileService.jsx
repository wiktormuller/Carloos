import axios from 'axios';

const PROFILE_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/profiles/me";

class ProfileService
{
    getProfile()
    {
        let config = {
            headers: {
                'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwibmFtZSI6IkNFTyIsImp0aSI6IjRkN2ZmNWQ5LWZhOWEtNGQzNi04N2M4LTI4NzAwM2FjODYzNiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMSIsImVtYWlsIjoiY2VvQGpvYmpldC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW5pc3RyYXRvciIsIlVzZXIiXSwiZXhwIjoxNjcxNDAyOTYyLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAzIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMyJ9.3C2JE8KdmGa9ri6Tjclr09LxL3y3vhr1OhYtcnqLeqk'
            }
        }

        return axios.get(PROFILE_API_BASE_URL, config);
    }
}

export default new ProfileService();