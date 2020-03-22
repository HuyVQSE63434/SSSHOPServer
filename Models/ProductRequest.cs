using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Models
{
    public class ProductRequest
    {
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductUnit { get; set; }
        public long ProductPrice { get; set; }
        public string imageUrl { get; set; }
    }
}
