const initState = {
    isAuthenticated: false,
    message: "",
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