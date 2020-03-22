using SSSHOPSERVER.Models;
using System.Collections.Generic;

namespace SSSHOPSERVER.Controllers
{
    public class updateRequest
    {
        public int orderID { get; set; }
        public long OrderTotal { get; set; }
        public string Status { get; set; }
        public List<OrderDetailRequest> orderDetail { get; set; }
    }
}