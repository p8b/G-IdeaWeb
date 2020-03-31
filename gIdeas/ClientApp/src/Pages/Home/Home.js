import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';
import JSZip from 'jszip';

class Home extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            btn :null,
        }
        this.uploadDocument = this.uploadDocument.bind(this);
    }
    async uploadDocument(files) {
        var zip = new JSZip();
        var zipDocuments = zip.folder("Documents");
        for (var i = 0; i < files.length; i++) {
            zipDocuments.file(files[i].name, files[i].arrayBuffer);
        }

        let zipFile = await zip.generateAsync({type: "arraybuffer"});
        const blob = new Blob([zipFile]);
        const h = URL.createObjectURL(blob);
        this.setState({
            btn: <a href={h} className="btn custom-btn" download="Documents.zip"> Download</a>
        });
    }
    render() {
        return (
            <Container >
                <div className="page-header">Home</div>
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
    }
};
/// Map actions (which may include dispatch to redux store) to component
const mapDispatchToProps = {
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(Home);