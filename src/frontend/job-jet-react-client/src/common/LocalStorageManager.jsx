class LocalStorageManager {

    wipeActualLoginResponse() {
        localStorage.setItem('loginResponse', '');
    }

    setActualLoginResponse(response) {
        localStorage.setItem('loginResponse', JSON.stringify(response.data));
    }

    getActualLoginResponse() {
        return localStorage.getItem('loginResponse');
    }
}

export default new LocalStorageManager();