import React, { Component, PureComponent } from 'react';
import { Container } from 'reactstrap';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { getComments, postComment } from './Actions/HomeAction';

class Home extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            newComment: ""
        };
    }
    componentDidMount() {
        this.props.getComments(); 
    }
    render() {
        return (
            <Container>
                <label>Add Comments</label>
                <textarea className="form-control col-6" onChange={(i) => this.setState({ newComment: i.target.value })} value={this.state.newComment} />
                <button className="btn btn-light" onClick={() => {
                    this.props.postComment(this.state.newComment).then(data => {
                        this.props.getComments();
                        this.setState({ newComment: "" });
                    })
                }}>Post</button>
                <h2>Comments: {this.props.comments.length}</h2>
                {this.props.comments.map((comment) => {
                        return (
                            <div key={comment.id}>
                                {comment.description}
                            </div>
                        )
                    })
                }
            </Container>
        );
    }
}
const mapStateToProps = (state) => {
    return {
        comments: state.commentsReducer
    }
};
const mapDispatchToProps = {
    getComments,
    postComment
}
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(Home);