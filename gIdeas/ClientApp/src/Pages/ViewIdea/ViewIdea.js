import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Card, CardText, CardBody, CardTitle, CardSubtitle } from 'reactstrap';
import CommentBox from './CommentBox';
import "./CommentBox.css";
import RecordPageView from "../../components/RecordPageView";

class ViewIdea extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            IdeaId:1,
            count: 0
        }

        this.handleClick = this.handleClick.bind(this); 
    }
    async ComponentDidMount() {

    }

    handleClick = () => {
        this.setState((prevState, { count }) => ({
            count: prevState.count + 1
        }));
    };

    render() {
        return (
            <Container className="pb-container">
                {/* Record Page view of current page */}
                <RecordPageView IdeaId={this.state.IdeaId} />
                <div className="page-header">Single Idea</div>
                    <Card>
                        <CardBody>
                            <Row>
                                <br />
                                <CardTitle><b>All staff should be given free coffee on Mondays</b></CardTitle>
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
                            <CardText>Some quick example text to build on the card title and  example text to build on the card title and make up the bulk of the ca example text to build on the card title and make up the bulk of the ca  example text to build on the card title and make up the bulk of the ca  example text to build on the card title and make up the bulk of the ca  example text to build on the card title and make up the bulk of the ca  example text to build on the card title and make up the bulk of the camake up the bulk of the card's content.</CardText>

                            <div className="row justify-content-between">
                                <div className="col-4">
                                    <button onClick={this.handleClick}>❤️ Likes: {this.state.count}</button>
                                </div>
                            </div>
                        </CardBody>
                    </Card>
                    <br />
                    <div>
                        <CommentBox />
                    </div>
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
)(ViewIdea);
