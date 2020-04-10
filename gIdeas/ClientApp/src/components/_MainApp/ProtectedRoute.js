import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

/// Action to Show the navigation bar and footer components
import { silentAuthentication } from '../../Actions/AuthenticationActions';

/// Protected Route functional component is used
/// to decide the access to the routed component.
/// props values is received from the app.js component
const ProtectedRoute = props => {
    //silent Authentication check
    try {
        props.silentAuthentication(props.Authentication.isAuthenticated, props.Authentication.user);
    } catch (e) {
        props.silentAuthentication(props.Authentication.isAuthenticated);
    }

    // if the user is authenticated route them to the intended page
    if (props.Authentication.isAuthenticated) {
        return (<Route path={props.path} render={props.Render} />);
    }

    /// Otherwise redirect them to the login page
    return (<Redirect to="/Login" />);
}

/// Mapping the redux state with component's properties
const mapStateToProps = (state) => {
    return {
        Authentication: state.Authentication
    }
};
/// Map actions (which may include dispatch to redux store) to component
const mapDispatchToProps = {
    silentAuthentication
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(ProtectedRoute);