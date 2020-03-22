using Microsoft.AspNetCore.Mvc;
using SSSHOPSERVER.EFModels;
using SSSHOPSERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Repositories
{
    public interface IProductRepository
    {
        object getAllProducts();
        object getProduct(int productID);
        IActionResult addProduct(ProductRequest request);
    }
}
