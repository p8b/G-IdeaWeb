import React, { Component } from 'react';
import { Link } from 'react-router-dom';

/// connect method from react redux in order to
// connect the component to redux state
import { connect } from 'react-redux';

/// This component is used to combine the required actions
// and reducers to be connected to the current component
import { bindActionCreators } from 'redux';

class Footer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <footer className="footer row pt-3 pb-3 justify-content-center">
                <div className="container">
                    <h4 className="text-center text-white col-12 h3-style m-3">Usefull Links</h4>
                    <div className="text-white w-auto col-12 white-underline">
                        <div className="row justify-content-center">
                            <ul className="w-auto">
                                <li><a className="text-white m-1" href="https://github.com/p8b/G-IdeaWeb">Project on Github</a></li>
                            </ul>
                            <ul className="w-auto">
                                <li><Link className="text-white m-1" to="/TeamMembers">Team members</Link></li>
                            </ul>
                        </div>
                        <div>COMP 1640 Project</div>
                    </div>
                </div>
            </footer>
        );
    }
}
/// Mapping the redux state with component's properties
const mapStateToProps = (state) => {
    return {
        NavAndFooterVisibility: state.NavAndFooterVisibility,
    }
};
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    null
)(Footer);
