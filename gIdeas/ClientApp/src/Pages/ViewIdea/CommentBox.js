import "./CommentBox.css";
import React, { PureComponent } from 'react';
import { gComment } from "../../components/_MainApp/Models";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

class CommentBox extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            showComments: false,
            comments: []
        };
    }

    render() {
        return (
            <div className="comment-box">
                <h3 className="text-center">Share your opinion</h3>
                    <div className="comment-form-fields">
                            <textarea placeholder="Write a comment" rows="4" required ref={(textarea) => this._body = textarea}></textarea>
                    </div>
                    <div className="comment-form-actions">
                        <button class="grebutton" type="submit" variant="outline-primary" onClick={this._postComment.bind(this)}>Submit Comment</button>
                    </div>
                    <button class="grebutton" type="button" onClick={() => this.setState({ showComments: !this.state.showComments })}>
                        {this.state.showComments ? 'Hide Comments' : 'View Comments'}
                    </button>

                <h4>Comments</h4>
                <h5 className="comment-count">
                    {this.state.comments.length == 0 ? 'No Comments yet' : `${this.state.comments.length} comments`}
                </h5>
                <div className="comment-list">
                    {this.state.showComments && this.state.comments.map((comment) => {
                        return (
                            <div className="comment" key={comment.id}>
                                <p className="comment-header">{`${comment.user} `}</p>
                                <p className="comment-body">- {comment.comment}</p>
                                <div className="comment-footer">
                                    <a herf="#" className="comment-footer-delete" onClick={this._deleteComment}>Delete / Report Comment</a>
                                </div>
                            </div>
                        );
                    })}
                </div>
            </div>
        );
    }
    async _postComment(event) {
        event.preventDefault();   // prevents page from reloading on submit
        const comment = new gComment({ comment: this._body.value, user: this.props.Authentication.user });

        await

            this.setState({ comments: this.state.comments.concat([comment]) }); // *new array references help React stay fast, so concat works better than push here.
    }
    _reportComment() {
    }
    _deleteComment() {
        alert("-- DELETE Functionality for coordinators / Report functionality for rest of staff...");
    }
}
/// Mapping the redux state with component's properties
const mapStateToProps = (state) => {
    return {
        Authentication: state.Authentication,
    }
};
/// Map actions (which may include dispatch to redux store) to component
const mapDispatchToProps = {

}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(CommentBox);