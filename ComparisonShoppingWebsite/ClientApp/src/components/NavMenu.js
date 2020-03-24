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
        <header class="masthead">

            <nav class="navbar navbar-expand-lg navbar-light bg-light shadow fixed-top">
                <div class="container">
                    <a class="navbar-brand" href="#">Comparison Shopping Website</a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarResponsive">
                        <ul class="navbar-nav ml-auto">
                            <li class="nav-item active">
                                <a class="nav-link" href="/">Home
                <span class="sr-only">(current)</span>
                                </a>
                            </li>
                            <LoginMenu>
                            </LoginMenu>
                        </ul>
                    </div>
                </div>
            </nav>

            <div class="container h-100">
                <div class="row h-100 align-items-center">
                    <div class="col-12 text-center">
                        <h1 class="font-weight-light">The system tracks prices in stores. The system provides the ability to view offers of a product of interest in stores.</h1>
                        <p class="lead">There are a lot of shops on one site, try how convenient it is....</p>
                    </div>
                </div>
            </div>
 
      </header>
    );
  }
}
