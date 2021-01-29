import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Prueba técnica</h1>
        <p></p>
        <h3>La aplicación actual funciona sobre .Net Core 3.1, usa de base de datos SQLite y ORM Entity Framework</h3>
        <p>Se manejan DTOs para separar la vista dependiendo de lo que se requiere en el momento, y para mostrar únicamente lo necesario.</p>
        <p>El crud del SOAT es un tanto distinto dado que este tiene todas sus propiedades como llaves, por lo que actualizarlo representa eliminarlo y volver a crearlo con los nuevos datos</p>
      </div>
    );
  }
}
