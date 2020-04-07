import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';
import { getAllCategories } from '../../Actions/CategoryActions';
import { gIdea, gUser, gDocument } from '../../components/_MainApp/Models';
import { IdeaStatus } from '../../components/_MainApp/appConst';
import JSZip from 'jszip';

import { Button, Row, Label, FormGroup, Input } from 'reactstrap';
import { Multiselect } from 'multiselect-react-dropdown';
import './NewIdea.css';



class NewIdea extends PureComponent {
    constructor(props) {
        super(props);

        // multi selector drop down menu constructor
        this.MultiselectRef = React.createRef();

        this.onSubmit = this.onSubmit.bind(this);
        this.onChangegCategoriesToIdeas = this.onChangegCategoriesToIdeas.bind(this);
        //this.onChangeClosedDate = this.onChangeClosedDate.bind(this);
        this.onChangeCreatedDate = this.onChangeCreatedDate.bind(this);
        this.onChangeDisplayAnonymous = this.onChangeDisplayAnonymous.bind(this);
        this.onChangegDocuments = this.onChangegDocuments.bind(this);
        this.onChangeShortDescription = this.onChangeShortDescription.bind(this);
        this.onChangeStatus = this.onChangeStatus.bind(this);
        this.onChangeTitle = this.onChangeTitle.bind(this);
        this.onChangeUser = this.onChangeUser.bind(this);
        this.onChangeFinalClosureDate = this.onChangeFinalClosureDate.bind(this);

        //constructor methods for upload button
        this.handleSubmit = this.handleSubmit.bind(this);
        this.fileInput = React.createRef();


        this.state = {
            categories: [],
            idea: new gIdea(),

            User: '',
            Status: '',
            gCategoriesToIdeas: '',
            Title: null,
            ShortDescription: null,
            CreatedDate: new Date().toLocaleString(),
            ClosedDate: null,
            DisplayAnonymous: false,
            gDocuments: null,

            options: [
                { name: 'Technology', id: 1 },
                { name: 'Environment', id: 2 },
                { name: 'Coronavirus', id: 3 },
                { name: 'Facilities', id: 4 },
                { name: 'Communications', id: 5 },
                { name: 'Brexit', id: 6 },
                { name: 'Parking', id: 7 },
                { name: 'Staff Benefits', id: 8 },
                { name: 'Career Progression', id: 9 },
                { name: 'Timetabling', id: 10 },
                { name: 'Equality and Diversity', id: 11 },
                { name: 'Pay and Reward', id: 12 },
                { name: 'Transport', id: 13 },
                { name: 'PhD Support', id: 14 },
                { name: 'Learning and Development', id: 15 },
                { name: 'Appraisals', id: 16 },
                { name: 'Wellbeing and Health', id: 17 },

            ],
            //validation states
            fields: {},
            errors: {}

        }
        // validation constructor methods
        this.handleChange = this.handleChange.bind(this);
        this.submitIdeaForm = this.submitIdeaForm.bind(this);

        //handle method for anonymous checkbox
        this.handleChecked = this.handleChecked.bind(this);
    }

    async ComponentDidMount() {
        this.setState({
            categories: await getAllCategories()
        });
    }

    // handle method for anonymous checkbox
    handleChecked() {
        this.setState({ DisplayAnonymous: !this.state.DisplayAnonymous });
    }

    // validation methods 
    handleChange(e) {
        let fields = this.state.fields;
        fields[e.target.name] = e.target.value;
        this.setState({
            fields
        });
    }


    //form submit validation method
    submitIdeaForm(e) {
        e.preventDefault();
        if (this.validateIdeaForm()) {
            let fields = {};
            fields["gCategoriesToIdeas"] = "";
            fields["Title"] = "";
            fields["ShortDescription"] = "";

            this.setState({ fields: fields });
            console.log("Category, title and desc are valid");
        }
    }

    // CAT, TITLE AND DESC VALIDATION 
    validateIdeaForm() {
        let fields = this.state.fields;
        let errors = {};
        let formIsValid = true;

        if (!fields["gCategoriesToIdeas"]) {
            formIsValid = false;
            errors["gCategoriesToIdeas"] = "Please choose a category";
        }

        if (!fields["Title"]) {
            formIsValid = false;
            errors["Title"] = "Please write a title";
        }

        if (!fields["ShortDescription"]) {
            formIsValid = false;
            errors["ShortDescription"] = "Please describe your idea";
        }
        // set state for validation
        this.setState({
            errors: errors
        });
        return formIsValid;
    }

    // On Change methods to set state to components
    onChangeUser(e) {
        this.setstate({
            User: e.target.value
        });
    }

    onChangeStatus(e) {
        this.setstate({
            Status: e.target.value
        });
    }

    onChangegCategoriesToIdeas(e) {
        this.setState({
            gCategoriesToIdeas: e.target.value
        });
    }

    onChangeTitle(e) {
        this.setState({
            Title: e.target.value
        });
    }

    onChangeShortDescription(e) {
        this.setState({
            ShortDescription: e.target.value
        });
    }

    onChangeCreatedDate(e) {
        this.setState({
            CreatedDate: e.target.value
        });
    }

    onChangeDisplayAnonymous(e) {
        this.setState({
            DisplayAnonymous: e.target.value
        });
    }

    onChangegDocuments(e) {
        this.setState({
            gDocuments: e.target.value
        });
    }

    onChangeFinalClosureDate(e) {
        this.setState({
            FinalClosureDate: e.target.value
        });
    }

    // methods that belong to the multiple selection drop down box

    resetValues() {
        this.multiselectRef.current.resetSelectedValues();
    }

    SelectedItems() {
        this.MultiselectRef.current.getSelectedItems();
    }

    SelectedItemsCount() {
        this.MultiselectRef.current.getSelectedItemsCount();
    }

    // upload file button method
    handleSubmit(event) {
        event.preventDefault();
        alert(
            `Selected file - ${this.fileInput.current.files[0].name}`
        );
    }

    //testing upload button via console log
    buttonTest = () => {
        console.log("upload works")
    }

    // Submit form methods
    onSubmit(e) {
        e.preventDefault();

        const { history } = this.props;

        let ideaObject = {
            Id: Math.floor(Math.random() * 1000), // to assign a random id number to new ideas
            Title: this.state.Title,
            User: this.state.User,
            Status: this.state.Status,
            gCategoriesToIdeas: this.state.gCategoriesToIdeas,
            ShortDescription: this.state.ShortDescription,
            CreatedDate: this.state.CreatedDate,
            ClosedDate: this.state.ClosedDate,
            DisplayAnonymous: this.state.DisplayAnonymous,
            gDocuments: this.state.gDocuments,
            FinalClosureDate: this.state.FinalClosureDate,
        }

        //here allow a method to handle the API request by pushing history?
    }

    async uploadDocument(files) {

        for (let i = 0; i < files.length; i++) {
            let reader1 = new FileReader();
            reader1.readAsDataURL(new Blob([files[i].arrayBuffer], { type: "arraybuffer" }));
            reader1.onload = () => {
                this.state.idea.Documents.push(new gDocument({ name: files[i].name, blobStringBase64: reader1.result.replace('data:arraybuffer;base64,', '') }));
            };
        }

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

        //var returnedDAta = await this.props.postFileTest(new Blob([zipFile]), new gIdea());
        //var returnedDAta = await this.props.postFileTest(this.state.idea);

        //var byteCharacters = window.atob(returnedDAta.FileBlobStringBase64);
        //const byteNumbers  = new Array(byteCharacters.length);
        //for (let i = 0; i < byteCharacters.length; i++) {
        //    byteNumbers [i] = byteCharacters.charCodeAt(i);
        //}
        //const byteArray = new Uint8Array(byteNumbers);
        //const blob = new Blob([byteArray]);

        //const h = URL.createObjectURL(blob);
        //this.setState({
        //    btn: <a href={h} className="btn custom-btn" download="Documents.zip">Documents.zip</a>
        //});
    }
    render() {

        const { newIdea } = this.state

        //rendering anonymous checkbox state and handleChecked method
        var txt;
        if (!this.state.DisplayAnonymous) {
            txt = 'I want my idea to be anonymous'
        } else {
            txt = 'Your idea will be anonymous'
        }

        return (

            // form container
            <div className="container-fluid">
                <div className="ideaForm" onSubmit={this.submitIdeaForm}>

                    <div className="img-header">
                        <div className="page-header">Share your ideas</div>
                    </div>
                    <br />
                    <form onSubmit={this.onSubmit}>
                        <Row>
                            <div className="col-md-4">Department:</div>
                            <div className="col-md-4 offset-md-4">
                                <label>Posted on: {this.state.CreatedDate}</label>
                            </div>
                        </Row>
                        <Row>
                            <div className="col-md-4">
                                <label>Posted by: {this.state.User.name}
                                </label>
                            </div>

                            <div className=" col-md-4 offset-md-4">
                                <label>Expires on: {this.state.ClosedDate} </label>

                            </div>
                        </Row>

                        <div className="cat-Container">
                            <label>Select a category:</label>
                            <Multiselect
                                options={this.state.options} // Options to display in the dropdown
                                selectedValues={this.state.selectedValue} // Preselected value to persist in dropdown
                                onSelect={this.onSelect} // Function will trigger on select event
                                onRemove={this.onRemove} // Function will trigger on remove event
                                displayValue="name" // Property name to display in the dropdown options

                                ref={this.MultiselectRef}
                            />
                        </div>

                        <div className="form-group">
                            <label>Title of idea:</label>
                            <input
                                type="text"
                                className="form-control"
                                name="Title"
                                placeholder="Title of your idea"
                                value={this.state.fields.Title}
                                // onChange={this.onChangeTitle}
                                onChange={this.handleChange}
                            />
                            <div className="errorMsg">{this.state.errors.Title}</div>
                        </div>

                        <div className="form-group">
                            <label>Description: </label>
                            <textarea
                                type="text"
                                className="form-control"
                                name="ShortDescription"
                                placeholder="Describe your idea"
                                value={this.state.fields.ShortDescription}
                                // onChange={this.onChangeShortDescription}
                                onChange={this.handleChange}
                            />
                            <div className="errorMsg">{this.state.errors.ShortDescription}</div>
                        </div>

                        <div className="container-fluid">
                            <form>
                                {/* Checkbox changes label on click public / anonymous */}
                                <div>
                                    <input type="checkbox" title="your identity remains visible to managers"
                                        onChange={this.handleChecked} />
                                    <label>{txt}</label>
                                </div>

                                <form class="was-validated">
                                    <div class="custom-control custom-checkbox mb-3">
                                        <input type="checkbox" class="custom-control-input" id="termsAndCons" required />
                                        <label class="custom-control-label" for="termsAndCons"> I agree to the Gre-Ideas terms and conditions</label>
                                        <div class="invalid-feedback">You must agree to terms and conditions</div>
                                    </div>
                                </form>

                                <Container>
                                    <div className="form-row">
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
                                </Container>
                                <Row>
                                    <br />
                                    <Button type="submit" variant="dark" className="grebutton" aria-label="Post">Post Idea</Button>

                                    <Button className="btn-changed" aria-label="Close">Cancel</Button>
                                </Row>
                            </form>
                        </div>
                    </form>
                </div>

            </div>
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

}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(NewIdea);
