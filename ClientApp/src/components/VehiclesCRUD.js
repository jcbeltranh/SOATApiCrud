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


export class VehiclesCRUD extends Component {
    static displayName = VehiclesCRUD.name;

    constructor(props) {
        super(props);
        this.state = {
            vehicles: [],
            loading: true,
            adding: false,
            newVehicle: {},
            response: '',
            editingVehicle: undefined
        };
    }

    componentDidMount() {
        this.populateVehicleData();
    }

    changeHandler = (event) => {
        let nam = event.target.name;
        let val = event.target.value;
        let newVehicle = this.state.newVehicle
        newVehicle[nam] = val
        this.setState({ newVehicle });
    }

    editingHandler = (event) => {
        let editingVehicle = this.state.editingVehicle
        let nam = event.target.name;
        let val = event.target.value;
        editingVehicle[nam] = val
        this.setState({ editingVehicle })
    }

    createVehicle = async () => {
        const response = await fetch('api/vehicles',
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(this.state.newVehicle),
                redirect: 'follow'
            });
        response.json()
            .then(data => {
                if (response.status === 201) {
                    this.setState({ response: data })
                    this.populateVehicleData();
                    this.closeModal();
                    window.alert("Vehiculo creado con éxito")
                } else if (response.status >= 400 && response.status < 500) {
                    window.alert("Error al tratar de crear el vehiculo, revise los datos")
                }
            })
    }

    deleteVehicle = async (plate) => {
        const response = await fetch('api/vehicles/' + plate,
            {
                method: 'DELETE',
                redirect: 'follow'
            });
        if (response.status === 204) {
            this.populateVehicleData();
            window.alert("Vehiculo borrado con éxito");
        } else if (response.status >= 400 && response.status < 500) {
            window.alert("Error al tratar de borrar el vehiculo")
        }
    }

    updateVehicle = async () => {
        let editingVehicle = this.state.editingVehicle
        const response = await fetch('api/vehicles/' + editingVehicle.plate,
            {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(editingVehicle),
                redirect: 'follow'
            });
        if (response.status === 204) {
            this.populateVehicleData();
            this.setState({ editingVehicle: undefined });
            window.alert("Vehiculo actualizado con éxito")
        } else if (response.status >= 400 && response.status < 500) {
            window.alert("Error al tratar de crear el vehiculo, revise los datos del vehiculo")
        }

    }

    editVehicle = (vehicle) => {
        this.setState({ editingVehicle: vehicle });
    }

    saveEdit = () => {
        this.updateVehicle();
    }

    closeModal = () => {
        this.setState({ adding: false });
    }

    openModal = () => {
        this.setState({ adding: true });
    }

    renderVehiclesTable = (vehicles) => {
        let { editingVehicle } = this.state
        return (
            <>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Plate</th>
                            <th>Color</th>
                            <th>Engine</th>
                            <th>Axles</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {vehicles.map(vehicle =>
                            (editingVehicle !== undefined && editingVehicle.plate === vehicle.plate) ?
                                <tr key={vehicle.plate}>
                                    <td>
                                        {vehicle.plate}
                                    </td>
                                    <td>
                                        <input
                                            defaultValue={editingVehicle.color}
                                            type='text'
                                            name='color'
                                            onChange={this.editingHandler}
                                        />
                                    </td>
                                    <td>
                                        <input
                                            defaultValue={editingVehicle.engine}
                                            type='text'
                                            name='engine'
                                            onChange={this.editingHandler}
                                        />
                                    </td>
                                    <td>
                                        <input
                                            defaultValue={editingVehicle.axles}
                                            type='number'
                                            name='axles'
                                            onChange={this.editingHandler}
                                        />
                                    </td>
                                    <td>
                                        <button className="btn btn-secondary" onClick={this.saveEdit}>✏️</button>
                                    </td>
                                </tr>
                                :
                                <tr key={vehicle.plate}>
                                    <td>{vehicle.plate}</td>
                                    <td>{vehicle.color}</td>
                                    <td>{vehicle.engine}</td>
                                    <td>{vehicle.axles}</td>
                                    {editingVehicle === undefined &&
                                        <td>
                                            <button className="btn btn-secondary" onClick={() => this.editVehicle(vehicle)}>✏️</button>
                                            <button className="btn btn-danger" onClick={() => this.deleteVehicle(vehicle.document)}>X</button>
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

    FormVehicle = () => {
        return (
            <Modal
                isOpen={this.state.adding}
                contentLabel="Crear vehiculo"
                onRequestClose={this.closeModal}
                ariaHideApp={false}
                style={customStyles}
            >
                <div class="col text-center">
                    <p>Plate</p>
                    <input
                        type='text'
                        name='plate'
                        onChange={this.changeHandler}
                    />
                    <p>Color</p>
                    <input
                        type='text'
                        name='color'
                        onChange={this.changeHandler}
                    />
                    <p>Engine</p>
                    <input
                        type='text'
                        name='engine'
                        onChange={this.changeHandler}
                    />
                    <p>Axles</p>
                    <input
                        type='number'
                        name='axles'
                        onChange={this.changeHandler}
                    />
                    <p />
                    <button className="btn btn-secondary" onClick={this.createVehicle}>Agregar</button>
                    <p />
                </div>
            </Modal>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderVehiclesTable(this.state.vehicles);
        let form = this.FormVehicle();

        return (
            <div>
                <h1 id="tabelLabel" >Vehicles</h1>
                {contents}
                {form}
            </div>
        );
    }

    async populateVehicleData() {
        this.setState({ loading: true });
        const response = await fetch('api/vehicles');
        const data = await response.json();
        this.setState({ vehicles: data, loading: false });
    }
}
