using System;
using System.Collections.Generic;

namespace SSSHOPSERVER.EFModels
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public long OrderDay { get; set; }
        public int UserId { get; set; }
        public long OrderTotal { get; set; }
        public string Status { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
