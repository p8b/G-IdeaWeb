import { apiCaller } from '../components/_MainApp/appConst';
import { gCategoryTag, gError } from '../components/_MainApp/Models';

export const getAllCategories = () => {
    return async dispatch => {
        let state = {
            categories: [],
            errors: []
        };
        try {
            const response = await apiCaller.get("Category/get");
            switch (response.status) {
                case 200: // Ok Response
                    await response.json().then(data => {
                        console.log(data)
                        data.map(category => {
                            state.categories.push(new gCategoryTag(category));
                        })
                    }).catch(e => { })
                    break;
                case 400: //Bad Response
                    await response.json().then(data => {
                        state.errors = data;
                    }).catch(e => { })
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

export const postCategory = (category = new gCategoryTag()) => {
    return async dispatch => {
        let state = {
            category: new gCategoryTag(),
            errors: []
        };
        const response = await apiCaller.post("Category/post", category);
        try {
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.category = new gCategoryTag(data);
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

export const deleteCateory = (category = new gCategoryTag()) => {
    return async dispatch => {
        let state = {
            isDeleted: false,
            errors: []
        };
        try {
            const response = await apiCaller.delete("category/delete", category);
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
