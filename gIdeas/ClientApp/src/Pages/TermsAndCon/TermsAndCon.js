import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';

import "./TermsAndCon.css";


class TermsAndCon extends PureComponent {
    constructor(props) {
        super(props);
    }
    async ComponentDidMount() {

    }
    render() {
        return (   

            <div class="termscontainer container-fluid" >
                        <div className="img-header">
                            <div className="page-header">Terms and conditions</div>
                        </div> <br />

                <div>
                        <p>
                            Please read these terms of use carefully before you start to use a site. We recommend that you print a copy of this for future reference.
                            By using Gre Ideas, you confirm that you accept these terms of use and that you agree to comply with them.
                    If you do not agree to these terms of use, you must not use this site.</p>
                       <h4>Anonymous posts</h4>
                        <p>A record from authors who post ideas and comments anonymously will be monitored and used by managers to facilitate the
                    correct following of these guidelines.</p>
                        <h4>Your account and password</h4>
                        <p>If you choose, or you are provided with, a user identification code, password or any other piece of information as part of our security procedures, you must treat such information as confidential.You must not disclose it to any third party.
                </p>
                        <h4>Discussion rules</h4>
                        <p><ul>
                            <li>Do not submit comments that contain personal information.</li>
                            <li>Do not submit comments that are unlawful, harassing, abusive, threatening, harmful, obscene, profane, sexually orientated or racially offensive.</li>
                            <li> Do not swear or use language that could offend other forum participants.</li>
                            <li> Do not advertise or promote products or services.</li>
                            <li>Do not spam or flood the forum.</li>
                            <li>Keep your comments relevant to the discussion topic.</li>
                        </ul>
                        </p></div>
                </div>
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
)(TermsAndCon);
