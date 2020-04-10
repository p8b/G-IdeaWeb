import { apiCaller, AllRecords } from '../components/_MainApp/appConst';
import { gIdea, gError } from '../components/_MainApp/Models';

export const getFilterdAndSortedIdeas = (
    submissionYear = new Date().getUTCFullYear(),
    categoryTagId = AllRecords,
    departmentId = AllRecords,
    roleId = AllRecords,
    ideaStatus = AllRecords,
    isAuthorAnon = AllRecords,

) => {
    return async dispatch => {
        let state = {
            Ideas: [],
            errors: [],
        };
        try {
            const response = await apiCaller.get(`ideas/get/${submissionYear}/${categoryTagId}/${departmentId}/${roleId}/${ideaStatus}/${isAuthorAnon}`);
            switch (response.status) {
                case 200: // Ok Response
                    await response.json().then(data => {
                        data.map(i => {
                            state.Ideas.push(new gIdea(i))
                        })
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

export const getIdea = (ideaId = 0) => {
    return async dispatch => {
        let state = {
            Idea: new gIdea(),
            errors: [],
        };
        try {
            const response = await apiCaller.get(`ideas/get/${ideaId}`);
            switch (response.status) {
                case 200: // Ok Response
                    await response.json().then(data => 
                    {
                        console.log(data)
                        state.Idea = new gIdea(data);
                        console.log(state.Idea)
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

export const postNewIdea = (newIdea = new gIdea()) => {
    return async dispatch => {
        let state = {
            newIdea: new gIdea(),
            errors: []
        };
        try {
            const response = await apiCaller.post("ideas/post", newIdea);
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.newIdea = new gIdea(data);
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

export const postNewVote = (ideaId = 0, thumbUpOrDown = true) => {
    return async dispatch => {
        let state = {
            isSuccessful: false,
            errors: []
        };
        try {
            const response = await apiCaller.get(`ideas/PostVote/vote/${ideaId}/${thumbUpOrDown}`);
            switch (response.status) {
                case 200: // Created Response
                    state.isSuccessful = true;
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

export const putIdeaBlockOrUnblock = (ideaId = 0, isBlocked = false) => {
    return async dispatch => {
        let state = {
            modifiedIdea: new gIdea(),
            errors: []
        };
        try {
            const response = await apiCaller.put(`idea/put/${ideaId}/${isBlocked}`);
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.modifiedIdea = new gIdea(data);
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

export const putIdea = (modifiedIdea = new gIdea()) => {
    return async dispatch => {
        let state = {
            modifiedIdea: new gIdea(),
            errors: []
        };
        try {
            const response = await apiCaller.post("idea/put", modifiedIdea);
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.modifiedIdea = new gIdea(data);
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
