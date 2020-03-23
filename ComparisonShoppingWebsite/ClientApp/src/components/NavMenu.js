import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import './NavMenu.css';


export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
        <header>
            <div class="pos-f-t">
                <div class="collapse" id="navbarToggleExternalContent">
                    <div class="bg-dark p-4">
                    </div>
                </div>
            <Navbar class="navbar navbar-dark bg-dark" light>

                <Container>
                    
           <NavbarBrand class="navbar-brand" tag={Link} to="/">Comparison Shopping Website</NavbarBrand>
                    <NavbarToggler onClick={this.toggleNavbar}  />
                   
                        <Collapse class="collapse navbar-collapse" isOpen={!this.state.collapsed} navbar>
                            <ul class="navbar-nav mr-auto" style={{ float: 'right'}}>
                            <NavItem class="nav-item active">
                                <NavLink class="nav-link" tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                
                           
                <LoginMenu>
                </LoginMenu>
              </ul>
            </Collapse>
          </Container>
                </Navbar>
            </div>
           
      </header>
    );
  }
}
