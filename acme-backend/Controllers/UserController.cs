﻿using acme_backend.Models.Dtos;
using acme_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace acme_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioListDto>>> listUsers()
        {
            try
            {
                var users = await _userService.listUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioListDto>> getUserById(string id)
        {
            try
            {
                var user = await _userService.getUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> createUser(UsuarioCreateDto userDto)
        {
            try
            {
                var newUser = await _userService.createUser(userDto);

                if (newUser == null)
                {
                    Console.WriteLine("Error on create user");
                    return BadRequest();
                }

                return CreatedAtAction(nameof(getUserById), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateUser(string id, UsuarioDto userDto)
        {
            try
            {
                await _userService.updateUser(id, userDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteUser(string id)
        {
            try
            {
                await _userService.deleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}