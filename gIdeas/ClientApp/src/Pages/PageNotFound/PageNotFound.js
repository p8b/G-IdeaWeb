import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';

import "./PageNotFound.css";


class PageNotFound extends PureComponent {
    render() {
        return (
            <div>


                <section class="py-5">
                    <div class="container-fluid pnfcontainer" >
                         <div className="img-header">
                    <div className="page-header">Page not found</div>
                </div> <br/>
                        <div class="container-fluid text-center" >
                            <div class="row content">
                                <div class="col-sm-2">
                                    <p><a href="/Home" >Home</a></p>
                                    <p><a href="/Faculties">Departments</a></p>
                                </div>
                                <div class="col-sm-8 text-left">
                                    <h4>Sorry, but the page you were expecting to see could not be found.</h4>
                                    <p>
                                        <ul>
                                            <li>Please check the spelling of the web address you are looking for.</li>
                                            <li>The navigation links on the left will redirect you to different areas of Gre-Ideas.</li>
                                        </ul>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        );
    }
}
/// Mapping the redux state with component's properties
const mapStateToProps = (state) => {
    return {
    }
};
/// Map actions (which may include dispatch to redux store) to component
const mapDispatchToProps = {
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(PageNotFound);
