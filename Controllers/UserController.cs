using System;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo repository;
        private readonly IMapper mapper;

        public UsersController(IUserRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        //GET /api/users
        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var usersItems = this.repository.GetAllUsers();
            return Ok(mapper.Map<IEnumerable<UserReadDto>>(usersItems));
        }

        //GET /api/users/{document}
        [HttpGet("{document}", Name="GetUserByDocument")]
        public ActionResult<UserReadDto> GetUserByDocument(int document)
        {
            var userItem = this.repository.GetUserByDocument(document);
            if(userItem != null)
            {
                var us = mapper.Map<UserReadDto>(userItem);
                return Ok(mapper.Map<UserReadDto>(userItem));
            }
            return NotFound();
        }

        //POST /api/users/
        [HttpPost]
        public ActionResult CreateUser(UserCreateDto user)
        {
            var commandModel = mapper.Map<User>(user);
            try{
                repository.CreateUser(commandModel);
                repository.SaveChanges();
            }catch(DbUpdateException){
                return Conflict(new {errorMessage = "Error al tratar de crear el usuario, el documento ya existe"});
            }
            var userReadDto = mapper.Map<UserReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetUserByDocument), new {Document = userReadDto.Document}, userReadDto);
        }
        
        //PUT /api/users/{document}
        [HttpPut("{document}")]
        public ActionResult UpdateUser(int document, UserUpdateDto userUpdateDto)
        {
            var userModel = this.repository.GetUserByDocument(document);
            if(userModel == null)
            {
                return NotFound();
            }
            mapper.Map(userUpdateDto, userModel);
            repository.UpdateUser(userModel);
            repository.SaveChanges();
            return NoContent();
        }
        
        //DELETE /api/users/{document}
        [HttpDelete("{document}")]
        public ActionResult DeleteUser(int document)
        {
            var userModel = this.repository.GetUserByDocument(document);
            if(userModel == null)
            {
                return NotFound();
            }
            repository.DeleteUser(userModel);
            repository.SaveChanges();
            return NoContent();
        }
    }
}