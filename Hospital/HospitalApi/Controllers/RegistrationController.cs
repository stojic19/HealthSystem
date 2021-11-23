using System.Threading.Tasks;
using AutoMapper;
using Hospital.Model;
using Hospital.Repositories.Base;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RegistrationController(IUnitOfWork uow, UserManager<User> userManager, IConfiguration config, IMapper mapper)
        {
            _uow = uow;
            _config = config;
            _userManager = userManager;
            _mapper = mapper;
        }
        [Consumes("application/json")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegistration newUser)  
        {
            var userToCreate = _mapper.Map<Patient>(newUser);

            var result = await _userManager.CreateAsync(userToCreate, newUser.Password);

            if (result.Succeeded)
            {
                var userFromDB = await _userManager.FindByNameAsync(userToCreate.UserName);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDB);

                // TODO: use token to build activation URL and call sendEmail method // 
                // http://localhost:4200/User/activate?token=

                return Ok(result);
            }

            return BadRequest(result);
        }   
    }
}
