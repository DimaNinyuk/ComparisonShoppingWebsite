import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { ProductsList } from './components/ProductsList';
import { ProductDetails } from './components/ProductDetails';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
       <Route exact path='/' component={Home} />
       <AuthorizeRoute path='/search' component={ProductsList} />
       <Route path='/details/:shop-:id' component={ProductDetails} />
       <AuthorizeRoute path='/fetch-data' component={FetchData} />
       <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
