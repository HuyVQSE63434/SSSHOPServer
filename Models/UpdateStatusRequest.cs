using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Models
{
    public class UpdateStatusRequest
    {
        public int orderID { get; set; }
        public string status { get; set; }
    }
}
