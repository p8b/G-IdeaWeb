import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';
import { getAllCategories } from '../../Actions/CategoryActions';
import { gIdea, gUser, gDocument } from '../../components/_MainApp/Models';
import { IdeaStatus } from '../../components/_MainApp/appConst';
import JSZip from 'jszip';

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
            this.state.idea.FileBlobStringBase64 = reader.result.replace('data:application/octet-stream;base64,','');
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
        return (
            <Container>
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

}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(NewIdea);
