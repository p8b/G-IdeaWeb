///*** Import required redux components
import { createStore, applyMiddleware, combineReducers } from 'redux';
// Used to add developer tool functionalities for debugging
import { composeWithDevTools } from 'redux-devtools-extension';
// used to add async functionality
import thunk from 'redux-thunk';

/// Reducer imports
import NavMenuReducer from './Layout/NavMenuReducer';
import AuthenticationReducer from './Authentication/AuthenticationReducer';
import NavAndFooterVisibilityReducer from './Layout/NavAndFooterVisibilityReducer';

/// Combining all the reducers to be added to redux store
const allReducers = combineReducers(
    {
        NavMenu: NavMenuReducer,
        NavAndFooterVisibility: NavAndFooterVisibilityReducer,
        Authentication: AuthenticationReducer,
    });

/// Create store to be exported
const store = createStore(
    allReducers,
    composeWithDevTools(applyMiddleware(thunk)));
export default store;