export const SelectThisIdea = (id) => {
    return async dispatch => {
        dispatch({ type: "SELECT", payload: id });
    }
}
export const DeselectIdea = () => {
    return async dispatch => {
        dispatch({ type: "SELECT", payload: 0 });
    }
}