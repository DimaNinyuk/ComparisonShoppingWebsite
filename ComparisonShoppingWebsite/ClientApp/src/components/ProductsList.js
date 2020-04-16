
import React, { Component } from 'react';
import axios from 'axios';
import { NavLink } from 'react-router-dom';
import './NavMenu.css';
//��������� ������ ���������� �������� 
export class Product extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            details: (this.props.product.detailsenabled === true) ?
                <NavLink class="btn btn-secondary m-1 p-1" to={`/details/${this.props.name}-${this.props.id}`
        }
                >Get details</NavLink> : null
        };

    }
    render() {
        
        return <div>
            
            <div class="card border-secondary my-sm-3">
               
                <div class="card-header bg-secondary text-white">
                    <p>shop: {this.props.product.name}</p>
                </div>
                <div class="card-body">
            <img width="140px" height="124px" src={this.props.product.imageurl} />
            <p><b>{this.props.product.title}</b></p>
            
            <p>Prise: {this.props.product.price}</p>
            <p>Currentcy: {this.props.product.currentcy}</p>
            <p>id: {this.props.product.id}</p>
                    <a class="btn btn-secondary m-1 p-1" href={`${this.props.product.url}`}>To {this.props.product.name}</a>
                    <p > {this.state.details}</p>
                   
              
                </div>
            </div>
        </div>;    
    }
}

//��������� ������ ������ ��������� � �������
export class ProductsList extends React.Component {

    constructor(props) {
        super(props);
        //��������� 2 �������� ���������, ������ �������� ���� � ������ ���������
        this.state = {
            keywords: "",
            products: []
        };
        //��������� ������
        this.filterData = this.filterData.bind(this);
        this.onKeywordChange = this.onKeywordChange.bind(this);
        this.loadData = this.loadData.bind(this);
    }
    //����� ��������� �������� �������� ����, �������� ��������� keywords
    onKeywordChange(e) {
        this.setState({ keywords: e.target.value });
    }
    //����� ������, �������� ���� �������� ������
    filterData(e) {
        this.setState({
            products: []
        })
        this.loadData();
    }
    //����� �������� ������, �������� ������ �� ������ � ���������� �� � ������ ��������� products
    loadData() {
        this.setState({
            products: []
        })
        var self = this;
        axios.get('/api/search/get', {
            params: {
                keywords: this.state.keywords
            }
        })
            .then(function (response) {
                self.setState({ products: response.data })
            })
            .catch(function (error) {
                console.log(error);
            });
    }
    //��������� ������, ������� �������� � ������������� �� ��������������� ������
    render() {
        return <div className="my-class">
            <div class="input-group mb-3">
                <input type="text" class="form-control" aria-describedby="basic-addon2"
                    placeholder="Keywords"
                    value={this.state.keywords}
                    onChange={this.onKeywordChange} />
                <div class="input-group-append">
                    <button onClick={this.filterData}>Search</button>
                </div>
            </div>
            
            <div class="PD">
            </div>
            <div>
                { 
                    // ������� ������ products, ��� ������� �������� ������� ��� ������� Product
                    this.state.products.map(function (product) {
                        return <Product key={product.id} product={product} />
                    })
                    }
                
            </div>

        </div>;
    }
}
