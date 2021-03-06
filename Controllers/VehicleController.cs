using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOATApiReact.Data;
using SOATApiReact.DTOs;
using SOATApiReact.Model;

namespace SOATApiReact.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepo repository;
        private readonly IMapper mapper;

        public VehiclesController(IVehicleRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        //GET /api/vehicles
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetAllVehicles()
        {
            var vehiclesItems = this.repository.GetAllVehicles();
            return Ok(mapper.Map<IEnumerable<VehicleReadDto>>(vehiclesItems));
        }

        //GET /api/vehicles/{plate}
        [HttpGet("{plate}", Name="GetVehicleByPlate")]
        public ActionResult<VehicleReadDto> GetVehicleByPlate(string plate)
        {
            var vehicleItem = this.repository.GetVehicleByPlate(plate);
            if(vehicleItem != null)
            {
                return Ok(mapper.Map<VehicleReadDto>(vehicleItem));
            }
            return NotFound();
        }

        //POST /api/vehicles
        [HttpPost]
        public ActionResult<VehicleReadDto> CreateVehicle(VehicleCreateDto vehicleCreateDto)
        {
            var vehicleModel = mapper.Map<Vehicle>(vehicleCreateDto);
            try{
                repository.CreateVehicle(vehicleModel);
                repository.SaveChanges();
            }catch(DbUpdateException){
                //Para este punto el Dto vehiculo ya debe tener todos los datos necesarios y validados con el modelo
                //por lo que la única razón para un error es que haya conflicto con las llaves
                return Conflict(new {errorMessage = "Error al tratar de crear el vehiculo, la placa ya existe"});
            }
            var vehicleReadDto = mapper.Map<VehicleReadDto>(vehicleModel);
            return CreatedAtRoute(nameof(GetVehicleByPlate), new {Plate = vehicleReadDto.Plate}, vehicleReadDto);
        }

        //PUT /api/vehicles/{plate}
        [HttpPut("{plate}")]
        public ActionResult UpdateVehicle(string plate, VehicleUpdateDto vehicleUpdateDto)
        {
            var vehicleModel = repository.GetVehicleByPlate(plate);
            if(vehicleModel == null)
            {
                return NotFound();
            }
            mapper.Map(vehicleUpdateDto, vehicleModel);
            repository.UpdateVehicle(vehicleModel);
            repository.SaveChanges();
            return NoContent();
        }

        //DELETE /api/vehicles/{plate}
        [HttpDelete("{plate}")]
        public ActionResult DeleteVehicle(string plate)
        {
            var vehicleModel = repository.GetVehicleByPlate(plate);
            if(vehicleModel == null)
            {
                return NotFound();
            }
            repository.DeleteVehicle(vehicleModel);
            repository.SaveChanges();
            return NoContent();
        }
    }
}