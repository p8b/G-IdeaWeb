import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, ButtonDropdown, Row, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

import "./AddCategory.css";
import { gCategoryTag } from '../../components/_MainApp/Models';
import { AllRecords } from '../../components/_MainApp/appConst';
import { getAllCategories, postCategory, deleteCateory } from '../../Actions/CategoryActions';
import RecordPageView from "../../components/RecordPageView";

class AddCategory extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            categoryTagId: AllRecords,
            categories: [],
            NewCategory: new gCategoryTag(),
            errors: [],
            errors1: [],
            Categories: []
        }
        this.submitNewCategory = this.submitNewCategory.bind(this);
        this.deleteThis = this.deleteThis.bind(this);
    }
    async componentDidMount() {
        var resultCategories = await this.props.getAllCategories();
        try {
            this.setState({ categories: resultCategories.categories });
        } catch (e) { }

    }
    async submitNewCategory() {
        const result = await this.props.postCategory(this.state.NewCategory);
        this.state.errors = []
        this.state.errors1 = []

        if (result.errors.length == 0) {
            const resultCategories = await this.props.getAllCategories();
            try {
                this.state.errors.push({ key: 0, value: `${this.state.NewCategory.Name} was Added` })
                this.setState({ categories: resultCategories.categories, });
                this.setState({ errors: this.state.errors })
            } catch (e) { }
        }
        else
            this.setState({ errors: result.errors })
    }

    async deleteThis(category) {
        const result = await this.props.deleteCateory(category);
        this.state.errors1 = []
        this.state.errors = []
        if (result.errors.length == 0) {
            const resultCategories = await this.props.getAllCategories();
            try {
                this.state.errors1.push({ key: 0, value: `${category.Name} was Deleted` })
                this.setState({ categories: resultCategories.categories, });
                this.setState({ errors1: this.state.errors1 })
            } catch (e) { }
        }
        else
            this.setState({ errors1: result.errors })
    }

    render() {
        return (
            <Container className="custom-container">
                {/* Record Page view of current page */}
                <RecordPageView IdeaId="0" />
                
                <div className="img-header mb-3">
                    <div className="page-header">My Gre Ideas Categories</div>
                </div>
                <Row className="justify-content-sm-center">
                    <div className="col-12 col-md-6">
                        <div className="col-12 mb-4">
                            <div className="my-profile col-12">All Existing Categories</div>
                            <ButtonDropdown className="col-12 m-0 p-0" isOpen={this.state.dropdownOpen1} toggle={() => this.setState({ dropdownOpen1: !this.state.dropdownOpen1 })}>
                                <DropdownToggle caret>
                                    Categories
                                </DropdownToggle>
                                <DropdownMenu className="col-12">
                                    {this.state.categories.length > 0 &&
                                        this.state.categories.map(i => {
                                            return <DropdownItem key={i.Id} ><Row><div className="col-8">{i.Name}</div> <div className="btn btn-danger col-4 " onClick={() => { this.deleteThis(i) }}>Delete</div></Row></DropdownItem>
                                        })}
                                </DropdownMenu>
                            </ButtonDropdown>
                            {// If no error message is to be show do not render warning icon
                                this.state.errors1.length != 0 &&
                                this.state.errors1.map(e => {
                                    return (
                                        <div className="col-12 mt-2">
                                            <i key={e.key} className="icofont-warning">{e.value}</i>
                                        </div>
                                    );
                                })
                            }
                        </div>
                        <div className="col-12 mt-4 pt-5">
                            <div className="my-profile col-12 mt-4">Add New Category</div>

                            <input placeholder="Category Name" type='text' required={true} onChange={(e) => { this.state.NewCategory.Name = e.target.value }} className="form-control col-12 mt-2" />
                            {// If no error message is to be show do not render warning icon
                                this.state.errors.length != 0 &&
                                this.state.errors.map(e => {
                                    return (
                                        <div className="col-12 mt-2">
                                            <i key={e.key} className="icofont-warning">{e.value}</i>
                                        </div>
                                    );
                                })
                            }
                            <button className="btn login-button col-12 mt-2 mb-2" onClick={this.submitNewCategory}>
                                Submit
                        </button>

                        </div>
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
    deleteCateory,
    getAllCategories,
    postCategory
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(AddCategory);
