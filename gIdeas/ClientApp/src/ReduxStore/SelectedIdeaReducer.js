const initState = {
    id: 0
}
const SelectedIdeaReducer = (state = initState, action) => {

    switch (action.type) {
        case "SELECT":
            return action.payload;
        default:
            return state;
    }
}
export default SelectedIdeaReducer;