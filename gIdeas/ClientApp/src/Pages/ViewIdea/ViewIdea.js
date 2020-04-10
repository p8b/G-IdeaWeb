import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Card, CardText, CardBody, CardTitle, CardSubtitle } from 'reactstrap';
import "./CommentBox.css";
import { Multiselect } from 'multiselect-react-dropdown';


import { getAllCategories } from '../../Actions/CategoryActions';
import { postComment, deleteComment } from '../../Actions/CommentActions';
import { gIdea, gComment } from '../../components/_MainApp/Models';
import { getIdea, postNewVote } from '../../Actions/IdeaActions';
import { SelectThisIdea, DeselectIdea } from '../../Actions/ClientSideActions/SelectedIdeaAction';
import RecordPageView from "../../components/RecordPageView";
import { StringBase64ToBlob } from '../../components/_MainApp/appConst';

class ViewIdea extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            idea: new gIdea(),
            categories: [],
            selectedCategories: [],
            isError: false,
            showComments: false,
            newComment: new gComment({ ideaId: props.ideaId, user: props.currentUser }),
            anonTxt: 'I want my comment to be anonymous',
        };
        this.postComment = this.postComment.bind(this);
        this.deleteThis = this.deleteThis.bind(this);

        // multi selector drop down menu constructor
        this.MultiselectRef = React.createRef();
        this.DislikeThis = this.DislikeThis.bind(this);
        this.LikeThis = this.LikeThis.bind(this);
    }
    async componentDidMount() {
        const result = await this.props.getIdea(this.props.SelectedIdea)
        try {

            console.log(result.Idea.CategoryTags)
            result.Idea.CategoryTags.map(cc => {
                this.state.categories.push({ id: cc.id, name: cc.name })
            })
        } catch (e) { }

        if (result.errors.length === 0) {

            const blob = StringBase64ToBlob(result.Idea.FileBlobStringBase64)

            const h = URL.createObjectURL(blob);
            this.setState({ idea: result.Idea, btn: <a href={h} className="btn custom-btn" download="Documents.zip">Documents.zip</a> })
        }
    }
    async postComment() {
        this.state.newComment.User = this.props.Authentication.user;
        this.state.newComment.IdeaId = this.state.idea.Id;
        const result = await this.props.postComment(this.state.newComment);
        if (result.isSuccess) {
            const result = await this.props.getIdea(this.props.SelectedIdea)
            try {

                console.log(result.Idea.CategoryTags)
                result.Idea.CategoryTags.map(cc => {
                    this.state.categories.push({ id: cc.id, name: cc.name })
                })
            } catch (e) { }

            if (result.errors.length === 0) {

                const blob = StringBase64ToBlob(result.Idea.FileBlobStringBase64)

                const h = URL.createObjectURL(blob);
                this.setState({ idea: result.Idea, btn: <a href={h} className="btn custom-btn" download="Documents.zip">Documents.zip</a> })
            }
            console.log(this.state.idea.Comments)
        }
    }
    async deleteThis(comment) {
        const result = await this.props.deleteComment(comment);
        if (result.isDeleted) {
            const result = await this.props.getIdea(this.props.SelectedIdea)
            try {

                console.log(result.Idea.CategoryTags)
                result.Idea.CategoryTags.map(cc => {
                    this.state.categories.push({ id: cc.id, name: cc.name })
                })
            } catch (e) { }

            if (result.errors.length === 0) {

                const blob = StringBase64ToBlob(result.Idea.FileBlobStringBase64)

                const h = URL.createObjectURL(blob);
                this.setState({ idea: result.Idea, btn: <a href={h} className="btn custom-btn" download="Documents.zip">Documents.zip</a> })
            }
        }
    }
    async DislikeThis() {
        const result = await this.props.postNewVote(this.state.idea.Id, false);

        if (result.isSuccessful) {
            const result = await this.props.getIdea(this.props.SelectedIdea)
            try {

                console.log(result.Idea.CategoryTags)
                result.Idea.CategoryTags.map(cc => {
                    this.state.categories.push({ id: cc.id, name: cc.name })
                })
            } catch (e) { }

            if (result.errors.length === 0) {

                const blob = StringBase64ToBlob(result.Idea.FileBlobStringBase64)

                const h = URL.createObjectURL(blob);
                this.setState({ idea: result.Idea, btn: <a href={h} className="btn custom-btn" download="Documents.zip">Documents.zip</a> })
            }
        }
    }
    async LikeThis() {
        const result = await this.props.postNewVote(this.state.idea.Id, true);
        if (result.isSuccessful) {
            const result = await this.props.getIdea(this.props.SelectedIdea)
            try {

                console.log(result.Idea.CategoryTags)
                result.Idea.CategoryTags.map(cc => {
                    this.state.categories.push({ id: cc.id, name: cc.name })
                })
            } catch (e) { }

            if (result.errors.length === 0) {

                const blob = StringBase64ToBlob(result.Idea.FileBlobStringBase64)

                const h = URL.createObjectURL(blob);
                this.setState({ idea: result.Idea, btn: <a href={h} className="btn custom-btn" download="Documents.zip">Documents.zip</a> })
            }
        }
    }
    render() {
        return (
            <Container className="pb-container">
                {/* Record Page view of current page */}
                <RecordPageView IdeaId={this.state.IdeaId} />
                <div className="view-container container-fluid">
                    <div className="img-header">
                        <div className="page-header">View ideas</div>
                    </div>
                    <Row className="mt-4">
                        <h4 className="col-6">Author: {this.state.idea.Author.FirstName} {this.state.idea.Author.Surname}
                        </h4>
                        <h4 className="col-6">Role: {this.state.idea.Author.Role.Name}
                        </h4>
                        <h4 className="col-12">Department: {this.state.idea.Author.Department.Name}
                        </h4>

                        <div className="col-12 col-md-6">
                            <label>Title of idea:</label>
                            <input disabled
                                type="text"
                                className="form-control"
                                name="Title"
                                value={this.state.idea.Title}
                            />
                            {/*<div className="errorMsg">{this.state.errors.Title}</div>*/}
                        </div>
                        <div className="col-12 col-md-6">
                            <label className="col-form-label pt3"><small>Document</small></label>
                            {
                                this.state.btn != null && this.state.btn
                            }
                            {
                                this.state.btn === null && "No Document attached"
                            }
                        </div>
                        <div className="col-12 col-md-6">
                            <label>Description: </label>
                            <textarea disabled
                                type="text"
                                className="form-control"
                                value={this.state.idea.ShortDescription}
                            />
                        </div>
                        <div className="col-12 col-md-6">
                            <Multiselect
                                placeholder={`Category Tagged: ${this.state.categories.length}`}
                                options={this.state.categories} // Options to display in the dropdown
                                displayValue="name" // Property name to display in the dropdown options
                            />
                        </div>
                        <div className="col-12 col-md-6">
                        </div>
                        <div className="col-12 col-md-6">
                            <div className="row">
                                <div className="col-4">
                                    <button onClick={this.LikeThis}>👍 Likes: {this.state.idea.TotalThumbUps}</button>
                                </div>
                                <div className="col-4">
                                    <button onClick={this.DislikeThis}>👎 Dislikes: {this.state.idea.TotalThumbDowns}</button>
                                </div>
                            </div>
                        </div>
                    </Row>
                    <div className="mt-3">
                        <div className="comment-box">
                            <h3 className="text-center">Share your opinion</h3>
                            <textarea placeholder="Write a comment" rows="4" required onChange={(d) => this.state.newComment.Description = d.target.value}></textarea>
                            <div className="comment-form-actions">
                                <button class="grebutton" variant="outline-primary" onClick={this.postComment}>Submit Comment</button>
                            </div>
                            <button class="grebutton" type="button" onClick={() => this.setState({ showComments: !this.state.showComments })}>
                                {this.state.showComments ? 'Hide Comments' : 'View Comments'}
                            </button>
                            <div className="col-12 col-md-6 mt-2">
                                <form className="bg-white text-dark p-3">
                                    <div className="custom-control custom-checkbox mb-3">
                                        <input onChange={(e) => {
                                            this.state.newComment.IsAnonymous = e.target.checked
                                            var txt;
                                            if (!this.state.newComment.IsAnonymous) {
                                                txt = 'I want my comment to be anonymous'
                                            } else {
                                                txt = 'Your comment will be anonymous'
                                            }
                                            this.setState({ anonTxt: txt });
                                        }}
                                            type="checkbox" className="custom-control-input"
                                            id="anon" required />
                                        <label className="custom-control-label" htmlFor="anon"  >{this.state.anonTxt}</label>
                                    </div>
                                </form>
                            </div>
                            <h4 className="mt-3">Comments</h4>
                            <h5 className="comment-count">
                                {this.state.idea.Comments.length == 0 ? 'No Comments yet' : `${this.state.idea.Comments.length} comments`}
                            </h5>
                            <div className="comment-list">
                                {this.state.showComments && this.state.idea.Comments.map((comment) => {
                                    return (
                                        <div className="comment" key={comment.id}>
                                            <p className="comment-header">{`${comment.user.firstName} ${comment.user.surname} `}</p>
                                            <p className="comment-body">- {comment.description}</p>
                                            <div className="comment-footer">
                                                <a herf="#" className="comment-footer-delete" onClick={() => this.deleteThis(comment)}>Delete / Report Comment</a>
                                            </div>
                                        </div>
                                    );
                                })}
                            </div>
                        </div>
                    </div>
                </div>
            </Container>
        );
    }
}
/// Mapping the redux state with component's properties
const mapStateToProps = (state) => {
    return {
        Authentication: state.Authentication,
        SelectedIdea: state.SelectedIdea,
    }
};
/// Map actions (which may include dispatch to redux store) to component
const mapDispatchToProps = {
    getIdea,
    SelectThisIdea,
    DeselectIdea,
    getIdea,
    getAllCategories,
    postNewVote,
    deleteComment,
    postComment

}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(ViewIdea);
