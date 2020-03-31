import { gUser } from "../components/_MainApp/Models";

const initState = {
    isAuthenticated: false,
    errors: [],
    user: new gUser(),
    accessClaim: ""
}
const AuthenticationReducer = (state = initState, action) => {

    switch (action.type) {
        case "LOGIN":
        case "LOGOUT":
        case "SILENT_ATHENTICATION":
            return action.payload;
        default:
            return state;
    }
}
export default AuthenticationReducer;