const initState = 
    [
        {
            id: 0,
            path: "/Lin1",
            displayName: "Link1",
            displayOnRight: false
        }
    ]
const NavMenuReducer = (state = initState, action) => {
    switch (action.type) {
        case "CUSTOMER_NAV_MENU":
        case "MANAGER_NAV_MENU":
            return action.payload;
        case "DEFAULT_NAV":
            return initState;
        default:
            return state;
    }
}
export default NavMenuReducer;