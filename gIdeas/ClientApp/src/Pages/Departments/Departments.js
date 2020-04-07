import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';
import "./Departments.css";



class Departments extends PureComponent {
 
    render() {
        return (
            <div class="container">
                <div class="grid-row">
                    <div class="grid-item">
                        <a class="wrapping-link" href="#"></a>
                        <div class="grid-item-wrapper">
                            <div class="grid-item-container">
                                <div class="grid-image-top fac1">
                                    <span class="centered project-image-bg fac1-image"></span>
                                </div>
                                <div class="grid-item-content">
                                    <span class="item-title">Faculty of Liberal Arts and Sciences</span>
                                    <span class="item-excerpt">The Arts and Sciences faculty includes ideas posted from the following departments:</span>
                                    <span class="more-info">View ideas <i class="fas fa-long-arrow-alt-right"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="grid-item">
                        <a class="wrapping-link" href="#" target="_blank"></a>
                        <div class="grid-item-wrapper">
                            <div class="grid-item-container">
                                <div class="grid-image-top fac2">
                                    <span class="centered project-image-bg fac2-image"></span>
                                </div>
                                <div class="grid-item-content">
                                    <span class="item-title">Business School</span>
                                    <span class="item-excerpt">The business school encompases the departments of blah and blah...</span>
                                    <span class="more-info">View ideas <i class="fas fa-long-arrow-alt-right"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="grid-item">
                        <a class="wrapping-link" href="#" target="_blank"></a>
                        <div class="grid-item-wrapper">
                            <div class="grid-item-container">
                                <div class="grid-image-top fac3">
                                    <span class="centered project-image-bg fac3-image"></span>
                                </div>
                                <div class="grid-item-content">
                                    <span class="item-title">Faculty of Education Health and Human Sciences</span>
                                    <span class="item-excerpt">The faculty of Education, Health and Human Sciences encompases the departments of ...</span>
                                    <span class="more-info">View ideas <i class="fas fa-long-arrow-alt-right"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="grid-item">
                        <a class="wrapping-link" href="#" target="_blank"></a>
                        <div class="grid-item-wrapper">
                            <div class="grid-item-container">
                                <div class="grid-image-top fac4">
                                    <span class="centered project-image-bg fac4-image"></span>
                                </div>
                                <div class="grid-item-content">
                                    <span class="item-title">Faculty of Engineering and Science</span>
                                    <span class="item-excerpt">The faculty of Engineering and Science encompases the departments of ...</span>
                                    <span class="more-info">View ideas <i class="fas fa-long-arrow-alt-right"></i></span>
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
    }
};
/// Map actions (which may include dispatch to redux store) to component
const mapDispatchToProps = {
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(Departments);
