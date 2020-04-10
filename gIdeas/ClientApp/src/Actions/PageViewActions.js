
import { apiCaller } from '../components/_MainApp/appConst';
import { gError } from '../components/_MainApp/Models';

export const getAllPageViews = () => {
    return async dispatch => {
        let state = {
            PageViews: [],
            errors: [],
        };
        try {
            const response = await apiCaller.get("pageview/get");
            switch (response.status) {
                case 200: // Ok Response
                    await response.json().then(data => {
                        state.PageViews = data;
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

/// The api for this action will register the page name automatically
export const postSubmitPageView = (IdeaId = 0) => {
    return async dispatch => {
        let state = {
            isSuccessful: false,
            errors: []
        };
        try {
            const response = await apiCaller.post("pageview/post", IdeaId);
            switch (response.status) {
                case 201: // Created Response
                    state.isSuccessful = true;
                    break;
                case 400: //Bad Response
                    state.isSuccessful = false;
                    await response.json().then(data => {
                        state.payload.errors = data;
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

