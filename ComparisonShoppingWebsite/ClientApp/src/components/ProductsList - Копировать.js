
import React, { Component } from 'react';

export class Product extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.product };
    }
    render() {
        return <div>
            <p><b>{this.state.data.title}</b></p>
            <p>URL: {this.state.data.url}</p>
            <p>Цена: {this.state.data.price}</p>
            <p>Валюта: {this.state.data.currentcy}</p>

        </div>;
    }
}

export class SearchForm extends React.Component {

    constructor(props) {
        super(props);
        this.state = { keywords: "" };

        this.onSubmit = this.onSubmit.bind(this);
        this.onKeywordChange = this.onKeywordChange.bind(this);
    }
    onKeywordChange(e) {
        this.setState({ keywords: e.target.value });
    }
    onSubmit(e) {
        e.preventDefault();
        var words = this.state.keywords;

        this.props.onSearchSubmit(words);
    }
    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <p>
                    <input type="text"
                        placeholder="Ключевые слова"
                        value={this.state.keywords}
                        onChange={this.onKeywordChange} />
                </p>
                <input type="submit" value="Поиск" />
            </form>
        );
    }
}

export class ProductsList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { products: [] };

        this.loadData = this.loadData.bind(this);
    }
    // загрузка данных
    loadData(keywords) {
        if (keywords) {
            var url = this.props.apiUrl + "/?keywords=" + keywords;
            var xhr = new XMLHttpRequest();
            xhr.open("get", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                if (xhr.status === 200) {
                    var data = JSON.parse(xhr.responseText);
                    this.setState({ products: data });
                }
            }.bind(this);
            xhr.send();
        }
        else {
            var xhr = new XMLHttpRequest();
            xhr.open("get", this.props.apiUrl, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                if (xhr.status === 200) {
                    var data = JSON.parse(xhr.responseText);
                    this.setState({ products: data });
                }
            }.bind(this);
            xhr.send();
        }
        this.setState({ products: [] });
    }

    componentDidMount() {
        this.loadData();
    }


    render() {

        return <div>
            <SearchForm onSearchSubmit={this.loadData} />
            <h2>Результат поиска</h2>
            <div>
                {
                    this.state.products.map(function (product) {

                        return <Product key={product.id} product={product} />
                    })
                }
            </div>
        </div>;
    }
}
