import NavMenuReducer from './Layout/NavMenuReducer';
import AuthenticationReducer from './Authentication/AuthenticationReducer';
import CommentsReducer from './Pages/HomeReducer';
import { combineReducers } from 'redux';

const allReducers = combineReducers(
    {
        navMenu: NavMenuReducer,
        auth: AuthenticationReducer,
        commentsReducer: CommentsReducer,
    });
export default allReducers;