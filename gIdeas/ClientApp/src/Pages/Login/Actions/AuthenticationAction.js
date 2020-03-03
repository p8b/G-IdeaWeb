import { getCookieValue } from './../../../components/_MainApp/appConst'

export const authenticate = (loginDetails = { email: "", password: "" }) => {
    return async dispatch => {
        let state = {
            type:"LOGIN",
            payload: {
                isAuthenticated: false,
                message: "",
                accessClaim: ""
            }
        };
        const response = await fetch("api/authentication/login", {
            method: "POST",
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
                //'X-AntiForgery-TOKEN': getCookieValue("AntiForgery-TOKEN"),
            },
            body: JSON.stringify(loginDetails),
        }).catch(err => console.log(err));

        switch (response.status) {
            case 200: /// "ok" response 
                await response.json().then(data => {
                    state.payload.isAuthenticated = true;
                    state.payload.accessClaim = data.accessClaim;
                    state.payload.message = "";
                });
                break;
            case 400: /// "bad request" response 
                await response.json().then(data => {
                    state.payload.isAuthenticated = false;
                    state.payload.accessClaim = "";
                    state.payload.message = data.message;
                });
                break;
        }
        dispatch(state);
    }
}
export const signOut = () => {
    return async dispatch => {
        let state = {
            type: "LOGOUT",
            payload: {
                isAuthenticated: false,
                message: "",
                accessClaim: ""
            }
        };
        await fetch("api/authentication/logout", {
            method: "GET",
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
        }).catch(err => console.log(err));
        dispatch(state);
    }
}
export const silentAuthentication = () => {
    return async dispatch => {
        let state = {
            type: 'SILENT_ATHENTICATION',
            payload: {
                isAuthenticated: false,
                message: "",
                accessClaim: ""
            }
        };
        const response = await fetch('api/authentication/SilentAuth');
        switch (response.status) {
            case 200: // Ok response
                await response.json().then(data => {
                    state.payload.isAuthenticated = true;
                    state.payload.accessClaim = data.accessClaim;
                }).catch(e => { })
            case 401: //Unauthorized 
            default:
                dispatch(state);
                break;
        };
    }
}