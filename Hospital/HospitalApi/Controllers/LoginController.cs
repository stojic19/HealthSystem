using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Hospital.SharedModel.Model;
using Hospital.SharedModel.Service;
using HospitalApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJWTTokenGenerator _jwtToken;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager,IJWTTokenGenerator jwtToken,RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtToken = jwtToken;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] LoginDTO user)
        {
            var userFromDb = await _userManager.FindByNameAsync(user.Username);
            if (userFromDb == null)
            {
                return BadRequest("There is no user with this username!");
            }      
            var result = await _signInManager.CheckPasswordSignInAsync(userFromDb, user.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest("Incorrect password!");
            }

            var roles = await _userManager.GetRolesAsync(userFromDb);
           
            return Ok(new
            {
                result = result,
                userName = userFromDb.UserName,
                email = userFromDb.Email,
                token = _jwtToken.GenerateToken(userFromDb,roles)
            });
        }
    }
}
