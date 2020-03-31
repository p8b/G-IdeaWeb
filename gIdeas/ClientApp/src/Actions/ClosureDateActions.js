import { apiCaller } from '../components/_MainApp/appConst';
import { gClosureDates } from '../components/_MainApp/Models';

export const getClosureDates = () => {
    return async dispatch => {
        let state = {
            ClosureDates: [] ,
            errors: []
        };
        try {
            const response = await apiCaller.get("closuredate/get");
            switch (response.status) {
                case 200: // Ok Response
                    await response.json().then(data => {
                        state.ClosureDates = new closureDate(data);
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

export const postClosureDate = (closureDate = new gClosureDates()) => {
    return async dispatch => {
        let state = {
            closureDate: new gClosureDates(),
            errors: []
        };
        try {
            const response = await apiCaller.post("closureDate", closureDate);
            switch (response.status) {
                case 201: // Created Response
                    await response.json().then(data => {
                        state.closureDate = new gClosureDates(data);
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
