import { apiCaller } from '../components/_MainApp/appConst';
import { gError, gDepartment } from '../components/_MainApp/Models';

export const getAllDepartments = () => {
    return async dispatch => {
        let state = {
            Departments: [],
            errors: []
        };
        try {
            const response = await apiCaller.get("department/get");
            switch (response.status) {
                case 200: // Ok Response
                    await response.json().then(data => {
                        state.Departments = data;
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

export const getDepartmentStatistics = () => {
    return async dispatch => {
        let state = {
            DepartmentStatistics: [],
            errors: []
        };
        try {
            const response = await apiCaller.get("department/GetStatistics");
            console.log(response);
            switch (response.status) {
                case 200: // Ok Response
                    await response.json().then(data => {
                        state.DepartmentStatistics = data;
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

export const postDepartment = (department = new gDepartment()) => {
    return async dispatch => {
        let state = {
            department: new gDepartment(),
            errors: []
        };
        try {
            const response = await apiCaller.post("department/post", department);
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.department = new gDepartment(data);
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


export const deleteDepartment = (department = new gDepartment()) => {
    return async dispatch => {
        let state = {
            isDeleted: false,
            errors: []
        };
        try {
            const response = await apiCaller.delete("department/delete", department);
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

