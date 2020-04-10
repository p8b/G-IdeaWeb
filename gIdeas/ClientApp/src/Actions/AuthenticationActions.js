import { apiCaller } from '../components/_MainApp/appConst';
import { gUser, gError } from '../components/_MainApp/Models';

export const authenticate = (loginDetails = { email: "", password: "", rememberMe: false }) => {
    return async dispatch => {
        let state = {
            type: "LOGIN",
            payload: {
                isAuthenticated: false,
                errors: [],
                user: new gUser(),
                accessClaim: ""
            }
        };
        try {
            const response = await apiCaller.post("authentication/login", loginDetails);
            switch (response.status) {
                case 200: /// "ok" response 
                    await response.json().then(data => {
                        state.payload.isAuthenticated = true;
                        state.payload.user = new gUser(data);
                        state.payload.accessClaim = data.role.accessClaim;
                    });
                    break;
                case 400: /// "bad request" response 
                    await response.json().then(data => {
                        state.payload.errors = data;
                    });
                    break;
                default:
                    state.errors.push(new gError({ key: "ConnectionError", value: `Server Error Code: ${response.state}` }))
                    break;
            }
        } catch (e) {
            console.log(e)
            state.errors.push(new gError({ key: "ConnectionError", value: "Server Connection Error" }))
        }
        dispatch(state);
    }
}

export const logout = () => {
    return async dispatch => {
        let state = {
            type: "LOGOUT",
            payload: {
                isAuthenticated: false,
                errors: [],
                user: new gUser(),
                accessClaim: ""
            }
        };
        try {
            await apiCaller.get("authentication/logout");
            dispatch(state);
        } catch (e) {
            console.log(e)
            state.errors.push(new gError({ key: "ConnectionError", value: "Server Connection Error" }))
        }
    }
}

export const silentAuthentication = (currentAuthentication, user) => {
    return async dispatch => {
        let state = {
            type: 'SILENT_ATHENTICATION',
            payload: {
                isAuthenticated: false,
                errors: [],
                user: new gUser(),
                accessClaim: ""
            }
        };

        try {
            const response = await apiCaller.get('authentication/silent');

            switch (response.status) {
                case 200: // Ok response
                    await response.json().then(data => {
                        state.payload.isAuthenticated = true;
                        state.payload.user = new gUser(data);
                        state.payload.accessClaim = data.role.accessClaim;
                    }).catch(e => { })
                    console.log(state.payload.isAuthenticated)
                    if (!currentAuthentication && state.payload.isAuthenticated)
                        dispatch(state);
                    break;
                case 401: //Unauthorized 
                default:
                    if (currentAuthentication)
                        dispatch(state);
                    if (user)
                    break;
            };
        } catch (e) {
            console.log(e)
            state.errors.push(new gError({ key: "ConnectionError", value: "Server Connection Error" }))
        }
    }
}

export const getBrowserStatistics = () => {
    return async dispatch => {
        let state = {
            browserStatistics: [],
            errors: []
        };
        try {
            const response = await apiCaller.get("authentication/get/browserStatistics");
            switch (response.status) {
                case 200: // Ok Response
                    await response.json().then(data => {
                        state.browserStatistics = data;
                    }).catch(e => { console.log(e) })
                    break;
                case 400: //Bad Response
                    await response.json().then(data => {
                        state.errors = data;
                    }).catch(e => { console.log(e) })
                    break;
                default:
                    state.errors.push(new gError({ key: "ConnectionError", value: `Server Error Code: ${response.state}` }))
                    break;
            };
        } catch (e) {
            state.errors.push(new gError({ key: "ConnectionError", value: "Server Connection Error" }))
        }
        return state;
    }
}
