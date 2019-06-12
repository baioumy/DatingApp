using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repos;
        public AuthController(IAuthRepository repos)
        {
            _repos = repos;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto model)
        {
            model.UserName =  model.UserName.ToLower();
            if(await _repos.UserExists( model.UserName))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                UserName =  model.UserName
            };

            var createdUser = await _repos.Register(userToCreate ,  model.Password);
            
            return StatusCode(201);
           // return CreatedAtRoute()
        }
    }
}