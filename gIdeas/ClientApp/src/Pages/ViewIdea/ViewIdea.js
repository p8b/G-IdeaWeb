import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Card, CardText, CardBody, CardTitle, CardSubtitle } from 'reactstrap';
import CommentBox from './CommentBox';
import "./CommentBox.css";
import { Multiselect } from 'multiselect-react-dropdown';


import { getAllCategories } from '../../Actions/CategoryActions';
import { gIdea } from '../../components/_MainApp/Models';
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
            isError: false
        }

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
                        <CommentBox />
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
    postNewVote

}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(ViewIdea);
