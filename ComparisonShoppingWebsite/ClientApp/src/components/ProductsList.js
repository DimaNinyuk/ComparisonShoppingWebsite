
import React, { Component } from 'react';
import axios from 'axios';
import { NavLink } from 'react-router-dom';

//компонент вывода отдельного продукта 
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
            <p>image: {this.props.product.imageurl}</p>
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

//компонент вывода списка продуктов с поиском
export class ProductsList extends React.Component {

    constructor(props) {
        super(props);
        //объ€вл€ем 2 элемента состо€ни€, строка ключевых слов и массив продуктов
        this.state = {
            keywords:"",
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
        this.loadData();
    }
    //метод загрузки данных, получаем данные по ссылки и записываем их в массив соста€ни€ products
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
    //описываем рендер, создаем элементы и подписываемс€ на соответствующие методы
    render() {
        return <div className="my-class">
            <p>
                <input type="text"
                    placeholder="Keywords"
                    value={this.state.keywords}
                    onChange={this.onKeywordChange} />
            </p>
            <button onClick={this.filterData}>Search</button>
            <h2>Results "{this.state.keywords}"</h2>
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
