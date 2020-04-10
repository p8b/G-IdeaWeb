import { gError, gComment } from "../components/_MainApp/Models";
import { apiCaller } from '../components/_MainApp/appConst';

export const postComment = (comment = new gComment()) => {
    return async dispatch => {
        let state = {
            isSuccess: false,
            errors: []
        };
        try {
            const response = await apiCaller.post("comment/post", comment);
            switch (response.status) {
                case 201: // Created Response
                        state.isSuccess = true;
                    break;
                case 400: //Bad Response
                    await response.json().then(data => {
                        state.errors = data;
                    }).catch(e => { console.log(e) })
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

export const deleteComment = (comment = new gComment()) => {
    return async dispatch => {
        let state = {
            isDeleted: false,
            errors: []
        };
        try {
            const response = await apiCaller.delete("comment/delete", comment);
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
