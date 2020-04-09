import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Card, CardText, CardBody, CardTitle, CardSubtitle } from 'reactstrap';
import CommentBox from './CommentBox';
import "./CommentBox.css";
import "./ViewIdea.css";
import { Multiselect } from 'multiselect-react-dropdown';



import { gIdea } from '../../components/_MainApp/Models';
import { getIdea } from '../../Actions/IdeaActions';
import { SelectThisIdea, DeselectThisIdea } from '../../Actions/ClientSideActions/SelectThisIdea';



class ViewIdea extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            // state for like button
            totalThumbUps: 0,
            idea: new gIdea(),

        }
        this.handleClick = this.handleClick.bind(this); 
    }
    async ComponentDidMount() {
        const result = await this.props.getIdea(this);

 ////////////////////////////////////////////////////////////////////////// CHANGE FIRA CODE FONT != ( fix )
        if (result.Idea != null) {
            this.setState({ idea: result.idea });
        }
    }

    handleClick = () => {
        this.setState((prevState, { totalThumbUps }) => ({
            totalThumbUps: prevState.totalThumbUps + 1
        }));
    };

    render() {
        const { idea } = this.state;
        return (
            <div className="view-container container-fluid">               
            <div className="img-header">
                    <div className="page-header">View ideas</div>
                </div>
                <br />

                    <Card>
                        <CardBody>
                            <Row>
                            <CardTitle><b>{idea.Title}</b></CardTitle>
                            </Row>
                            <br />
                            <div className="row justify-content-between">
                                <div className="col-4">
                                    <CardSubtitle>Category Name</CardSubtitle></div>
                                <div className="col-3">
                                    <CardSubtitle>Author's Name</CardSubtitle></div>
                            </div>

                        </CardBody>
                        <CardBody className="text-center">
                        <CardText>{this.state.idea.ShortDescription}</CardText>

                            <div className="row justify-content-between">
                                <div className="col-4">
                                <button onClick={this.handleClick}>👍 Likes: {this.state.idea.TotalThumbDowns}</button>
                                </div>
                                <div className="col-4">
                                <button onClick={this.handleClick}>👎 Dislikes: {this.state.TotalThumbDowns}</button>
                                </div>
                            </div>
                        </CardBody>
                    </Card>
                    <br />
                    <div>
                        <CommentBox />
                    </div>
            </div>
        );
    }
}
/// Mapping the redux state with component's properties
const mapStateToProps = (state) => {
    return {
        Authentication: state.Authentication,
        SelectedIdea: state.SelectedIdea
    }
};
/// Map actions (which may include dispatch to redux store) to component
const mapDispatchToProps = {
    getIdea,
    SelectThisIdea,
    DeselectThisIdea
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(ViewIdea);
