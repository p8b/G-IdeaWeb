///*** import required CSS Files
import 'bootstrap/dist/css/bootstrap.css';
import '../src/components/_MainApp/Styles/main.css';
import '../src/components/_MainApp/Styles/fonts/Antonio/stylesheet.css';
//import '../src/components/_MainApp/fonts.css';
import '../src/components/_MainApp/Styles/icofont.css';

///*** Import React and required components
import React from 'react';
/// render component from react dom for
// rendering the virtual dom components
import { render } from 'react-dom';
/// BrowserRouter component is used to create a
// virtual dom to wrap around the application
import { BrowserRouter } from 'react-router-dom';

/// The application component which 
// includes the entire application
import App from './App';

/// Provider component is used to create 
// global state which wraps around the entire app 
import { Provider } from 'react-redux';
/// Import the store for the redux state management
import store from '../src/ReduxStore/index'

// the base URL for the virtual dom
const baseUrl = document.getElementsByTagName('base')[0]
    .getAttribute('href');

/// The main render method to add redux for state management,
/// create react virtual dom and include the parent component
// of the application which will be added to the index.html rootDiv element
render(
    <Provider store={store}>
        <BrowserRouter basename={baseUrl}>
            <App />
        </BrowserRouter>
    </Provider>
    , document.getElementById('rootDiv'));