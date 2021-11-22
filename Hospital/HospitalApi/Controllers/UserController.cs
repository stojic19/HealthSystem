﻿using System;
using System.Threading.Tasks;
using Hospital.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public UserController(UserManager<User> userManager, IConfiguration config)
        {
            _config = config;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User newUser)  
        {
        //var userToCreate = new User // mapping
        //{
        //    Email = newUser.Email,
        //    FirstName = newUser.FirstName,
        //    UserName = newUser.UserName
        //};

        //var result = await _userManager.CreateAsync(userToCreate, newUser.PasswordHash);

        //if (result.Succeeded)
        //{

        //    var userFromDB = await _userManager.FindByNameAsync(userToCreate.UserName);

        //    var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDB);

        //    // TODO: use token to build activation URL and call sendEmail method // 
        //    // http://localhost:4200/api/activate?token=

        //    return Ok(result);
        //}

        //return BadRequest(result);

        throw new NotImplementedException();
        }   
    }
}