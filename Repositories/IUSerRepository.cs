using SSSHOPSERVER.EFModels;
using SSSHOPSERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Repositories
{
    interface IUSerRepository
    {
        User getUserByUsernameAndPassword(LoginRequest request);
        object createUser(CreateUSerRequest request);
        bool deleteUser(int userId);
        bool updateUser(UpdateUserRequest request);
    }
}
