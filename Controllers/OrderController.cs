using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SSSHOPSERVER.Models;
using SSSHOPSERVER.Repositories;

namespace SSSHOPSERVER.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("api/order")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : Controller
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


        IOrderRepository repository;

        public OrderController(IOrderRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("all")]
        [Authorize]
        public IActionResult getAllOrderOfUser()
        {
            return repository.getAllOrderOfUser(GetUserId());
        }


        [Authorize]
        [HttpGet("detail")]
        public IActionResult getOrderDetail(int orderID)
        {
            return repository.getOrderDetail(orderID, GetUserId());
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult createOrder([FromBody] CreateOrderRequest request)
        {
            return repository.createOrder(request, GetUserId());
        }

        [Authorize]
        [HttpPut("update-status")]
        public IActionResult updateStatusOrder([FromBody] UpdateStatusRequest request)
        {
            return repository.updateOrderStatus(request, GetUserId());
        }

        [Authorize]
        [HttpPut("update")]
        public IActionResult updateOrder([FromBody] updateRequest request)
        {
            return repository.updateOrder(request, GetUserId());
        }
    }
}