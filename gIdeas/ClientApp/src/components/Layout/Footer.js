import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { Link } from 'react-router-dom';

export class Footer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <footer className="footer row pt-3 pb-3">
                <Container className="custom-container text-white">
                    <div className='col-6 m-0 p-0'>
                        <ul>
                            <li className={'pt-2'}><Link to='/Privacy'>Privacy</Link></li>
                            <li className={'pt-2'}><a >&copy; 2019 Project</a></li>
                        </ul>
                    </div>
                    <div className='col-6 m-0 p-0'>
                        <ul>
                        </ul>
                    </div>
                </Container>
            </footer>
        );
    }
}
