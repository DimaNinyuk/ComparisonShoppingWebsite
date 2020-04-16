
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
            <div class="card border-secondary my-sm-3">

                <div class="card-header bg-secondary text-white">
                    <p>shop: {this.state.object.name}</p>
                </div>
                <div class="card-body">

                    <img width="140px" height="124px" src={this.state.object.imageurl} />

            <p><b>{this.state.object.title}</b></p>
            
            <p>Prise: {this.state.object.price}</p>
            <p>Currentcy: {this.state.object.currentcy}</p>
                    <p>id: {this.state.object.id}</p>
                    
                    <a class="btn btn-secondary m-1 p-1" href={this.state.object.url}>Go</a>
                </div>
            </div>
        </div>;
    }
}
