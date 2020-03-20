
import React, { Component } from 'react';
import axios from 'axios';

//компонент вывода отдельного продукта 

export class ProductDetails extends React.Component {
    
    state = {
        object: Object
    }

        // загрузка данных
    componentDidMount() {

        const id = this.props.match.params.id;
        const shop = this.props.match.params.shop;
        var self = this;

        axios.get('/api/Details/Get/', {
            params: {
                shop: shop,
                id: id
            }
        })
            .then(function (response) {
                self.setState({ object: response.data })
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    render() {

        return <div>
            <p>shop: {this.state.object.name}</p>
            <p>image: {this.state.object.imageurl}</p>
            <p><b>{this.state.object.title}</b></p>
            <p>URL: {this.state.object.url}</p>
            <p>Prise: {this.state.object.price}</p>
            <p>Currentcy: {this.state.object.currentcy}</p>
            <p>id: {this.state.object.id}</p>
        </div>;
    }
}
