using SSSHOPSERVER.EFModels;
using SSSHOPSERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Repositories
{
    public class UserRepositoryImpl : IUSerRepository
    {
        private ShoeShopDBContext context;

        public object createUser(CreateUSerRequest request)
        {
            User user = new User();
            user.Email = request.Email;
            user.FullName = request.FullName;
            user.Address = request.Address;
            user.Username = request.Username;
            user.Password = Hashing.HashPassword(request.Password);
            user.IsRemoved = false;
            try
            {
                context.User.Add(user);
                context.SaveChanges();
                User temp = context.User
                    .Where(s => s.Username == user.Username)
                    .FirstOrDefault();
                return new
                {
                    webOwner = new UserResponse(temp.Username, temp.FullName, temp.Email, temp.Address)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public User getUserByUsernameAndPassword(LoginRequest request)
        {
            
            using (context = new ShoeShopDBContext())
            {
                var user = context.User
                    .Where(s => s.Username == request.Username)
                    .Where(s => s.IsRemoved == false)
                    .FirstOrDefault();
                if (user != null)
                {
                    return Hashing.ValidatePassword(request.Password, user.Password) ? user : null;
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

        public bool updateUser(UpdateUserRequest request)
        {
            User webOwner = context.User.Where(s => s.UserId == request.UserId)
               .FirstOrDefault();
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
    }
}
