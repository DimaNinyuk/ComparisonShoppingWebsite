
import React, { Component } from 'react';
import axios from 'axios';
import { NavLink } from 'react-router-dom';

//��������� ������ ���������� �������� 
export class Product extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            details: (this.props.product.detailsenabled === true) ?
                <NavLink to={`/details/${this.props.product.name}-${this.props.product.id}`}
                >Get details</NavLink> : null
        };

    }
    render() {
        
        return <div>
            <p>shop: {this.props.product.name}</p>
            <img width="140px" height="124px" src={this.props.product.imageurl} />
            <p><b>{this.props.product.title}</b></p>
            <p>URL: {this.props.product.url}</p>
            <p>Prise: {this.props.product.price}</p>
            <p>Currentcy: {this.props.product.currentcy}</p>
            <p>id: {this.props.product.id}</p>
            <a href={`${this.props.product.url}`}>To {this.props.product.name}</a>
            <p> {this.state.details}</p>
            <hr/>
        </div>;    
    }
}

//��������� ������ ������ ��������� � �������
export class ProductsList extends React.Component {

    constructor(props) {
        super(props);
        //��������� 2 �������� ���������, ������ �������� ���� � ������ ���������
        this.state = {
            keywords:"",
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
        this.loadData();
    }
    //����� �������� ������, �������� ������ �� ������ � ���������� �� � ������ ��������� products
    loadData() {
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
            <h2>Results "{this.state.keywords}"</h2>
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
