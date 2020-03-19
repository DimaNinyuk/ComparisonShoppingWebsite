import React, { Component } from 'react';
export class AmazonHome extends Component {
    static displayName = AmazonHome.name;
    constructor(props) {
        super(props);
        this.state = { message: "" };

        fetch('api/Products/Get')
            .then(response => response.json())
            .then(data => {
                this.setState({ message: data });
            });
        
    }

    render() {
        return (


            <h1>{this.message.toString()}</h1>

        );
    }
}