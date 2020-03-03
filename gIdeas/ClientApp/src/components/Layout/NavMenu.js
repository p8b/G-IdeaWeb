import React, { Component, createRef } from 'react';
/// Navigation components used from react strap library
import {
    Collapse, Navbar, NavbarBrand, NavbarToggler, NavLink, Container
} from 'reactstrap';
/// Use Link component from react router dom in order
// to navigate to another page
import { Link } from 'react-router-dom';

/// connect method from react redux in order to
// connect the component to redux state
import { connect } from 'react-redux';

// Navigation menu component
class NavMenu extends Component {
    constructor(props) {
        super(props);
        /// Component state
        this.state = {
            /// use to make the small screen navigation menu visibility
            smScreenNavIsOpen: false,
        };
        // used for auto hiding navbar and its drop-down list
        // for small screens
        this.toggleContainerNavBar = createRef(0);

        // bind props with methods
        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.onClickOutsideHandler = this.onClickOutsideHandler.bind(this);
    }

    /// After the component is mounted
    componentDidMount() {
        /// Create a click event listener used for auto 
        // hiding navbar and its drop - down list for small screens
        window.addEventListener('click', this.onClickOutsideHandler);
    }
    /// Before the component is unmounted
    componentWillUnmount() {
        /// Remove a click event listener used for auto 
        // hiding navbar and its drop - down list for small screens
        window.removeEventListener('click', this.onClickOutsideHandler);
    }

    /// Add auto hide functionalities to the 
    /// drop-down menu in small devices
    onClickOutsideHandler(event) {
        const { smScreenNavIsOpen } = this.state;
        // close Navigation menu in small screen when user clicks away from the menu (When user is logged in)
        // this is used so that the navigation menu is not closed when the drop-down items are selected
        try {
            if (smScreenNavIsOpen &&
                !this.toggleContainerNavBar.current.contains(event.target)) {
                this.setState({
                    smScreenNavIsOpen: !this.state.smScreenNavIsOpen
                });
            }
        }
        // if the user is NOT logged in the error is thrown
        catch (e) {
            // then try to hide the navigation menu in small screen
            try {
                if (smScreenNavIsOpen &&
                    !this.toggleContainerNavBar.current.contains(event.target)) {
                    this.setState({
                        smScreenNavIsOpen: !this.state.smScreenNavIsOpen
                    });
                }
            } catch (e) { }
        }
    }
    // Used to change the mobile nav span icon
    toggleNavbar() {
        this.setState({
            smScreenNavIsOpen: !this.state.smScreenNavIsOpen
        });
    }

    render() {
        /// Hide the header if necessary (Used for login page)
        if (!this.props.NavAndFooterVisibility)
            return (<div />)

        /// Otherwise render the header components
        return (
            <header>
                {/** Top bar */}
                <div className="top-bar">
                    <Container>
                        <div className="top-bar-txt row">
                            <div className="m-1 ml-auto">
                                <font className="txt-gray">Welcome:</font> First Name
                            </div>
                            <Link className="top-bar-logout-link" to="/Logout">Logout</Link>
                        </div>
                    </Container>
                </div>

                {/** Navigation Menu */}
                <Navbar className="bg-white navbar-expand-sm navbar-toggleable-sm box-shadow padding-0 ">
                    <Container className="p-0">
                        {/* Logo */}
                        <NavbarBrand tag={Link} className="p-0 m-0" to="/">
                            <img className="logo" src="/Images/GreenwichUni_Logo.png" alt="Greenwich University Logo" />
                        </NavbarBrand>

                        {/* Small screen navbar toggler */}
                        <div ref={this.toggleContainerNavBar} className="w-auto">
                            <NavbarToggler className="menu-toggler" onClick={this.toggleNavbar}>
                                <span className={'ml-auto'} >Menu</span>
                            </NavbarToggler>
                        </div>

                        {/* Navbar items */}
                        <Collapse isOpen={this.state.smScreenNavIsOpen} navbar
                            className="d-sm-inline-flex flex-sm-row text-center font-weight-bold">
                            {/* Authorized user links */}
                            <div className="text-dark text-nav word-break ml-auto"></div>
                            {this.props.NavMenu.map(link => {
                                return (<NavLink key={link.id} tag={Link} className="nav-item" to={link.path}>{link.displayName}</NavLink>);
                            })}
                        </Collapse>
                    </Container>
                </Navbar>
            </header >
        );
    }
}
/// Mapping the redux state with component's properties
const mapStateToProps = (state) => {
    return {
        NavMenu: state.NavMenu,
        NavAndFooterVisibility: state.NavAndFooterVisibility,
        Authentication: state.Authentication,
    }
};
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    null
)(NavMenu);