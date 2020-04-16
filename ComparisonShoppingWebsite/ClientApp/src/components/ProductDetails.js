
import React, { Component } from 'react';
import axios from 'axios';

//компонент вывода отдельного продукта 

export class ProductDetails extends React.Component {
    
    state = {
        object: []
    }

 

    render() {

        return <div>
            <div class="card border-secondary my-sm-3">

                <div class="card-header bg-secondary text-white">
                    <p>shop: {this.props.match.params.object.url}</p>
                </div>
                <div class="card-body">
                    
                    <img width="140px" height="124px"  />

                    <p><b>{}</b></p>
            
            <p>Prise: {}</p>
            <p>Currentcy: {}</p>
                    <p>id: {}</p>
                    
                    <a class="btn btn-secondary m-1 p-1" >Go</a>
                </div>
            </div>
        </div>;
    }
}
