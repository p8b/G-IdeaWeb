import { AllRecords, apiCaller } from '../components/_MainApp/appConst';
import { gUser, gError } from '../components/_MainApp/Models';

export const getSearchUser = (roleId = AllRecords, departmentId = AllRecords, searchValue = "") => {
    return async dispatch => {
        let state = {
            userList: [],
            errors: [],
        };
        try {
            const response = await apiCaller.get(`user/get/${roleId}/${departmentId}/${searchValue}`);
            switch (response.status) {
                case 200: // Ok Response
                    await response.json().then(data => {
                        state.userList = data;
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
            console.log(e)
            state.errors.push(new gError({ key: "ConnectionError", value: "Server Connection Error" }))
        }
        return state;
    }
}


export const putUser = (modifiedUser = new gUser()) => {
    return async dispatch => {
        let state = {
            modifiedUser: new gUser(),
            errors: []
        };
        try {
            const response = await apiCaller.put("user/put", modifiedUser);
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.modifiedUser = data;
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
            console.log(e)
            state.errors.push(new gError({ key: "ConnectionError", value: "Server Connection Error" }))
        }
        return state;
    }
}

export const putPassword = (modifiedUser = new gUser()) => {
    return async dispatch => {
        let state = {
            modifiedUser: new gUser(),
            errors: []
        };
        try {
            const response = await apiCaller.put("user/putPassword", modifiedUser);
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.modifiedUser = data;
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
            console.log(e)
            state.errors.push(new gError({ key: "ConnectionError", value: "Server Connection Error" }))
        }
        return state;
    }
}

export const putMyPassword = (modifiedUser = new gUser()) => {
    return async dispatch => {
        let state = {
            modifiedUser: new gUser(),
            errors: []
        };
        try {
            const response = await apiCaller.put("user/putPassword", modifiedUser);
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.modifiedUser = data;
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
            console.log(e)
            state.errors.push(new gError({ key: "ConnectionError", value: "Server Connection Error" }))
        }
        return state;
    }
}

export const putBlockOrUnblockUser = (userId = 0, isBlocked = false) => {
    return async dispatch => {
        let state = {
            modifiedUser: new gUser(),
            errors: []
        };
        try {
            const response = await apiCaller.put(`user/put/${userId}/${isBlocked}`);
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.modifiedUser = data;
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
            console.log(e)
            state.errors.push(new gError({ key: "ConnectionError", value: "Server Connection Error" }))
        }
        return state;
    }
}

export const deleteUser = (selectedUser = new gUser()) => {
    return async dispatch => {
        let state = {
            isDeleted: false,
            errors: []
        };
        try {
            const response = await apiCaller.delete("user/delete", selectedUser);
            switch (response.status) {
                case 200: // Ok Response
                    state.isDeleted = true;
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
            console.log(e)
            state.errors.push(new gError({ key: "ConnectionError", value: "Server Connection Error" }))
        }
        return state;
    }
}