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
//import { Prev } from 'react-bootstrap/lib/Pagination';



class ViewIdea extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            // state for like button
            totalThumbUps: 0,
            totalThumbsDown: 0,
            ideaAuthor: [],

            idea: new gIdea(),
        }
        this.handleClickL = this.handleClickL.bind(this); 
        this.handleClickD = this.handleClickD.bind(this);
    }
    async ComponentDidMount() {
        const result = await this.props.getIdea(this);

        if (result.Idea != null) {
            this.setState({ idea: result.idea });
        }


    }


    handleClickL = () => {
        this.setState((prevState, { totalThumbUps }) => ({
            totalThumbUps: prevState.totalThumbUps + 1
        }));
    };

    handleClickD = () => {
        this.setState((prevState, { totalThumbsDown }) => ({
            totalThumbsDown: prevState.totalThumbsDown + 1
        }));
    }


    render() {
        const { idea } = this.state;


        return (
            <div className="view-container container-fluid">               
            <div className="img-header">
                    <div className="page-header">View ideas</div>
                </div>
                    <Card className="square">
                        <CardBody>
                            <Row>
                            <CardTitle><b>{idea.Title}</b></CardTitle>
                            </Row>
                            <br />
                            <div className="row justify-content-between">
                            <div className="col-4">
                                <CardSubtitle>{idea.CategoriesToIdeas}</CardSubtitle></div>
                            <div className="col-3">
                                <CardSubtitle>{idea.Author}</CardSubtitle></div>
                            </div>

                        </CardBody>
                        <CardBody className="text-center">
                        <CardText>{this.state.idea.ShortDescription}</CardText>

                            <div className="row justify-content-between">
                                <div className="col-4">
                                <button onClick={this.handleClickL}>👍 Likes: {this.state.idea.TotalThumbUps}</button>
                                </div>
                                <div className="col-4">
                                <button onClick={this.handleClickD}>👎 Dislikes: {this.state.idea.TotalThumbDowns}</button>
                                </div>
                            </div>
                        </CardBody>
                    </Card>
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
