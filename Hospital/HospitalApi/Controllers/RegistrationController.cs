using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Hospital.MedicalRecords.Service;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RegistrationController(UserManager<User> userManager, IConfiguration config, IMapper mapper, RoleManager<IdentityRole<int>> roleManager)
        {
            _config = config;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [Consumes("application/json")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] NewPatientDTO newUser)
        {
            if (!(await _roleManager.RoleExistsAsync("Patient")))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>("Patient"));
            }


            var userToCreate = _mapper.Map<Patient>(newUser);

            var result = await _userManager.CreateAsync(userToCreate, newUser.Password);

            if (!result.Succeeded) return BadRequest(result);

            var userFromDB = await _userManager.FindByNameAsync(userToCreate.UserName);
            await _userManager.AddToRoleAsync(userFromDB, "Patient");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDB);

            var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = newUser.Email }, Request.Scheme);
            EmailService emailService = new EmailService();
            bool emailResponse = emailService.SendEmail(userFromDB.Email, confirmationLink);
            return Ok(result);

        }

        [Consumes("application/json")]
        [HttpPost]
        public async Task<IActionResult> RegisterManager([FromBody] NewManagerDTO newUser)
        {
            if (!(await _roleManager.RoleExistsAsync("Manager")))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>("Manager"));
            }


            var userToCreate = _mapper.Map<Manager>(newUser);
            userToCreate.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(userToCreate, newUser.Password);

            if (!result.Succeeded) return BadRequest(result);

            var userFromDB = await _userManager.FindByNameAsync(userToCreate.UserName);
            await _userManager.AddToRoleAsync(userFromDB, "Manager");
            return Ok(result);

        }
    }
}
