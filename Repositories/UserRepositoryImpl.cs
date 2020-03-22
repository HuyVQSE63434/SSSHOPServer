using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SSSHOPSERVER.EFModels;
using SSSHOPSERVER.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Repositories
{
    public class UserRepositoryImpl : IUSerRepository
    {

        private readonly IConfiguration _configuration;

        public UserRepositoryImpl(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private ShoeShopDBContext context = new ShoeShopDBContext();

        public object createUser(CreateUSerRequest request)
        {

            try
            {
                User user = new User();
                user.Email = request.email;
                user.FullName = request.fullName;
                user.Address = request.address;
                user.Username = request.username;
                user.Password = Hashing.HashPassword(request.password);
                user.IsRemoved = false;
                context.User.Add(user);
                context.SaveChanges();
                User temp = context.User
                    .Where(s => s.Username == user.Username)
                    .FirstOrDefault();
                return new
                {
                    access_token = GenerateJwtToken(user.Email, user, "ssshopshoeUser123"),
                    expires_in = (int)TimeSpan.FromDays(30).TotalSeconds,
                    user = new
                    {
                        user_id = user.UserId,
                        full_name = user.FullName,
                        email = user.Email,
                        address = user.Address
                    }

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Object getUserByUsernameAndPassword(LoginRequest request)
        {

            using (context = new ShoeShopDBContext())
            {
                var user = context.User
                    .Where(s => s.Username == request.username)
                    .Where(s => s.IsRemoved == false)
                    .FirstOrDefault();
                if (user != null)
                {
                    return Hashing.ValidatePassword(request.password, user.Password) ?
                         new
                         {
                             access_token = GenerateJwtToken(user.Email,user, "ssshopshoeUser123"),
                             expires_in = (int)TimeSpan.FromDays(30).TotalSeconds,
                             user = new
                             {
                                 user_id = user.UserId,
                                 full_name = user.FullName,
                                 email = user.Email,
                                 address = user.Address
                             }
                         } : null;
                }
                return null;
            }
        }


        public bool deleteUser(int userId)
        {
            User webOwner = context.User.Where(s => s.UserId == userId)
                .FirstOrDefault();
            try
            {
                webOwner.IsRemoved = true;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool updateUser(UpdateUserRequest request, int userId)
        {
            User webOwner = context.User.Where(s => s.UserId == userId)
               .FirstOrDefault();
            if (webOwner == null) return false;
            webOwner.FullName = request.FullName;
            webOwner.Email = request.Email;
            webOwner.Address = request.Address;
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public object GenerateJwtToken(string email, User user, string Role)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["Auth0:ApiIdentifier"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
