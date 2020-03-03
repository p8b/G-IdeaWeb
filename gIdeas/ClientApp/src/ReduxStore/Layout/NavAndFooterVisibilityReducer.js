const initState = true;

/// Reducer for setting the visibility of the navbar and footer
const NavAndFooterVisibilityReducer = (state = initState, action) => {
    switch (action.type) {
        case "INVISIBLE":
            return false;
        case "VISIBLE":
            return true;
        default:
            return state;
    }
}
export default NavAndFooterVisibilityReducer;