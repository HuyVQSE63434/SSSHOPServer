using SSSHOPSERVER.EFModels;
using SSSHOPSERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Repositories
{
    public interface IUSerRepository
    {
        Object getUserByUsernameAndPassword(LoginRequest request);
        object createUser(CreateUSerRequest request);
        bool deleteUser(int userId);
        bool updateUser(UpdateUserRequest request,int userId);
        Object GenerateJwtToken(string email, User user, string Role);
    }
}
