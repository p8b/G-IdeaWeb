import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import Home from './components/Pages/home';
import NavMenu from './components/Layout/NavMenu';
import { Footer } from './components/Layout/Footer';


export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <div>
                <NavMenu />
                <Switch>
                    <Route exact path='/' render={props => <Home {...props}/>} />
                </Switch>
                <Footer />
            </div>
        );
    }
}
