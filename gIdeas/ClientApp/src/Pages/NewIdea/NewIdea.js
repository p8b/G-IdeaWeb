import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';
import { getAllCategories } from '../../Actions/CategoryActions';
import { gIdea, gUser, gDocument, gCategoryTag, gError } from '../../components/_MainApp/Models';
import { Base64ToArrayBuffer, StringBase64ToBlob } from '../../components/_MainApp/appConst';
import JSZip from 'jszip';
import RecordPageView from "../../components/RecordPageView";
import { postNewIdea } from "../../Actions/IdeaActions";

import { Button, Row, Label, FormGroup, Input } from 'reactstrap';
import { Multiselect } from 'multiselect-react-dropdown';
import './NewIdea.css';
import { Redirect } from 'react-router-dom';

class NewIdea extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            categories: [],
            selectedCategories: [],
            idea: new gIdea(),
            anonTxt: 'I want my idea to be anonymous',
            termsAndConditions: false,
            RedirectToSingleIdea: false,
            errors: [],
            isError: false
        }

        // multi selector drop down menu constructor
        this.MultiselectRef = React.createRef();
        this.submitIdea = this.submitIdea.bind(this);
        this.CateroryOnSelect = this.CateroryOnSelect.bind(this);
        this.CatergoryOnRemove = this.CatergoryOnRemove.bind(this);
    }
    async componentDidMount() {
        var resultCategories = await this.props.getAllCategories();
        try {
            resultCategories.categories.map(c => {
                this.state.categories.push({ id: c.Id, name: c.Name })
            })
            this.setState({ categories: this.state.categories });
        } catch (e) { }

    }
    async uploadDocument(files) {
        var zip = new JSZip();
        var zipDocuments = zip.folder("Documents");
        for (let i = 0; i < files.length; i++) {
            zipDocuments.file(files[i].name, files[i].arrayBuffer);
        }

        let zipFile = await zip.generateAsync({ type: "arraybuffer" });

        let reader = new FileReader();
        reader.readAsDataURL(new Blob([zipFile]));
        reader.onload = () => {
            this.state.idea.FileBlobStringBase64 = reader.result.replace('data:application/octet-stream;base64,', '');
        };

        
    }
    async submitIdea() {

        this.state.errors = [];
        if (!this.state.termsAndConditions) {
            this.state.errors.push(new gError({ key: 0, value: "Terms and condition must be accepted." }))
            this.setState({ isError: !this.state.isError });
            return;
        }

        this.state.idea.Author = this.props.Authentication.user;
        this.state.selectedCategories.map(c => {
            this.state.idea.CategoryTags.push(new gCategoryTag(c))
        })
        const result = await this.props.postNewIdea(this.state.idea);


        if (result.errors.length == 0) {
            this.setState({ RedirectToSingleIdea: true })
            return
        } else {
            this.setState({ errors: result.errors })
            return
        } 

        const blob = StringBase64ToBlob(result.newIdea.FileBlobStringBase64)

        const h = URL.createObjectURL(blob);
        this.setState({
            btn: <a href={h} className="btn custom-btn" download="Documents.zip">Documents.zip</a>
        });
    }
    CateroryOnSelect(e) {
        this.state.selectedCategories = e;
    }
    CatergoryOnRemove(e) {
        this.state.selectedCategories = e;
    }
    render() {
        if (this.state.RedirectToSingleIdea)
            return (<Redirect to="/ViewIdea" />)

        return (
            <Container className="custom-container">
                {/* Record Page view of current page */}
                <RecordPageView IdeaId="0" />
                <div className="">
                    <div className="img-header mb-2">
                        <div className="page-header">Share your ideas</div>
                    </div>
                    <Row>
                        <h4 className="col-12">Author: {this.props.Authentication.user.FirstName} {this.props.Authentication.user.Surname}
                        </h4>

                        <div className="col-12 col-md-6">
                            <label>Title of idea:</label>
                            <input
                                type="text"
                                className="form-control"
                                name="Title"
                                placeholder="Title of your idea"
                                onChange={(e) => this.state.idea.Title = e.target.value}
                            />
                            {/*<div className="errorMsg">{this.state.errors.Title}</div>*/}
                        </div>
                        <div className="col-12 col-md-6">
                            <label className="col-form-label pt3"><small>Document</small></label>
                            <div className="custom-file">
                                <input id="UploadDocument" type="file" multiple accept="/*" className="custom-file-input"
                                    onChange={e => this.uploadDocument(e.target.files)} />
                                <div id="lblUploadDocument" className="custom-file-label">Upload Document(s)</div>
                            </div>
                            {
                                this.state.btn != null && this.state.btn
                            }
                        </div>
                        <div className="col-12 col-md-6">
                            <label>Description: </label>
                            <textarea
                                type="text"
                                className="form-control"
                                name="ShortDescription"
                                placeholder="Describe your idea"
                                onChange={(e) => this.state.idea.ShortDescription = e.target.value}
                            />
                        </div>
                        <div className="col-12 col-md-6">
                            <label>Select a category:</label>
                            <Multiselect
                                options={this.state.categories} // Options to display in the dropdown
                                selectedValues={this.state.selectedCategories} // Preselected value to persist in dropdown
                                onSelect={this.CateroryOnSelect} // Function will trigger on select event
                                onRemove={this.CatergoryOnRemove} // Function will trigger on remove event
                                displayValue="name" // Property name to display in the dropdown options

                                ref={this.MultiselectRef}
                            />
                        </div>

                        <div className="col-12 col-md-6 mt-2">
                            <form>
                                <div className="custom-control custom-checkbox mb-3">
                                    <input onChange={(e) => {
                                        this.state.idea.IsAnonymous = e.target.checked
                                        var txt;
                                        if (!this.state.idea.IsAnonymous) {
                                            txt = 'I want my idea to be anonymous'
                                        } else {
                                            txt = 'Your idea will be anonymous'
                                        }
                                        this.setState({ anonTxt: txt });
                                    }}
                                        type="checkbox" className="custom-control-input"
                                        id="anon" required />
                                    <label className="custom-control-label" htmlFor="anon"  >{this.state.anonTxt}</label>
                                </div>
                            </form>
                            <form className="was-validated">
                                <div className="custom-control custom-checkbox mb-3"  >
                                    <input onChange={(e) => this.state.termsAndConditions = e.target.checked} type="checkbox" class="custom-control-input" id="termsAndCons" required />
                                    <label className="custom-control-label" htmlFor="termsAndCons" > I agree to the Gre-Ideas terms and conditions</label>
                                    <div className="invalid-feedback">You must agree to terms and conditions</div>
                                </div>
                            </form>
                        </div>
                        {/***** Error display container ****/}
                        <div className="container-cell-right">
                            {// If no error message is to be show do not render warning icon
                                this.state.errors.length != 0 &&
                                this.state.errors.map(e => {
                                    return <i key={e.Key} className="icofont-warning">{e.Value}</i>;
                                })
                            }
                        </div>
                    </Row>
                    <Button className="btn login-btn col-6 mb-3" onClick={this.submitIdea}>Post Idea</Button>
                </div>
            </Container>
        );
    }
}
/// Mapping the redux state with component's properties
const mapStateToProps = (state) => {
    return {
        Authentication: state.Authentication
    }
};
/// Map actions (which may include dispatch to redux store) to component
const mapDispatchToProps = {
    getAllCategories,
    postNewIdea,
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(NewIdea);
