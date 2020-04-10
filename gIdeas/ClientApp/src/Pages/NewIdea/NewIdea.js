import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';
import { getAllCategories } from '../../Actions/CategoryActions';
import { gIdea, gUser, gDocument } from '../../components/_MainApp/Models';
import { Base64ToArrayBuffer, StringBase64ToBlob } from '../../components/_MainApp/appConst';
import JSZip from 'jszip';
import RecordPageView from "../../components/RecordPageView";
import { postNewIdea } from "../../Actions/IdeaActions";

class NewIdea extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            categories: [],
            idea: new gIdea()
        }
    }
    async ComponentDidMount() {
        this.setState({
            categories: await getAllCategories()
        });
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
            this.state.idea.FileBlobStringBase64 = reader.result.replace('data:application/octet-stream;base64,','');
        };
        console.log(this.state.idea);
        var returnedDAta = await this.props.postNewIdea(this.state.idea);
        //var returnedDAta = await this.props.postFileTest(new Blob([zipFile]), new gIdea());

        const blob = StringBase64ToBlob(this.state.idea.FileBlobStringBase64)

        const h = URL.createObjectURL(blob);
        this.setState({
            btn: <a href={h} className="btn custom-btn" download="Documents.zip">Documents.zip</a>
        });
    }
    render() {
        return (
            <Container>
                {/* Record Page view of current page */}
                <RecordPageView IdeaId="0" />
                <div className="page-header">New Idea</div>
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
