import React, { PureComponent } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Link, Redirect } from "react-router-dom";
import { authenticate } from "../../Actions/AuthenticationActions";
import "./Login.css";
import "./GreUni.jpg";
import RecordPageView from "../../components/RecordPageView";

class Login extends PureComponent {
    constructor(props) {
        super(props)
        /// Login state 
        this.state =
        {
            email: "",
            password: "",
            rememberMe: false
        }
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
                {/* Record Page view of current page */}
                <RecordPageView IdeaId="0"/>
                <div className='login-bg-img'>
                    {/***** Login Container ****/}
                    <div align="right" className="login-container m-shadow-box">
                        {/***** Logo ****/}
                        <div className="container-cell-top login-img-logo-container">
                            <Link to="/">
                                <img alt="Greenwich University Logo" className="login-logo-img" src="/Images/GreenwichUni_Logo.png" />
                            </Link>
                        </div>
                        <div className="container-cell-top">
                            {/***** Email Input ****/}
                            <div className="container-cell-right mt-3">
                                <input className="login-input" type="text" placeholder="Email" onChange={i => this.setState({ email: i.target.value })} />
                            </div>
                            {/***** Password Input ****/}
                            <div className="container-cell-right">
                                <input className="login-input" type="password" placeholder="Password" onChange={i => this.setState({ password: i.target.value })} />
                            </div>
                            {/***** Remember me check-box Input ****/}
                            <div className="container-cell-right pl-5">
                                <input type="checkbox" className="form-check-input" id="rememberMe" onChange={i => i.target.value == "on" ? this.setState({ rememberMe: true }) : this.setState({ rememberMe: false })} />
                                <label className="form-check-label" htmlFor="rememberMe">Remember Me</label>
                            </div>
                            {/***** Login Button ****/}
                            <div className="container-cell-right mb-3">
                                <button type="submit" className="login-button" onClick={() => this.props.authenticate(this.state)}>Log In</button>
                            </div>
                            {/***** Error display container ****/}
                            <div className="container-cell-right">
                                {// If no error message is to be show do not render warning icon
                                    this.props.Authentication.errors.length != 0 &&
                                    this.props.Authentication.errors.map(e => {
                                        return <i key={e.key} className="icofont-warning">{e.value}</i>;
                                    })
                                }
                            </div>
                        </div>
                        <div className="container-cell-top p-3 mt-4 login-footer-container">
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
                    {/***** Bottom Title Container ****/}
                    <div align="right" className="login-container m-shadow-box">
                        <div className="container-cell-top ">

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
        );
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
    authenticate,
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(Login);

