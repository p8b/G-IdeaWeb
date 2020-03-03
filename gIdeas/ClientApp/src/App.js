/// Necessary components from react and react-router
/// for creating the component object and enable routing 
/// in the virtual dom of react
import React, { PureComponent } from 'react';
import { Route, Switch } from 'react-router';


// security components
import ProtectedRoute from '../src/components/_MainApp/ProtectedRoute';

/// Page Components
import NavMenu from './components/Layout/NavMenu';
import Footer from './components/Layout/Footer';
import Home from './Pages/Home/Home';
import TeamMembers from './Pages/TeamMembers/TeamMembers';
import Login from './Pages/Login/Login';
import Logout from './Pages/Login/Logout';

/// App component which is the main component in the application
/// which includes the navigation bar, main body (Page Components)
/// and the footer components.
const App = () => {
    /// Returns a JSX type for rendering
    return (
        <div className="BackgroundImage">
            {/** Navigation menu Component */}
            <NavMenu />

            {/** Switch used to navigate between the pages */}
            <Switch>
                {/** Unprotected Routes */}
                <Route exact path='/Login' render={(props) => <Login {...props} />} />
                <Route exact path='/TeamMembers' render={(props) => <TeamMembers {...props} />} />

                {/** Protected Routes */}
                <ProtectedRoute path='/Logout' Render={(props) => <Logout {...props} />} />
                <ProtectedRoute path='/' Render={(props) => <Home {...props} />} />

                {/** All Other Routes Routes */}
                <Route path='*' render={(props) => <Login {...props} />} />
            </Switch>

            {/** Footer Component */}
            <Footer />
        </div>
    );
}
/// Redux Connection before exporting the component
export default App;
