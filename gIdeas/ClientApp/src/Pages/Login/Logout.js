import React, { PureComponent } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Redirect } from "react-router-dom";
import { logout } from "../../Actions/AuthenticationActions";

class Logout extends PureComponent {
    componentDidMount() {
        /// if the user is authenticated then trigger the logout action
        if (this.props.Authentication.isAuthenticated) {
            this.props.logout();
        }
    }
    render() {
        /// then redirect to login page
        return (<Redirect to="/Login" />);
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
    logout,
};
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(Logout);