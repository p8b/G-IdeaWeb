


export const SelectThisIdea = (id) => {
    return async dispatch => {
        dispatch({ type: "SELECT", payload: id });

    }
}

export const DeselectThisIdea = (id) => {
    return async dispatch => {
        dispatch({ type: "SELECT", payload: 0 });

    }
}