import React, { Component } from 'react';
import Modal from 'react-modal';

const customStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)'
    }
};


export class SOATCrud extends Component {
    static displayName = SOATCrud.name;

    constructor(props) {
        super(props);
        this.state = {
            SOATs: [],
            loading: true,
            adding: false,
            newSOAT: {},
            response: '',
            editingSOAT: undefined,
            originalSOAT: undefined
        };
    }

    componentDidMount() {
        this.populateSOATData();
    }

    changeHandler = (event) => {
        let nam = event.target.name;
        let val = event.target.value;
        let newSOAT = this.state.newSOAT
        newSOAT[nam] = val
        this.setState({ newSOAT });
    }

    editingHandler = (event) => {
        let editingSOAT = this.state.editingSOAT
        let nam = event.target.name;
        let val = event.target.value;
        editingSOAT[nam] = val
        this.setState({ editingSOAT })
    }

    createSOAT = async () => {
        let newSOAT = this.state.newSOAT
        newSOAT.year = newSOAT.year + "-01-01"
        console.log(JSON.stringify(newSOAT));
        const response = await fetch('api/soat',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newSOAT),
                redirect: 'follow'
            });
        response.json()
            .then(data => {
                if (response.status === 201) {
                    this.setState({ response: data })
                    this.populateSOATData();
                    this.closeModal();
                    window.alert("SOAT creado con éxito")
                } else if (response.status >= 400 && response.status < 500) {
                    window.alert("Error al tratar de crear el SOAT, revise los datos")
                }
            })
    }

    deleteSOAT = async (SOAT) => {
        const response = await fetch('api/soat/' + SOAT.owner.document+ '/' + SOAT.vehicle.plate + '/' + (new Date(SOAT.year)).getFullYear(),
            {
                method: 'DELETE',
                redirect: 'follow'
            });
        if (response.status === 204) {
            this.populateSOATData();
            window.alert("SOAT borrado con éxito");
        } else if (response.status >= 400 && response.status < 500) {
            window.alert("Error al tratar de borrar el SOAT")
        }

        // this.populateUserData();
    }

    updateSOAT = async () => {
        let editedSOAT = this.state.editingSOAT
        editedSOAT.year = editedSOAT.year + "-01-01"
        let originalSOAT = this.state.originalSOAT
        const response = await fetch('api/soat/' + originalSOAT.owner.document + "/" + originalSOAT.vehicle.plate + "/" + (new Date(originalSOAT.year)).getFullYear(),
            {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(editedSOAT),
                redirect: 'follow'
            });
        if (response.status === 204) {
            this.populateSOATData();
            this.setState({ editingSOAT: undefined });
            window.alert("SOAT actualizado con éxito")
        } else if (response.status >= 400 && response.status < 500) {
            window.alert("Error al tratar de crear el SOAT, revise los datos del SOAT")
        }

    }

    editSOAT = (SOAT) => {
        let editingSOAT =  {
            owner: SOAT.owner.document,
            vehicle: SOAT.vehicle.plate,
            year: (new Date(SOAT.year)).getFullYear()
        }
        this.setState({ editingSOAT, originalSOAT: SOAT});
    }

    saveEdit = () => {
        this.updateSOAT();
    }

    closeModal = () => {
        this.setState({ adding: false });
    }

    openModal = () => {
        this.setState({ adding: true });
    }

    renderSOATsTable = (SOATs) => {
        let { editingSOAT } = this.state
        return (
            <>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>OwnerDocument</th>
                            <th>VehiclePlate</th>
                            <th>Year</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {SOATs.map((SOAT, index) =>
                            (editingSOAT !== undefined && editingSOAT.owner === SOAT.owner.document && editingSOAT.vehicle === SOAT.vehicle.plate) ?
                                <tr key={index}>
                                    <td>
                                        {editingSOAT.owner}
                                    </td>
                                    <td>
                                        <input
                                            defaultValue={editingSOAT.vehicle}
                                            type='text'
                                            name='vehicle'
                                            onChange={this.editingHandler}
                                        />
                                    </td>
                                    <td>
                                        <input
                                            defaultValue={editingSOAT.year}
                                            type="number"
                                            min="1900"
                                            max="2099"
                                            step="1"
                                            name="year"
                                            onChange={this.editingHandler}
                                        />
                                    </td>
                                    <td>
                                        <button className="btn btn-secondary" onClick={this.saveEdit}>✏️</button>
                                    </td>
                                </tr>
                                :
                                <tr key={index}>
                                    <td>{SOAT.owner.document}</td>
                                    <td>{SOAT.vehicle.plate}</td>
                                    <td>{SOAT.year}</td>
                                    {editingSOAT === undefined &&
                                        <td>
                                            <button className="btn btn-secondary" onClick={() => this.editSOAT(SOAT)}>✏️</button>
                                            <button className="btn btn-danger" onClick={() => this.deleteSOAT(SOAT)}>X</button>
                                        </td>
                                    }
                                </tr>
                        )}
                    </tbody>
                </table>
                <div className="col text-center">
                    <button className="btn btn-secondary centered" onClick={this.openModal} style={{ position: 'relative', left: 'auto', right: 'auto' }}>Agregar</button>
                </div>
            </>
        );
    }

    FormSOAT = () => {
        return (
            <Modal
                isOpen={this.state.adding}
                contentLabel="Crear SOAT"
                onRequestClose={this.closeModal}
                ariaHideApp={false}
                style={customStyles}
            >
                <div className="col text-center">
                    <p>Owner</p>
                    <input
                        type='number'
                        name='owner'
                        onChange={this.changeHandler}
                    />
                    <p>Vehicle</p>
                    <input
                        type='text'
                        name='vehicle'
                        onChange={this.changeHandler}
                    />
                    <p>Year</p>
                    <input
                        type="number"
                        min="1900"
                        max="2099"
                        step="1"
                        defaultValue="2020"
                        name="year"
                        onChange={this.changeHandler}
                    />
                    <p />
                    <button className="btn btn-secondary" onClick={this.createSOAT}>Agregar</button>
                    <p />
                </div>
            </Modal>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderSOATsTable(this.state.SOATs);
        let form = this.FormSOAT();

        return (
            <div>
                <h1 id="tabelLabel" >SOATs</h1>
                {contents}
                {form}
            </div>
        );
    }

    async populateSOATData() {
        this.setState({ loading: true });
        const response = await fetch('api/soat');
        console.log(response);
        const data = await response.json();
        console.log(data);
        this.setState({ SOATs: data, loading: false });
    }
}
