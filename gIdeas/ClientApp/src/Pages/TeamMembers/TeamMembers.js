﻿import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';

class TeamMembers extends PureComponent {
    render() {
        return (
            <Container>
                <div className="page-header">Team Members</div>
            </Container>
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
)(TeamMembers);