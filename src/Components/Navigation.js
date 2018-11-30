import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Link } from 'react-router-dom';
import { 
    Navbar,
    NavbarBrand,
    NavbarToggler,
    Collapse,
    Nav,
    NavItem,
    NavLink
 } from 'reactstrap';

 import Counter from '../Container/Counter';
 import Home from './Home';

export default class Navigation extends Component {
  constructor (props) {
      super(props);

      this.toggle = this.toggle.bind(this);
      this.state = {
        isOpen: false
      };
  }
  toggle () {
      this.setState ({
          isOpen: !this.state.isOpen
      });
  }
  render() {
    return (
        <Router>
          <div>
            <Navbar color="dark" dark expand="md">
            <NavbarBrand href="/">MyReactApp</NavbarBrand>
            <NavbarToggler onClick={this.toggle}></NavbarToggler>
            <Collapse isOpen={this.state.isOpen} navbar>
                <Nav className="mr-auto" navbar>
                <NavItem>
                    <Link to="/" className="nav-link">Home</Link>
                </NavItem>
                <NavItem>
                    <Link to="/counter" className="nav-link">Counter</Link>
                </NavItem>
                </Nav>
            </Collapse>
            </Navbar>
            <Route path="/" exact component={Home}></Route> 
            <Route path="/counter/" component={Counter}></Route> 
          </div>
        </Router>
    )
  }
}
