import axios from 'axios';

const USERS_API_BASE_URL = "https://jobjet.azurewebsites.net/api/v1/users";

class UserService {

    getUsers(){
        return axios.get(USERS_API_BASE_URL);
    }

    getUserById(usersId){
        return axios.get(USERS_API_BASE_URL + '/' + usersId);
    }

    updateUser(user, userId){
        return axios.put(USERS_API_BASE_URL + '/' + userId, user);
    }

    deleteUser(userId){
        return axios.delete(USERS_API_BASE_URL + '/' + userId);
    }
}

export default new UserService()