const State = 
    [{
        id: 0,
        description: "Failed to load comments"
    }]

const CommentsReducer = (state = State, action) => {
    switch (action.type) {
        case "COMMENTS":
            state = action.comments;
            break;
        default:
            return state;
    }
    return state;
};

export default CommentsReducer;
