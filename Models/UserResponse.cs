using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Models
{
    public class UserResponse
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public UserResponse(string username, string fullName, string email, string address)
        {
            Username = username;
            FullName = fullName;
            Email = email;
            Address = address;
        }
    }
}
