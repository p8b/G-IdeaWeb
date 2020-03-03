import React, { Component, PureComponent } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Link, Redirect } from "react-router-dom";
import { authenticate} from "./Actions/AuthenticationAction";
import "./Login.css";
import "./GreUni.jpg";

/// Action to hide the navigation bar and footer components
const footerAndNavbarHide = () => { return { type: "INVISIBLE" } };

class Login extends PureComponent {
    constructor(props) {
        super(props)
        /// Login state 
        this.state =
        {
            email: "",
            password: ""
        }
        /// trigger the hide navigation an footer action
        this.props.footerAndNavbarHide();
    }
    /// Render component method
    render() {
        /// If user is authenticated then redirect the user to home page
        if (this.props.Authentication.isAuthenticated) {
            return (<Redirect to="/" />);
        }

        /// Else the user is not logged-in thus return the login page JSX 
        return (
            <div className="login-img-container">
                <div className='login-bg-img'>
                    {/***** Login Container *****/}
                    <div align="right" className="login-container m-shadow-box">
                        {/***** Logo *****/}
                        <div className="container-cell-top login-img-logo-container">
                            <img alt="Greenwich University Logo" className="login-logo-img" src="/Images/GreenwichUni_Logo.png" />
                        </div>
                        <div className="row container-cell-top ">
                            <div className="col-12">
                                {/***** Email Input *****/}
                                <div className="container-cell-right mt-3">
                                    <input className="login-input" type="text" placeholder="Email" onChange={i => this.setState({ email: i.target.value })} />
                                </div>
                                {/***** Password Input *****/}
                                <div className="container-cell-right">
                                    <input className="login-input" type="password" placeholder="Password" onChange={i => this.setState({ password: i.target.value })} />
                                </div>
                                {/***** Login Button *****/}
                                <div className="container-cell-right">
                                    <button type="submit" className="login-button" onClick={() => this.props.authenticate(this.state)}>Log In</button>
                                </div>
                            </div>
                            {/***** Error display container *****/}
                            <div className="col-12">
                                <div className="container-cell-right">
                                    {(() => {
                                        // If no error message is to be show do not render warning icon
                                        if (this.props.Authentication.message != "" && this.props.Authentication.message != undefined)
                                            return <i className="icofont-warning"></i>;
                                    })()}
                                    <i className="error-style"> {this.props.Authentication.message}</i>
                                </div>
                            </div>
                        </div>
                        <div className="container-cell-top row p-3 login-footer-container">
                            {
                                //<a href="Google">
                                //    <img src="/Images/GoogleButton.jpg" alt="GoogleButton" className="google-login" onClick={this.myfunction} />
                                //</a>
                            }
                            <div className="container-cell-right col-auto">
                                <div className="link-style">
                                    <i className="icofont-ui-unlock"> </i>
                                    <Link to="/ForgotPassword">Can't log in?</Link>
                                </div>
                            </div>
                            <div className="container-cell-right col-auto">
                                <div className="link-style">
                                    <i className="icofont-question-circle"> </i>
                                    <Link to="/Help" >Need more help?</Link>
                                </div>
                            </div>
                        </div>
                    </div>
                    {/***** Bottom Title Container *****/}
                    <div align="right" className="login-container m-shadow-box">
                        <div className="row container-cell-top ">

                            <div className="col-12">
                                <div className="container-cell-right mt-3 text-center">
                                    <h2 className="text-bold">Greenwich University Ideas</h2>
                                </div>
                                <div className="container-cell-right center-img">
                                    <div className="col">
                                        <img alt="Brain Image" src="/Images/Brainstorm_100px.png" />
                                        <img alt="Brain Image" src="/Images/Student Male_100px.png" />
                                        <img alt="Brain Image" src="/Images/Goal_100px.png" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

/// Mapping the redux state with component's properties
const mapStateToProps = (state) => {
    return {
        NavAndFooterVisibility: state.NavAndFooterVisibility,
        Authentication: state.Authentication
    }
};
/// Map actions (which may include dispatch to redux store) to component
const mapDispatchToProps = {
    footerAndNavbarHide,
    authenticate,
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(Login);

