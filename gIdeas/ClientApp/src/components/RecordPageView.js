import React, { PureComponent } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { postSubmitPageView } from "../Actions/PageViewActions";

class RecordPageView extends PureComponent {
    async componentDidMount() {
        await this.props.postSubmitPageView(this.props.IdeaId);
    }
    /// Render component method
    render() {
        return (
            <div/>
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
    postSubmitPageView
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(RecordPageView);

