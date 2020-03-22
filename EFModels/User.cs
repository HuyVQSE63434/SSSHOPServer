using System;
using System.Collections.Generic;

namespace SSSHOPSERVER.EFModels
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsRemoved { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
