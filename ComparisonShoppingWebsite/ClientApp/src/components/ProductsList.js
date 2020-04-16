
import React, { Component } from 'react';
import axios from 'axios';
import { NavLink } from 'react-router-dom';
import './NavMenu.css';
//компонент вывода отдельного продукта 
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

//компонент вывода списка продуктов с поиском
export class ProductsList extends React.Component {

    constructor(props) {
        super(props);
        //объ€вл€ем 2 элемента состо€ни€, строка ключевых слов и массив продуктов
        this.state = {
            keywords: "",
            products: []
        };
        //объ€вл€ем методы
        this.filterData = this.filterData.bind(this);
        this.onKeywordChange = this.onKeywordChange.bind(this);
        this.loadData = this.loadData.bind(this);
    }
    //метод изменени€ значени€ ключевых слов, измен€ет состо€ние keywords
    onKeywordChange(e) {
        this.setState({ keywords: e.target.value });
    }
    //метод посика, вызывает метд загрузки данныз
    filterData(e) {
        this.setState({
            products: []
        })
        this.loadData();
    }
    //метод загрузки данных, получаем данные по ссылки и записываем их в массив соста€ни€ products
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
    //описываем рендер, создаем элементы и подписываемс€ на соответствующие методы
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
                    // выводим массив products, дл€ каждого элемента создаем наш элемент Product
                    this.state.products.map(function (product) {
                        return <Product key={product.id} product={product} />
                    })
                    }
                
            </div>

        </div>;
    }
}
