
import React, { Component } from 'react';
import axios from 'axios';



export class ProductsList extends Component {
        state = {
            objects: []
        }

    componentDidMount() {
        axios.get(`/api/ProductsEbay/Get/?keywords=iphone`)
            .then(res => {
                const objects = res.data;
                this.setState({ objects });
            })
        }
    // загрузка данных
    



    render() {

        return (
            <ol>
                {this.state.objects.map(ob => <li>{ob.title}</li>)}
            </ol>
        );
    }
}
