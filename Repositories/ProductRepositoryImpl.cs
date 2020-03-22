using Microsoft.AspNetCore.Mvc;
using SSSHOPSERVER.EFModels;
using SSSHOPSERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Repositories
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private ShoeShopDBContext context = new ShoeShopDBContext();

        public IActionResult addProduct(ProductRequest  request)
        {
            try
            {
                Product product = new Product();
                product.ProductName = request.ProductName;
                product.ProductCategory = request.ProductCategory;
                product.ProductPrice = request.ProductPrice;
                product.ProductUnit = request.ProductUnit;
                product.ImageUrl = request.imageUrl;

                context.Product.Add(product);
                context.SaveChanges();
                return new OkObjectResult(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : " + ex.Message);
                return new BadRequestResult();
            }
        }

        public object getAllProducts()
        {
            return context.Product.ToList();
        }

        public object getProduct(int productID)
        {
            return context.Product.Where(s => s.ProductId == productID).FirstOrDefault();
        }
    }
}
