import { apiCaller } from '../components/_MainApp/appConst';
import { gFlaggedIdea } from '../components/_MainApp/Models';

export const postFlaggedIdea = (flaggedIdea = new gFlaggedIdea()) => {
    return async dispatch => {
        let state = {
            flaggedIdea: new gFlaggedIdea(),
            errors: []
        };
        try {
            const response = await apiCaller.post("FlaggedIdea/post", flaggedIdea);
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.flaggedIdea = new flaggedIdea(data);
                    }).catch(e => { console.log(e) })
                    break;
                case 400: //Bad Response
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
