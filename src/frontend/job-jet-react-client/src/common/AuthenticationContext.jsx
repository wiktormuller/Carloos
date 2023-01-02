import React, { useState, useEffect, createContext } from 'react';
import LoginService from '../clients/LoginService';
import LocalStorageManager from '../common/LocalStorageManager';

export const AuthenticationContext = createContext({});

export const AuthenticationProvider = ({children}) => {
    const [ currentUser, setCurrentUser ] = useState();

    useEffect(() =>
    {
        console.log('Auth Provider');
        checkLoggedIn();
    }, []);

    function checkLoggedIn()
    {
        let loggedUser = LoginService.getAuthenticatedUser();

        if (loggedUser === null)
        {
            LocalStorageManager.wipeActualLoginResponse();
        }

        setCurrentUser(loggedUser);
    }

    return (
        <AuthenticationContext.Provider value={[currentUser, setCurrentUser]}>
            {children}
        </AuthenticationContext.Provider>
    );
}