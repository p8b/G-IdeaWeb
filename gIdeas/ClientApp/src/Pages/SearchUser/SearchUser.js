import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, ButtonDropdown, Row, DropdownToggle, DropdownMenu, DropdownItem, Button, Table } from 'reactstrap';
import { AllRecords, AllRoles } from '../../components/_MainApp/appConst';
import { getAllDepartments } from '../../Actions/DepartmentActions';
import { getSearchUser, putBlockOrUnblockUser } from '../../Actions/UserActions';
import RecordPageView from "../../components/RecordPageView";

class SearchUser extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            departments: [],
            userList: [],
            searchValue: AllRecords,
            roleId: "",
            filterRoleSelected: "",
            departmentId: "",
            filterDepartmentSelected: "",
            loading: false
        }
        this.getNewIdeasDepartment = this.getNewIdeasDepartment.bind(this);
        this.getNewIdeasChangedRoles = this.getNewIdeasChangedRoles.bind(this);
        this.searchUser = this.searchUser.bind(this);
        this.resetFilter = this.resetFilter.bind(this);
        this.showAllUsers = this.showAllUsers.bind(this);
        this.blockORunblockUser = this.blockORunblockUser.bind(this);
        this.blockORunblockUser = this.blockORunblockUser.bind(this);
    }
    async componentDidMount() {
        var resultDepartment = await this.props.getAllDepartments();
        try {
            this.setState({ departments: resultDepartment.Departments });
        } catch (e) { }
    }
    async getNewIdeasDepartment(DepartmentId, DepartmentName) {
        if (AllRecords === DepartmentId) {
            this.setState({ filterDepartmentSelected: "Show All" })
            return;
        }

        this.state.departmentId = DepartmentId;
        this.state.filterDepartmentSelected = DepartmentName;
        this.searchUser();
    }
    async getNewIdeasChangedRoles(Roleid, RoleName) {
        if (AllRecords === Roleid) {
            this.setState({ filterRoleSelected: "Show All" })
            return;
        }
        this.state.roleId = Roleid;
        this.state.filterRoleSelected = RoleName;
        this.searchUser();
    }
    async showAllUsers() {
        this.state.searchValue = "";
        this.searchUser();
    }
    async resetFilter() {
        this.state.roleId = AllRecords;
        this.state.filterRoleSelected = "Role";
        this.state.departmentId = AllRecords;
        this.state.filterDepartmentSelected = "Department";
        this.searchUser();
    }
    async searchUser() {
        const { roleId, departmentId, searchValue } = this.state;
        var searchResult = await this.props.getSearchUser(roleId === "" ? AllRecords : roleId,
            departmentId === "" ? AllRecords : departmentId,
            searchValue);
        try {
            console.log(searchResult.userList)
            this.setState({ userList: searchResult.userList });
        } catch (e) {
            console.log(e);
        }
    }
    async blockORunblockUser(userId, status) {
        const result = await this.props.putBlockOrUnblockUser(userId, status);
        this.searchUser();
    }
    render() {
        return (
            <Container className="custom-container">
                {/* Record Page view of current page */}
                <RecordPageView IdeaId="0" />

                <div className="img-header mb-3">
                    <div className="page-header">User Management</div>
                </div>
                <Row>
                    <div className="col-12 col-md-3 side-bar  pt-md-5 mb-md-5 pb-md-5">
                        <input onChange={(e) => { this.state.searchValue = e.target.value }}
                            placeholder="Search ⌕" className="form-control col-12" />
                        <Button className="side-bar-dropdown col-12 mb-1 " disabled={this.state.searching}
                            onClick={this.searchUser} >
                            {this.state.searching ? 'Searching...' : 'Search'}
                        </Button>
                        <Button className="side-bar-dropdown col-12 mb-3 "
                            onClick={this.showAllUsers} >
                            Show All Users
                        </Button>
                        <Button className="side-bar-dropdown col-12"
                            onClick={this.resetFilter} >
                            Reset Filter
                        </Button>
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
                    </div>

                    {/* User Table Container */}
                    <div className="col-12 col-lg-9 mt-3 mt-md-0">
                        {this.state.userList.length > 0 &&
                            <Table className="col-12 m-0 p-0 text-center" striped responsive>
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Surname</th>
                                        <th>Department</th>
                                        <th>Role</th>
                                        <th>No. Ideas</th>
                                        <th>No. Comments</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {this.state.userList.length > 0 &&
                                        this.state.userList.map((i) => {
                                            return (
                                                <tr key={i.id}>
                                                    <td>{i.firstName}</td>
                                                    <td>{i.surname}</td>
                                                    <td>{i.department.name}</td>
                                                    <td>{i.role.name}</td>
                                                    <td>{i.totalNumberOfIdeas}</td>
                                                    <td>{i.totalNumberOfComments}</td>
                                                    <td>{i.isBlocked == true ?
                                                        <td className="p-0">
                                                            <button className="btn btn-success m-0"
                                                                onClick={() => this.blockORunblockUser(i.id, false)}>
                                                                Unlock</button>
                                                        </td>
                                                        : 
                                                        <td className="p-0">
                                                            <button className="btn btn-danger m-0"
                                                                onClick={() => this.blockORunblockUser(i.id, true)}>
                                                                Lock</button>
                                                        </td>
                                                    }</td>
                                                    
                                                </tr>
                                            )
                                        })
                                    }
                                </tbody>
                            </Table>
                        }

                        {/* Idea Container For Testing Data */}
                        {this.state.userList.length == 0 &&
                            <Table className="col-12 m-0 p-0 text-center" striped responsive>
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Surname</th>
                                        <th>Department</th>
                                        <th>Role</th>
                                        <th>No. Ideas</th>
                                        <th>No. Comments</th>
                                        <th></th>
                                    </tr>
                                </thead>
                            </Table>
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
    getAllDepartments,
    getSearchUser,
    putBlockOrUnblockUser
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(SearchUser);
