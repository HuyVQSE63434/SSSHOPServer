using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Models
{
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
