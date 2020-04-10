import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row } from 'reactstrap';
import { Link } from 'react-router-dom';
import './Home.css'
import RecordPageView from "../../components/RecordPageView";

class Home extends PureComponent {
    constructor(props) {
        super(props)
        this.state = {
            lastLogin: ""
        }
    }
    componentDidMount() {
        try {
            this.setState({ lastLogin: this.props.Authentication.user.LastLoginDate.toLocaleDateString() })
        } catch (e) {
        }
    }
    render() {
        return (
            <Container className="custom-container">
                {/* Record Page view of current page */}
                <RecordPageView IdeaId="0" />
                <div className="img-header mb-3">
                    <div className="page-header">Home</div>
                </div>
                <Row>
                    <div className="col-12 col-md-4 p-3">
                        {this.props.Authentication.isAuthenticated &&
                            <div className="text-center">
                                <h3 className="my-profile border-bottom">My Profile</h3>
                                <h5 className="h3 mt-4"><div className="text-underline">User </div> {this.props.Authentication.user.FirstName} {this.props.Authentication.user.Surname}</h5>
                                <h5 className="h3"><div className="text-underline">Role</div> {this.props.Authentication.user.Role.Name}</h5>
                                <h5 className="h3"><div className="text-underline">Department</div> {this.props.Authentication.user.Department.Name}</h5>
                                <h5 className="h3"><div className="text-underline">Last Login</div><div>{this.state.lastLogin}</div></h5>
                                <Link className="btn col-12 login-button" to="/NewIdea">
                                    Create Idea
                                </Link>
                            </div>
                        }
                        {!this.props.Authentication.isAuthenticated &&
                            <div className="text-center">
                                <h3 className="my-profile border-bottom">Welcome to Greenwich Idea Website</h3>
                                <Link className="btn col-12 mt-4 login-button mb-4" to="/Login">Login</Link>
                            </div>
                        }
                    </div>
                    <div className="col-12 col-md-8">
                        <Row>
                            <div className="col-6 pt-2 pb-2 text-center">
                                <Link to="/NewIdea">
                                    <img className="col-12" src="/Images/Faculty of Education, H and HS.png" alt="Directory to browse ideas" />
                                    <div className="col-12 h3">New Idea</div>
                                </Link>
                            </div>
                            <div className="col-6 pt-2 pb-2 text-center">
                                <Link to="/BrowseIdea">
                                    <img className="col-12" src="/Images/Liberal Arts and Sciences.png" alt="Directory to new ideas" />
                                    <div className="col-12 h3">Browse Idea</div>
                                </Link>
                            </div>
                            <div className="col-6 pt-2 pb-2 text-center">
                                <Link to="/TeamMembers">
                                    <img className="col-12" src="/Images/Faculty of Business Schooling.png" alt="Directory to team members" />
                                    <div className="col-12 h3">Team Members</div>
                                </Link>
                            </div>
                            <div className="col-6 pt-2 pb-2 text-center">
                                <a href="https://github.com/p8b/G-IdeaWeb">
                                    <img className="col-12" src="/Images/Faculty of Engineering and Science.png" alt="Directory to team members" />
                                    <div className="col-12 h3">Github Project</div>
                                </a>
                            </div>
                        </Row>
                    </div>
                </Row>
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
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(Home);