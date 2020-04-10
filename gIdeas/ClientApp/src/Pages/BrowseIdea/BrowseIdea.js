import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, ButtonDropdown, Row, DropdownToggle, DropdownMenu, DropdownItem, Button } from 'reactstrap';

import "./BrowseIdea.css";
import { AllRecords, IdeaStatus, AllRoles } from '../../components/_MainApp/appConst';
import { getFilterdAndSortedIdeas } from '../../Actions/IdeaActions';
import { getAllDepartments } from '../../Actions/DepartmentActions';
import { getAllCategories } from '../../Actions/CategoryActions';
import { SelectThisIdea } from '../../Actions/ClientSideActions/SelectedIdeaAction';
import { Redirect } from 'react-router-dom';
import RecordPageView from "../../components/RecordPageView";

class BrowseIdea extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            IdeaList: [],
            submissionYear: new Date().getUTCFullYear(),
            categoryTagId: AllRecords,
            departmentId: AllRecords,
            roleId: AllRecords,
            ideaStatus: AllRecords,
            isAuthorAnon: AllRecords,
            departments: [],
            categories: [],
            date: [],
            filterYearSelected: "",
            filterCategorySelected: "",
            filterDepartmentSelected: "",
            filterRoleSelected: "",
            filterStatusSelected: "",
            filterIsAuthorAnonSelected: "",
            RedirectToSingleIdea: false,
        }

        this.getNewIdeasChangedYear = this.getNewIdeasChangedYear.bind(this);
        this.getNewIdeasCategory = this.getNewIdeasCategory.bind(this);
        this.getNewIdeasChangedAnonAuthor = this.getNewIdeasChangedAnonAuthor.bind(this);
        this.getNewIdeasDepartment = this.getNewIdeasDepartment.bind(this);
        this.convertDateToString = this.convertDateToString.bind(this);
    }
    async componentDidMount() {
        console.clear();

        for (var i = 4; i >= 0; i--) {
            this.state.date.push(new Date().getUTCFullYear() - i);
        }

        var resultCategories = await this.props.getAllCategories();
        try {
            this.state.categories = resultCategories.categories;
        } catch (e) { }

        var resultDepartment = await this.props.getAllDepartments();
        try {
            this.state.departments = resultDepartment.Departments;
        } catch (e) { }

        var resultIdeaList = await this.props.getFilterdAndSortedIdeas();
        try {
            this.setState({ IdeaList: resultIdeaList.Ideas });
        } catch (e) {
            console.log(e);
        }
    }

    async getNewIdeasChangedYear(Year) {
        this.state.submissionYear = Year;
        const { categoryTagId, departmentId, roleId, ideaStatus, isAuthorAnon } = this.state;
        var resultIdeaList = await this.props.getFilterdAndSortedIdeas(Year, categoryTagId, departmentId, roleId, ideaStatus, isAuthorAnon);
        try {
            this.setState({ IdeaList: resultIdeaList.Ideas, filterYearSelected: Year });
        } catch (e) {
            console.log(e);
        }
    }
    async getNewIdeasCategory(CategoryId, CategoryName) {
        if (AllRecords === CategoryId) {
            this.setState({ filterCategorySelected: "Show All" })
            return;
        }

        this.state.categoryTagId = CategoryId;
        const { submissionYear, departmentId, roleId, ideaStatus, isAuthorAnon } = this.state;
        var resultIdeaList = await this.props.getFilterdAndSortedIdeas(submissionYear, CategoryId, departmentId, roleId, ideaStatus, isAuthorAnon);
        try {
            this.setState({ IdeaList: resultIdeaList.Ideas, filterCategorySelected: CategoryName });
        } catch (e) {
            console.log(e);
        }
    }
    async getNewIdeasChangedAnonAuthor(AuthorAnon) {
        if (AllRecords === AuthorAnon) {
            this.setState({ filterIsAuthorAnonSelected: "Show All" })
            return;
        }

        this.state.isAuthorAnon = AuthorAnon;
        const { submissionYear, categoryTagId, departmentId, roleId, ideaStatus } = this.state;
        var resultIdeaList = await this.props.getFilterdAndSortedIdeas(submissionYear, categoryTagId, departmentId, roleId, ideaStatus, AuthorAnon);
        try {
            this.setState({ IdeaList: resultIdeaList.Ideas, filterIsAuthorAnonSelected: AuthorAnon });
        } catch (e) {
            console.log(e);
        }
    }
    async getNewIdeasChangedStatus(status) {
        if (AllRecords === status) {
            this.setState({ filterStatusSelected: "Show All" })
            return;
        }

        this.state.ideaStatus = status;
        const { submissionYear, categoryTagId, departmentId, roleId, isAuthorAnon } = this.state;
        var resultIdeaList = await this.props.getFilterdAndSortedIdeas(submissionYear, categoryTagId, departmentId, roleId, status, isAuthorAnon);
        try {
            this.setState({ IdeaList: resultIdeaList.Ideas, filterStatusSelected: status });
        } catch (e) {
            console.log(e);
        }
    }
    async getNewIdeasDepartment(DepartmentId, DepartmentName) {
        if (AllRecords === DepartmentId) {
            this.setState({ filterDepartmentSelected: "Show All" })
            return;
        }

        this.state.departmentId = DepartmentId;
        const { submissionYear, categoryTagId, roleId, ideaStatus, isAuthorAnon } = this.state;
        var resultIdeaList = await this.props.getFilterdAndSortedIdeas(submissionYear, categoryTagId, DepartmentId, roleId, ideaStatus, isAuthorAnon);
        try {
            this.setState({ IdeaList: resultIdeaList.Ideas, filterDepartmentSelected: DepartmentName });
        } catch (e) {
            console.log(e);
        }
    }
    async getNewIdeasChangedRoles(Roleid, RoleName) {
        this.state.roleId = Roleid;
        const { submissionYear, categoryTagId, departmentId, ideaStatus, isAuthorAnon } = this.state;
        var resultIdeaList = await this.props.getFilterdAndSortedIdeas(submissionYear, categoryTagId, departmentId, Roleid, ideaStatus, isAuthorAnon);
        try {
            this.setState({ IdeaList: resultIdeaList.Ideas, filterRoleSelected: RoleName });
        } catch (e) {
            console.log(e);
        }
    }
    async resetFilter() {
        this.state.submissionYear = new Date().getFullYear();
        this.state.filterYearSelected = new Date().getFullYear();
        this.state.roleId = AllRecords;
        this.state.filterRoleSelected = "Role";
        this.state.departmentId = AllRecords;
        this.state.filterDepartmentSelected = "Department";
        this.state.ideaStatus = AllRecords;
        this.state.filterStatusSelected = "Status";
        this.state.categoryTagId = AllRecords;
        this.state.filterCategorySelected = "Category Tag";
        this.state.isAuthorAnon = AllRecords;
        this.state.filterIsAuthorAnonSelected = "Show All";
        const { submissionYear, categoryTagId, departmentId, roleId, ideaStatus, isAuthorAnon } = this.state;
        var resultIdeaList = await this.props.getFilterdAndSortedIdeas(submissionYear, categoryTagId, departmentId, roleId, ideaStatus, isAuthorAnon);
        try {
            this.setState({ IdeaList: resultIdeaList.Ideas });
        } catch (e) {
            console.log(e);
        }
    }
    SendToSingleIdea(IdeaId) {
        this.props.SelectThisIdea(IdeaId);
        this.setState({ RedirectToSingleIdea: true })

    }
    convertDateToString(date) {
        return date.toLocaleDateString();
    }
    render() {
        if (this.state.RedirectToSingleIdea)
            return (<Redirect to="/ViewIdea" />)

        return (
            <Container className="custom-container">
                <div className="img-header mb-3">
                    <div className="page-header">Browse Idea</div>
                </div>
                {/* Record Page view of current page */}
                <RecordPageView IdeaId="0" />

                <Row>
                    <div className="col-12 col-md-3 side-bar  pt-md-5 mb-md-5 pb-md-5">
                        {/* Year */}
                        <ButtonDropdown className="side-bar-dropdown  col-12" isOpen={this.state.dropdownOpen} toggle={() => this.setState({ dropdownOpen: !this.state.dropdownOpen })}>
                            <DropdownToggle caret className="hide-overflow">
                                {this.state.submissionYear}
                            </DropdownToggle>
                            <DropdownMenu>
                                {this.state.date.length > 0 && this.state.date.map(year => {
                                    return (< DropdownItem onClick={() => { this.getNewIdeasChangedYear(year) }}>{year}</DropdownItem>)
                                })}
                            </DropdownMenu>
                        </ButtonDropdown>
                        {/* Category Tag */}
                        <ButtonDropdown className="side-bar-dropdown col-12" isOpen={this.state.dropdownOpen1} toggle={() => this.setState({ dropdownOpen1: !this.state.dropdownOpen1 })}>
                            <DropdownToggle caret className="hide-overflow">
                                {this.state.filterCategorySelected === "" && "Cat / Tags"}
                                {this.state.filterCategorySelected != "" && this.state.filterCategorySelected}
                            </DropdownToggle>
                            <DropdownMenu>
                                {this.state.categories.length > 0 &&
                                    this.state.categories.map(i => {
                                        return <DropdownItem onClick={() => { this.getNewIdeasCategory(i.Id, i.Name) }}>{i.Name}</DropdownItem>
                                    })}
                                <DropdownItem onClick={() => { this.getNewIdeasCategory(AllRecords) }} >Show All</DropdownItem>
                            </DropdownMenu>
                        </ButtonDropdown>
                        {/* Departments */}
                        <ButtonDropdown className="side-bar-dropdown col-12" isOpen={this.state.dropdownOpen2} toggle={() => this.setState({ dropdownOpen2: !this.state.dropdownOpen2 })}>
                            <DropdownToggle caret className="hide-overflow">
                                {this.state.filterDepartmentSelected === "" && "Departments"}
                                {this.state.filterDepartmentSelected != "" && this.state.filterDepartmentSelected}
                            </DropdownToggle>
                            <DropdownMenu>
                                {this.state.departments.length > 0 &&
                                    this.state.departments.map(i => {
                                        return <DropdownItem onClick={() => { this.getNewIdeasDepartment(i.id, i.name) }}>{i.name}</DropdownItem>
                                    })}
                                <DropdownItem onClick={() => { this.getNewIdeasDepartment(AllRecords) }} >Show All</DropdownItem>
                            </DropdownMenu>
                        </ButtonDropdown>
                        {/* Roles */}
                        <ButtonDropdown className="side-bar-dropdown col-12" isOpen={this.state.dropdownOpen3} toggle={() => this.setState({ dropdownOpen3: !this.state.dropdownOpen3 })}>
                            <DropdownToggle caret className="hide-overflow">
                                {this.state.filterRoleSelected === "" && "Roles"}
                                {this.state.filterRoleSelected != "" && this.state.filterRoleSelected}
                            </DropdownToggle>
                            <DropdownMenu>
                                {AllRoles.map(i => {
                                    return (<DropdownItem onClick={() => { this.getNewIdeasChangedRoles(i.Id, i.Name) }} >{i.Name}</DropdownItem>);
                                })}
                            </DropdownMenu>
                        </ButtonDropdown>
                        {/* Status */}
                        <ButtonDropdown className="side-bar-dropdown col-12" isOpen={this.state.dropdownOpen4} toggle={() => this.setState({ dropdownOpen4: !this.state.dropdownOpen4 })}>
                            <DropdownToggle caret className="col-12 hide-overflow">
                                {this.state.filterStatusSelected === "" && "Status"}
                                {this.state.filterStatusSelected != "" && this.state.filterStatusSelected}
                            </DropdownToggle>
                            <DropdownMenu className="col-12">
                                {IdeaStatus.All.map(i => {
                                    return (<DropdownItem onClick={() => { this.getNewIdeasChangedStatus(i) }} >{i}</DropdownItem>);
                                })}
                                <DropdownItem onClick={() => { this.getNewIdeasChangedStatus(AllRecords) }} >Show All Authors</DropdownItem>
                            </DropdownMenu>
                        </ButtonDropdown>
                        {/* Anon Authors */}
                        <ButtonDropdown className="side-bar-dropdown col-12" isOpen={this.state.dropdownOpen5} toggle={() => this.setState({ dropdownOpen5: !this.state.dropdownOpen5 })}>
                            <DropdownToggle caret className="hide-overflow">
                                {this.state.filterIsAuthorAnonSelected === "" && "Anon Authors"}
                                {this.state.filterIsAuthorAnonSelected != "" && `Anon Authors (${this.state.filterIsAuthorAnonSelected})`}
                            </DropdownToggle>
                            <DropdownMenu className="col-12">
                                <DropdownItem onClick={() => { this.getNewIdeasChangedAnonAuthor("True") }} >Show Author</DropdownItem>
                                <DropdownItem onClick={() => { this.getNewIdeasChangedAnonAuthor("False") }} >Hide Author</DropdownItem>
                                <DropdownItem onClick={() => { this.getNewIdeasChangedAnonAuthor(AllRecords) }} >Show All Authors</DropdownItem>
                            </DropdownMenu>
                        </ButtonDropdown>
                        <Button className="side-bar-dropdown col-12 mt-4" onClick={() => { this.resetFilter() }} >
                            Reset Filter
                        </Button>
                    </div>

                    {/* Idea Container For Real Data */}
                    <div className="col-12 col-lg-9 m-0 pt-md-5 mb-md-5 pb-md-5">
                        {this.state.IdeaList.length > 0 &&
                            this.state.IdeaList.map(i => {
                                return (
                                    <div className="col-12 mb-5 pb-4 pl-2 pr-2 mainCard m-shadow-box">
                                        <div className="row">
                                            <div className="col-12 col-sm-6">{`Author: ${i.Author.FirstName} ${i.Author.Surname} `}</div>
                                            <div className="col-12 col-sm-6">Created Date: {i.CreatedDate}</div>
                                            <div className="col-6 m-0 p-0">
                                                <div className="col-12 col-sm-12"><font>{`Department: ${i.Author.Department.Name}`}</font></div>
                                                <div className="col-12 col-sm-12"><font>{`Role: ${i.Author.Role.Name}`}</font></div>
                                            </div>
                                            <div className="col-6">Title: {i.Title}</div>
                                        </div>
                                        <button className="btn login-button col-4" onClick={() => { this.SendToSingleIdea(i.Id) }} >View Details</button>
                                    </div>
                                )
                            })
                        }

                        {/* Idea Container For Testing Data */}
                        {this.state.IdeaList.length == 0 &&
                            <div className="col-12 mt-md-5 pt-md-5 mb-md-5 pb-md-5">
                                <div className="col-12 mainCard m-shadow-box">
                                    No Idea...
                                </div>
                            </div>
                        }
                    </div>
                </Row>
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
    getFilterdAndSortedIdeas,
    getAllDepartments,
    getAllCategories,
    SelectThisIdea
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(BrowseIdea);