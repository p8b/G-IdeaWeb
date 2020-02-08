import 'bootstrap/dist/css/bootstrap.css';
import '../src/components/_MainApp/main.css';
import React from 'react';
import { render } from 'react-dom';
import { BrowserRouter } from 'react-router-dom';

import App from './App';

// Redux store 
import { createStore, applyMiddleware } from 'redux';
import { Provider } from 'react-redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import thunk from 'redux-thunk';
import allReducers from '../src/ReduxStore/index'

const store = createStore(allReducers, composeWithDevTools(applyMiddleware(thunk)));

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');

render(
    <Provider store={store}>
        <BrowserRouter basename={baseUrl}>
            <App />
        </BrowserRouter>
    </Provider>
    , document.getElementById('root'));