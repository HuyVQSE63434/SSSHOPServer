using System;
using System.Collections.Generic;

namespace SSSHOPSERVER.EFModels
{
    public partial class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductUnit { get; set; }
        public long ProductPrice { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
