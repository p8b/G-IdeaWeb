import React, { PureComponent } from 'react';
import { Route, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { AdminNav, DefaultNav } from './../Layout/Actions/NavMenuAction';
import { silentAuthentication } from '../../Pages/Login/Actions/AuthenticationAction';

/// Action to hide the navigation bar and footer components
const footerAndNavbarShow = () => { return { type: "VISIBLE" } };

/// Protected Route functional component is used
/// to decide the access to the routed component.
/// props values is received from the app.js component
class ProtectedRoute extends PureComponent {
    componentDidMount() {
         //silent Authentication check
        this.props.silentAuthentication();
        /// enable navigation bar and footer components
        this.props.footerAndNavbarShow();

        /// Check which menu items to show for the user
        switch (this.props.Authentication.accessClaim) {
            case "Admin":
                this.props.AdminNav();
                break;
            default:
                this.props.DefaultNav();
                break;
        }
    }


    render() {
        const { path, Render } = this.props;
        // if the user is NOT authenticated then redirect them to the login page
        if (!this.props.Authentication.isAuthenticated) {
            return (<Route exact path={path} render={()=> <Redirect to="/Login" />} />);
        }

        /// Otherwise they authenticated thus route them to the intended page
        return (<Route exact path={path} render={Render} />);
    }
}

/// Mapping the redux state with component's properties
const mapStateToProps = (state) => {
    return {
        Authentication: state.Authentication
    }
};
/// Map actions (which may include dispatch to redux store) to component
const mapDispatchToProps = {
    AdminNav,
    DefaultNav,
    silentAuthentication,
    footerAndNavbarShow,
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(ProtectedRoute);