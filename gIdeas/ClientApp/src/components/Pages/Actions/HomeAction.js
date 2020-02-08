import { getCookieValue } from '../../_MainApp/appConst';

export const postComment = (comment = "") => {
    return async dispatch => {
        let state = {
            status:"Failed"
        };
        const response = await fetch("api/Comment/Post", {
            method: "POST",
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
                'X-AntiForgery-TOKEN': getCookieValue("AntiForgery-TOKEN"),
            },
            body: JSON.stringify(comment),
        }).catch(err => console.log(err));

        switch (response.state) {
            case 200: /// "ok" response 
                await response.json().then(data => {
                    state.comments = data.comment;
                })
                return true;
            case 400: /// "bad request" response 
            default:
                break;
        }
        return false;
    }
}
export const getComments = () => {
    return async dispatch => {
        let state = {
            type: "COMMENTS",
            comments: [{
                id: 0,
                description: "Failed to load comments"
            }]
        };

        const response = await fetch("api/Comment/Get")
            .catch(err => console.log(err));

        switch (response.status) {
            case 200: // "ok" response
                await response.json().then(data => {
                    state.comments = data
                });
                break;
            case 400:
            default:
                break;
        }
        dispatch(state);
        return state;
    }
}