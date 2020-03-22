using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SSSHOPSERVER.EFModels;
using SSSHOPSERVER.Models;
using SSSHOPSERVER.Repositories;

namespace SSSHOPSERVER.Controllers
{
    [ApiController]
    [Route("api/user")]
    [EnableCors]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {

        protected int GetUserId()
        {
            try
            {
                return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        IUSerRepository repository;

        public UserController(IUSerRepository repository)
        {
            this.repository = repository;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult login([FromBody] LoginRequest request)
        {
            //repository = new UserRepositoryImpl();
            var user = repository.getUserByUsernameAndPassword(request);
            //just hard code here.  
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(401);
            }
        }


        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] CreateUSerRequest request)
        {
            //repository = new UserRepositoryImpl();
            var webOwner = repository.createUser(request);

            if (webOwner != null) return Ok(webOwner);
            return StatusCode(400);
        }

        [HttpDelete("delete")]
        [Authorize]
        public IActionResult deleteUser()
        {
            //repository = new UserRepositoryImpl();
            bool result = repository.deleteUser(GetUserId());
            if (result) return Ok();
            return BadRequest();
        }

        [HttpPut("udpate")]
        [Authorize]
        public IActionResult updateUser([FromBody] UpdateUserRequest request)
        {
            //repository = new UserRepositoryImpl();
            bool result = repository.updateUser(request,GetUserId());
            if (result) return Ok();
            return BadRequest();
        }
    }
}