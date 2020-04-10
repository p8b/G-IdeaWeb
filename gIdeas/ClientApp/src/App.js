/// Necessary components from react and react-router
/// for creating the component object and enable routing 
/// in the virtual dom of react
import React from 'react';
import { Route, Switch } from 'react-router';

// security components
import ProtectedRoute from '../src/components/_MainApp/ProtectedRoute';
import CustomRoute from './components/_MainApp/CustomRoute';

/// Page Components
import AddCategory from './Pages/AddCategory/AddCategory';
import BrowseIdea from './Pages/BrowseIdea/BrowseIdea';
import Home from './Pages/Home/Home';
import Login from './Pages/Login/Login';
import Logout from './Pages/Login/Logout';
import PageNotFound from './Pages/PageNotFound/PageNotFound';
import SearchUser from './Pages/SearchUser/SearchUser';
import ViewIdea from './Pages/ViewIdea/ViewIdea';
import Statistics from './Pages/Statistics/Statistics';
import TeamMembers from './Pages/TeamMembers/TeamMembers';
import NavMenu from './components/Layout/NavMenu';
import Footer from './components/Layout/Footer';
import NewIdea from './Pages/NewIdea/NewIdea';

/// App component which is the main component in the application
/// which includes the navigation bar, main body (Page Components)
/// and the footer components.
const App = () => {
    /// Returns a JSX type for rendering
    return (
        <div className="BackgroundImage">
            {/** Navigation menu Component */
                window.location.pathname != "/Login" &&
                <NavMenu />
            }

            {/** Switch used to navigate between the pages */}
            <Switch>
                {/** Unprotected Routes */}
                <CustomRoute exact path='/' Render={(props) => <Home {...props} />} />
                <CustomRoute exact path='/Login' Render={(props) => <Login {...props} />} />
                <CustomRoute exact path='/TeamMembers' Render={(props) => <TeamMembers {...props} />} />
                <CustomRoute exact path='/Logout' Render={(props) => <Logout {...props} />} />

                {/** Protected Routes */}
                <ProtectedRoute exact path='/AddCategory' Render={(props) => <AddCategory {...props} />} />
                <ProtectedRoute exact path='/BrowseIdea' Render={(props) => <BrowseIdea {...props} />} />
                <ProtectedRoute exact path='/UserManagement' Render={(props) => <SearchUser {...props} />} />
                <ProtectedRoute path='/ViewIdea' Render={(props) => <ViewIdea {...props} />} />
                <ProtectedRoute exact path='/Statistics' Render={(props) => <Statistics {...props} />} />
                <ProtectedRoute exact path='/NewIdea' Render={(props) => <NewIdea {...props} />} />

                {/** All Other Routes */}
                <CustomRoute path='*' Render={(props) => <PageNotFound {...props} />} />
            </Switch>

            {/** Footer Component **/
                window.location.pathname != "/Login" &&
                <Footer />
            }
        </div>
    );
}
export default App;
