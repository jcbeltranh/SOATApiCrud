import React, { Component } from 'react';
import Modal from 'react-modal';

const customStyles = {
    content: {
        top: '40%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)'
    }
};


export class UsersCRUD extends Component {
    static displayName = UsersCRUD.name;

    constructor(props) {
        super(props);
        this.state = {
            users: [],
            loading: true,
            adding: false,
            newUser: {
                documentType: "CC"
            },
            response: '',
            editingUser: undefined
        };
    }

    componentDidMount() {
        this.populateUserData();
    }

    changeHandler = (event) => {
        let nam = event.target.name;
        let val = event.target.value;
        let newUser = this.state.newUser
        newUser[nam] = val
        this.setState({ newUser });
    }

    editingHandler = (event) => {
        let editingUser = this.state.editingUser
        let nam = event.target.name;
        let val = event.target.value;
        editingUser[nam] = val
        this.setState({ editingUser })
    }

    createUser = async () => {
        const response = await fetch('api/users',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(this.state.newUser),
                redirect: 'follow'
            });
        response.json()
            .then(data => {
                console.log(data);
                if (response.status === 201) {
                    this.setState({ response: data })
                    this.populateUserData();
                    this.closeModal();
                    window.alert("Usuario creado con éxito")
                } else if (response.status >= 400 && response.status < 500) {
                    window.alert("Error al tratar de crear el usuario, revise los datos")
                }
            })
    }

    deleteUser = async (document) => {
        const response = await fetch('api/users/' + document,
            {
                method: 'DELETE',
                redirect: 'follow'
            });
        if (response.status === 204) {
            this.populateUserData();
            window.alert("Usuario borrado con éxito");
        } else if (response.status >= 400 && response.status < 500) {
            window.alert("Error al tratar de borrar el usuario")
        }

        // this.populateUserData();
    }

    updateUser = async () => {
        let editedUser = this.state.editingUser
        const response = await fetch('api/users/' + editedUser.document,
            {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(editedUser),
                redirect: 'follow'
            });
        if (response.status === 204) {
            this.populateUserData();
            this.setState({ editingUser: undefined });
            window.alert("Usuario actualizado con éxito")
        } else if (response.status >= 400 && response.status < 500) {
            window.alert("Error al tratar de crear el usuario, revise los datos del usuario")
        }

    }

    editUser = (user) => {
        this.setState({ editingUser: user });
    }

    saveEdit = () => {
        console.log(this.state.editingUser);
        this.updateUser();
    }

    closeModal = () => {
        this.setState({ adding: false });
    }

    openModal = () => {
        this.setState({ adding: true });
    }

    renderForecastsTable = (users) => {
        let { editingUser } = this.state
        return (
            <>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Document</th>
                            <th>Doc. Type</th>
                            <th>Name</th>
                            <th>Surname</th>
                            <th>Genre</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map(user =>
                            (editingUser !== undefined && editingUser.document === user.document) ?
                                <tr key={user.document}>
                                    <td>
                                        {user.document}
                                    </td>
                                    <td>
                                        <select
                                            name='documentType'
                                            defaultValue={editingUser.documentType}
                                            multiple={false}
                                            onChange={this.editingHandler}>
                                            <option value="CC">Cédula de Ciudadanía</option>
                                            <option value="CE">Cédula de extranjería</option>
                                            <option value="TI">Tarjeta de Identidad</option>
                                            <option value="NIT">Número de Identificacion Tributaria</option>
                                            <option value="DIP">Documento Diplomático</option>
                                        </select>
                                    </td>
                                    <td>
                                        <input
                                            defaultValue={editingUser.name}
                                            type='text'
                                            name='name'
                                            onChange={this.editingHandler}
                                        />
                                    </td>
                                    <td>
                                        <input
                                            defaultValue={editingUser.surname}
                                            type='text'
                                            name='surname'
                                            onChange={this.editingHandler}
                                        />
                                    </td>
                                    <td>
                                        <input
                                            defaultValue={editingUser.genre}
                                            type='text'
                                            name='genre'
                                            onChange={this.editingHandler}
                                        />
                                    </td>
                                    <td>
                                        <button className="btn btn-secondary" onClick={this.saveEdit}>✏️</button>
                                    </td>
                                </tr>
                                :
                                <tr key={user.document}>
                                    <td>{user.document}</td>
                                    <td>{user.documentType}</td>
                                    <td>{user.name}</td>
                                    <td>{user.surname}</td>
                                    <td>{user.genre}</td>
                                    {editingUser === undefined &&
                                        <td>
                                            <button className="btn btn-secondary" onClick={() => this.editUser(user)}>✏️</button>
                                            <button className="btn btn-danger" onClick={() => this.deleteUser(user.document)}>X</button>
                                        </td>
                                    }
                                </tr>
                        )}
                    </tbody>
                </table>
                <div class="col text-center">
                    <button className="btn btn-secondary centered" onClick={this.openModal} style={{ position: 'relative', left: 'auto', right: 'auto' }}>Agregar</button>
                </div>
            </>
        );
    }

    FormUser = () => {
        return (
            <Modal
                isOpen={this.state.adding}
                contentLabel="Crear usuario"
                onRequestClose={this.closeModal}
                ariaHideApp={false}
                style={customStyles}
            >
                <div class="col text-center">

                    <p>Document</p>
                    <input
                        type='number'
                        name='document'
                        onChange={this.changeHandler}
                    />
                    <p>DocumentType</p>
                    <select
                        name='documentType'
                        multiple={false}
                        onChange={this.changeHandler}>
                        <option value="CC">Cédula de Ciudadanía</option>
                        <option value="CE">Cédula de extranjería</option>
                        <option value="TI">Tarjeta de Identidad</option>
                        <option value="NIT">Número de Identificacion Tributaria</option>
                        <option value="DIP">Documento Diplomático</option>
                    </select>
                    <p>Name</p>
                    <input
                        type='text'
                        name='name'
                        onChange={this.changeHandler}
                    />
                    <p>Surname</p>
                    <input
                        type='text'
                        name='surname'
                        onChange={this.changeHandler}
                    />
                    <p>Genre</p>
                    <input
                        type='text'
                        name='genre'
                        onChange={this.changeHandler}
                    />
                    <p />
                    <button className="btn btn-secondary" onClick={this.createUser}>Agregar</button>
                    <p />
                </div>
            </Modal>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForecastsTable(this.state.users);
        let form = this.FormUser();

        return (
            <div>
                <h1 id="tabelLabel" >Users</h1>
                {contents}
                {form}
            </div>
        );
    }

    async populateUserData() {
        this.setState({ loading: true });
        const response = await fetch('api/users');
        const data = await response.json();
        this.setState({ users: data, loading: false });
    }
}
