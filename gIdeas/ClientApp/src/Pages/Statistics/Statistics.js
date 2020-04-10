/* eslint-disable import/first */
import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';

// external libraries
import { getDepartmentStatistics } from '../../Actions/DepartmentActions';
import { getAllPageViews } from '../../Actions/PageViewActions';
import { getBrowserStatistics } from '../../Actions/AuthenticationActions';
import { VictoryPie, VictoryChart, VictoryBar, VictoryGroup, VictoryTooltip, VictoryLabel, VictoryAxis, VictoryTheme } from 'victory'; // https://formidable.com/open-source/victory/
import RecordPageView from "../../components/RecordPageView";

class Statistics extends PureComponent {
    constructor(props) {
        super(props);
        this.state = {
            departmentStatistics: [],
            departmentStatisticsTotalNum: [],
            pageViewStatistics: [],
            browserStatsResult: [],
            isLoading: true,
            accordion1: "show",
            accordion2: "",
            accordion3: "",
            accordion4: "",
        }
        this.showAccordion = this.showAccordion.bind(this);
    }
    // executed directly after the component has been rendered to the DOM
    // sets the state in the constructor
    async componentDidMount() {
        var departmentStatsResults = await this.props.getDepartmentStatistics();
        var pageviewStatsResult = await this.props.getAllPageViews();
        var browserStatsResult = await this.props.getBrowserStatistics();
        departmentStatsResults.DepartmentStatistics.map(d => {
            this.state.departmentStatistics.push({
                x: d.id,
                y: d.totalPercentageOfIdeas,
                label: d.name
            })
            this.state.departmentStatisticsTotalNum.push({
                x: d.id,
                y: d.totalNumberOfIdeas,
                label: d.name
            })
        })
        pageviewStatsResult.PageViews.map(d => {
            this.state.pageViewStatistics.push({
                x: d.pageName,
                y: d.pageCount,
                label: d.name
            })
        })
        browserStatsResult.browserStatistics.map(d => {
            this.state.browserStatsResult.push({
                x: d.name,
                y: d.totalHits,
                label: d.name
            })
        })
        try {
            this.setState({ accordion1: "show" });
        }
        catch (e) { }

    }
    showAccordion(selected) {
        this.state.accordion1 = "";
        this.state.accordion2 = "";
        this.state.accordion3 = "";
        this.state.accordion4 = "";

        switch (selected) {
            case 1:
                this.setState({ accordion1: "show" });
                break
            case 2:
                this.setState({ accordion2: "show" });
                break
            case 3:
                this.setState({ accordion3: "show" });
                break
            case 4:
            default:
                this.setState({ accordion4: "show" });
                break
        }
    }
    render() {
        const { accordion1, accordion2, accordion3, accordion4 } = this.state;
        return (
            <Container className="custom-container">
                <div className="img-header mb-3">
                    <div className="page-header">Statistics</div>
                </div>
                <RecordPageView IdeaId="0" />
                <div className="accordion">
                    {/* card one */}
                    <div className="card">
                        <div className="card-header">
                            <h2 className="mb-0">
                                <button className="btn btn-link" onClick={() => { this.showAccordion(1) }}>
                                    Precentage of Ideas Per Department
                                </button>
                            </h2>
                        </div>

                        <div className={"collapse " + this.state.accordion1} visible="true">
                            <div class="card-body">
                                <VictoryGroup>
                                    <VictoryPie
                                        labelComponent={<VictoryTooltip />} //added
                                        labelRadius={130} //added
                                        style={{ labels: { fill: "black", fontSize: 5 } }}
                                        data={this.state.departmentStatistics}
                                        colorScale="cool" //added
                                    />
                                </VictoryGroup>
                            </div>
                        </div>
                    </div>
                    {/* card two */}
                    <div className="card">
                        <div className="card-header">
                            <h2 className="mb-0">
                                <button className="btn btn-link" onClick={() => { this.showAccordion(2) }}>
                                    Total Ideas Per Department
                                </button>
                            </h2>
                        </div>
                        <div className={"collapse " + this.state.accordion2} visible="true">
                            <VictoryChart
                                domainPadding={{ x: 1 }}
                            >
                                <VictoryBar horizontal
                                    style={{
                                        data: { fill: "#c43a31" },
                                        labels: { fontSize: 0 }
                                    }}
                                    data={this.state.departmentStatisticsTotalNum}
                                />
                            </VictoryChart>

                        </div>
                    </div>
                    {/* card three */}
                    <div className="card">
                        <div className="card-header">
                            <h2 className="mb-0">
                                <button className="btn btn-link" onClick={() => { this.showAccordion(3) }}>
                                    Page View Stats
                                </button>
                            </h2>
                        </div>
                        <div className={"collapse " + this.state.accordion3} visible="true">

                            <VictoryChart

                                domainPadding={{ x: 50 }}
                            >
                                <VictoryBar horizontal
                                    style={{
                                        data: { fill: "#c43a31" },
                                        labels: { fontSize: 0 }
                                    }}
                                    data={this.state.pageViewStatistics}
                                />
                            </VictoryChart>
                        </div>
                    </div>
                    {/* card four */}
                    <div className="card">
                        <div className="card-header">
                            <h2 className="mb-0">
                                <button className="btn btn-link" onClick={() => { this.showAccordion(4) }}>
                                    Browser Statistics
                                </button>
                            </h2>
                        </div>
                        <div className={"collapse " + this.state.accordion4} visible="true">
                            <VictoryChart domainPadding={{ x: 50 }} >
                                <VictoryBar horizontal
                                    className="pl-5"
                                    style={{
                                        data: { fill: "#c43a31" },
                                        labels: { fontSize: 0 }
                                    }}
                                    data={this.state.browserStatsResult}
                                />
                            </VictoryChart>
                        </div>
                    </div>
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
    getDepartmentStatistics,
    getBrowserStatistics,
    getAllPageViews
}
/// Redux Connection before exporting the component
export default connect(
    mapStateToProps,
    dispatch => bindActionCreators(mapDispatchToProps, dispatch)
)(Statistics);