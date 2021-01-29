using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SOATController : ControllerBase
    {
        private readonly ISoatRepo repository;
        private readonly IMapper mapper;

        public SOATController(ISoatRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SOATReadDto>> GetAllSoats()
        {
            var soatItems = repository.GetAllSoats();
            return Ok(mapper.Map<IEnumerable<SOATReadDto>>(soatItems));
        }

        [HttpGet("{document:int}/{plate}", Name = "GetSOATByOwnerDocumentAndPlate")]
        public ActionResult<IEnumerable<SOATReadDto>> GetSOATByOwnerDocumentAndPlate(int document, string plate)
        {
            var soatItems = repository.GetSOATByOwnerDocumentAndPlate(document, plate);
            return Ok(mapper.Map<IEnumerable<SOATReadDto>>(soatItems));
        }

        [HttpGet("{document:int}/{plate}/{year}", Name = "GetSOATByOwnerDocumentPlateAndYear")]
        public ActionResult<IEnumerable<SOATReadDto>> GetSOATByOwnerDocumentPlateAndYear(int document, string plate, DateTime year)
        {
            var soatItem = repository.GetSOATByOwnerDocumentPlateAndYear(document, plate, year);
            return Ok(mapper.Map<SOATReadDto>(soatItem));
        }

        [HttpPost]
        public ActionResult<SOATReadDto> CreateSOAT(SOATCreateDto soat)
        {
            var soatModel = mapper.Map<SOAT>(soat);
            try{
                repository.CreateSOAT(soatModel);
                repository.SaveChanges();
            }catch(DbUpdateException)
            {
                return Conflict(new {errorMessage = "Error al tratar de crear el SOAT, revise que el vehiculo y el usuario existen, y que no se repita la fecha para el mismo vehiculo"});
            }
            var soatReadDto = mapper.Map<SOATReadDto>(soatModel);
            return CreatedAtRoute(nameof(GetSOATByOwnerDocumentAndPlate), new { Document = soatModel.Owner.Document, Plate = soatModel.Vehicle.Plate }, soatReadDto);
        }

        [HttpPut("{document:int}/{plate}/{year:int}")]
        public ActionResult UpdateSOAT(int document, string plate, int year, SOATCreateDto soat)
        {
            var soatModel = repository.GetSOATByOwnerDocumentPlateAndYear(soat.Owner, soat.Vehicle, new DateTime(year, 1, 1));
            if (soatModel == null)
            {
                return NotFound();
            }
            repository.DeleteSOAT(soatModel);
            repository.SaveChanges();
            try{
                var newSoatModel = mapper.Map<SOAT>(soat);
                repository.CreateSOAT(newSoatModel);
                repository.SaveChanges();
            }catch(Exception){
                repository.CreateSOAT(soatModel);
                repository.SaveChanges();
                return Conflict(new {errorMessage = "No se pudo actualizar el SOAT"});
            }
            return NoContent();
        }

        [HttpDelete("{document:int}/{plate}/{year:int}")]
        public ActionResult DeleteSOAT(int document, string plate, int year)
        {
            var soatModel = repository.GetSOATByOwnerDocumentPlateAndYear(document, plate, new DateTime(year, 1, 1));
            if (soatModel == null)
            {
                return NotFound();
            }
            repository.DeleteSOAT(soatModel);
            repository.SaveChanges();
            return NoContent();
        }
    }
}