using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel.Model;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RegistrationController(UserManager<User> userManager, IConfiguration config, IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Consumes("application/json")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] NewPatientDTO newUser)
        {
            var userToCreate = _mapper.Map<Patient>(newUser);
            ////var userToCreate = new Patient();
            ////userToCreate.UserName = newUser.UserName;
            ////userToCreate.PasswordHash = newUser.Password;
            ////userToCreate.MedicalRecord = _mapper.Map<MedicalRecord>(newUser.MedicalRecord);
            ////userToCreate.MedicalRecord.DoctorId = 2;


            var result = await _userManager.CreateAsync(userToCreate, newUser.Password);

            if (!result.Succeeded) return BadRequest(result);

            var userFromDB = await _userManager.FindByNameAsync(userToCreate.UserName);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDB);

            // TODO: use token to build activation URL and call sendEmail method // 

            return Ok(result);

        }
    }
}
