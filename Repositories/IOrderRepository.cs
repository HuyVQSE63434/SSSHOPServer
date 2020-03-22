using Microsoft.AspNetCore.Mvc;
using SSSHOPSERVER.Controllers;
using SSSHOPSERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Repositories
{
    public interface IOrderRepository
    {
        IActionResult getAllOrderOfUser(int v);
        IActionResult getOrderDetail(int orderID, int v);
        IActionResult createOrder(CreateOrderRequest request, int v);
        IActionResult updateOrderStatus(UpdateStatusRequest request, int v);
        IActionResult updateOrder(updateRequest request, int v);
    }
}
