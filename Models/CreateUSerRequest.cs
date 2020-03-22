using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Models
{
    public class CreateUSerRequest
    {
        public string username  { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
        public string address { get; set; }
        public string email { get; set; }
    }
}
