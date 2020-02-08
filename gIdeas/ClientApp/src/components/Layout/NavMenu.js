import React, { PureComponent, createRef } from 'react';
import {
    Collapse, Navbar, NavbarBrand, NavbarToggler, NavLink
} from 'reactstrap';
import { Link } from 'react-router-dom';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

// Navigation menu component 
class NavMenu extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            smScreenNavIsOpen: false,
            myAccDropdownBool: false
        };
        // used for auto hiding navbar and its dropdown list
        // in small screens
        this.toggleContainerNavBar = createRef(0);
        this.toggleContainerDropdown = createRef(1);

        // bind props with methods
        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.dropdownToggler = this.dropdownToggler.bind(this);
        this.onClickOutsideHandler = this.onClickOutsideHandler.bind(this);
    }

    componentDidMount() {
        window.addEventListener('click', this.onClickOutsideHandler);
    }
    componentWillUnmount() {
        window.removeEventListener('click', this.onClickOutsideHandler);
    }
    onClickOutsideHandler(event) {
        const { myAccDropdownBool, smScreenNavIsOpen } = this.state;
        // only if the dropdown menu is activated and the user clicks away from the menu
        try {
            if (myAccDropdownBool && !this.toggleContainerDropdown.current.contains(event.target)) {
                this.setState({ myAccDropdownBool: false });
            }
        } catch (e) { }

        // close Navigation menu in small screen when user clicks away from the menu (When user is logged in)
        // this is used so that the navigation menu is not closed when the dropdown items are selected
        try {
            if (smScreenNavIsOpen &&
                (!this.toggleContainerDropdown.current.contains(event.target)
                    && !this.toggleContainerNavBar.current.contains(event.target))) {
                this.setState({
                    smScreenNavIsOpen: !this.state.smScreenNavIsOpen
                });
            }
        }
        // if the user is NOT logged in the error is thrown
        catch (e) {
            // then try to hide the navigation menu in small screen
            try {

                if (smScreenNavIsOpen && !this.toggleContainerNavBar.current.contains(event.target)) {
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
    //Used to toggle my account dropdown menu
    dropdownToggler() {
        this.setState({
            myAccDropdownBool: !this.state.myAccDropdownBool
        });
    }

    // Return Jsx for login, logout and authenticated user's dropdown links
    AuthUserLink() {
        //if authenticated show logout link else show login
        if (this.props.auth.isAuthenticated) {
            /* get all the authenticated user links which must be displayed 
             in the dropdown menu */
            const links = this.props.navMenu.map((link) => {
                if (link.displayOnRight) {
                    return (
                        <NavLink key={link.id} tag={Link}
                            className="text-dark text-nav"
                            onClick={() => {
                                this.toggleNavbar();
                                this.dropdownToggler();
                            }}
                            to={link.path}>
                            {link.displayName}
                        </NavLink>);
                }
                return;
            });
            // Return the dropdown menu for authenticated user else login link 
            return (
                <div className="navbar-nav " ref={this.toggleContainerDropdown}>
                    <NavLink className="text-nav align-middle text-underline"
                        onClick={this.dropdownToggler}>
                        My Account
                    </NavLink>
                    {this.state.myAccDropdownBool && (
                        <span className={"dropdown-menu dropdown-menu-right text-center dropdown-span show"}>
                            {links}
                            <a className="text-dark text-nav"
                                onClick={() => {
                                    this.props.logout();
                                    this.toggleNavbar();
                                    this.dropdownToggler();
                                }}
                            >Logout</a>
                        </span>
                    )}
                </div>
            )
        }
        else {
            return (
                <div>
                    <Link to="/" />
                </div>
            )
        }
    }

    render() {
        return (
            <header >
                <Navbar className="bg-transparent navbar-expand-sm navbar-toggleable-sm box-shadow mb-3 mt-0 pt-0" light>
                    {/* Logo */
                    }
                    <NavbarBrand tag={Link} to="/">
                        <img src={""} alt="[[[ LOGO ]]]" className={'Logo'} />
                    </NavbarBrand>
                    {/* Small screen navbar toggler */
                    }
                    <div ref={this.toggleContainerNavBar} className="w-auto">
                        <NavbarToggler>
                            <span className={'navbar-toggler-icon ml-auto'} onClick={this.toggleNavbar}></span>
                        </NavbarToggler>
                    </div>
                    {// Navbar items 
                    }
                    <Collapse isOpen={this.state.smScreenNavIsOpen} navbar
                        className="d-sm-inline-flex flex-sm-row text-center font-weight-bold">
                        {/* Create the navigation links from the current store nav menu item */
                        }
                        {this.props.navMenu.map(link => {
                            if (!link.displayOnRight) {
                                return (<NavLink key={link.id} tag={Link} className="text-dark text-nav word-break" to={link.path}>{link.displayName}</NavLink>);
                            }
                            return;
                        })}
                        <div className="text-dark text-nav word-break ml-auto"></div>

                        {/* Authorized user links  */
                        }
                        {this.AuthUserLink()}
                    </Collapse>
                </Navbar>
            </header>
        );
    }
}
const mapStateToProps = (state) => {
    return {
        navMenu: state.navMenu,
        auth: state.auth
    }
};
const mapDispatchToProps = {
}
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(NavMenu);