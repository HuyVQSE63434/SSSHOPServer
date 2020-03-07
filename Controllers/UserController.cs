using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SSSHOPSERVER.EFModels;
using SSSHOPSERVER.Models;
using SSSHOPSERVER.Repositories;

namespace SSSHOPSERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        IUSerRepository repository;
        [HttpPost("login")]
        public IActionResult login([FromBody] LoginRequest request)
        {
            repository = new UserRepositoryImpl();
            User user = repository.getUserByUsernameAndPassword(request);
            //just hard code here.  
            if (user != null)
            {

                var now = DateTime.UtcNow;

                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                };

                var signingKey = new SymmetricSecurityKey(Encoding.Default.GetBytes("SecretKeySSHOPSystems"));
                var jwt = new JwtSecurityToken(
                    issuer: "Iss",
                    audience: "Aud",
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromDays(30)),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var customerresponse = new
                {
                    user_id = user.UserId,
                    full_name = user.FullName,
                    email = user.Email,
                    address = user.Address
                };
                var responseJson = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)TimeSpan.FromDays(30).TotalSeconds,
                    customer = customerresponse
                };

                return Ok(responseJson);
            }
            else
            {
                return StatusCode(401);
            }
        }


        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] CreateUSerRequest request)
        {
            repository = new UserRepositoryImpl();
            var webOwner = repository.createUser(request);

            if (webOwner != null) return Ok(webOwner);
            return StatusCode(400);
        }

        [HttpDelete("delete")]
        [Authorize]
        public IActionResult deleteUser(int userId)
        {
            repository = new UserRepositoryImpl();
            bool result = repository.deleteUser(userId);
            if (result) return Ok();
            return BadRequest();
        }

        [HttpPut("udpate")]
        [Authorize]
        public IActionResult updateUser([FromBody] UpdateUserRequest request)
        {
            repository = new UserRepositoryImpl();
            bool result = repository.updateUser(request);
            if (result) return Ok();
            return BadRequest();
        }
    }
}