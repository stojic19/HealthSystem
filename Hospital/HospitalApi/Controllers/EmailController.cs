using AutoMapper;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Repository.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class EmailController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public EmailController(UserManager<User> usrMgr)
        {
            _userManager = usrMgr;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("User cannot be found.");
            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");

            return RedirectPermanent("http://localhost:4200/login");
        }
    }
}
