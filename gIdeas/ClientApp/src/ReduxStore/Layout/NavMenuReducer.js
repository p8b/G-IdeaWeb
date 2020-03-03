/// Initial state of a navigation button
const initState =
    [{
        id: 0,
        path: "/Path",
        displayName: "",
    }];

/// Reducer for navigation menu
const NavMenuReducer = (state = initState, action) => {
    switch (action.type) {
        case "ADMIN_NAV":
        case "QAMANAGER_NAV":
        case "QACOORDINATOR_NAV":
        case "STAFF_NAV":
            return action.payload;
        case "DEFAULT_NAV":
            return initState;
        default:
            return state;
    }
}
export default NavMenuReducer;