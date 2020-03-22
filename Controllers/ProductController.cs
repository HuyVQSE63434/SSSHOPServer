using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SSSHOPSERVER.EFModels;
using SSSHOPSERVER.Models;
using SSSHOPSERVER.Repositories;

namespace SSSHOPSERVER.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("api/product")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : Controller
    {
        protected int GetUserId()
        {
            try
            {
                return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet("get-all")]
        [Authorize(Roles = "ssshopshoeUser123")]
        public Object getAllProducts()
        {
            return repository.getAllProducts();
        }


        [HttpGet("get")]
        [Authorize(Roles = "ssshopshoeUser123")]
        public Object getProduct(int productID)
        {
            return repository.getProduct(productID);
        }

        [HttpPost("add")]
        [AllowAnonymous]
        public IActionResult addProduct([FromBody] ProductRequest request)
        {
            return repository.addProduct(request);
        }

    }
}