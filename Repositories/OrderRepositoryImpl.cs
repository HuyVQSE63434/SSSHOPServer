using Microsoft.AspNetCore.Mvc;
using SSSHOPSERVER.Controllers;
using SSSHOPSERVER.EFModels;
using SSSHOPSERVER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSSHOPSERVER.Repositories
{
    public class OrderRepositoryImpl : IOrderRepository
    {
        private readonly ShoeShopDBContext context = new ShoeShopDBContext();
        public IActionResult createOrder(CreateOrderRequest request, int v)
        {
            User user = context.User.Where(s => s.UserId == v).FirstOrDefault();
            if (user == null) return new BadRequestResult();
            Order order = new Order();
            order.OrderDay = request.OrderDay;
            order.OrderTotal = request.OrderTotal;
            order.Status = request.Status;
            context.Order.Add(order);
            context.SaveChanges();
            foreach (var item in request.orderDetail)
            {
                OrderDetail detail = new OrderDetail();
                detail.OrderId = order.OrderId;
                detail.ProductId = item.ProductId;
                detail.TotalPrice = item.TotalPrice;
                detail.Quantity = item.Quantity;
                context.OrderDetail.Add(detail);
                context.SaveChanges();
            }
            return new OkObjectResult(order);
        }

        public IActionResult getAllOrderOfUser(int v)
        {
            List<Order> result = context.Order.Where(s => s.UserId == v).ToList();
            if(result !=null)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return new BadRequestResult();
            }
        }

        public IActionResult getOrderDetail(int orderID, int v)
        {
            Order order = context.Order.Where(s => s.UserId == v && s.OrderId == orderID).FirstOrDefault();
            if (order == null) return new BadRequestResult();
            List<OrderDetail> result = context.OrderDetail.
                Where(s => s.OrderId == orderID).ToList();
            if (result != null)
            {
                return new OkObjectResult(result);
            }
            else
            {
                return new BadRequestResult();
            }
        }

        public IActionResult updateOrder(updateRequest request, int v)
        {
            User user = context.User.Where(s => s.UserId == v).FirstOrDefault();
            if (user == null) return new BadRequestResult();
            Order order = context.Order.Where(s => s.OrderId == request.orderID && s.UserId == v).FirstOrDefault();
            if (order == null) return new BadRequestResult();
            order.OrderTotal = request.OrderTotal;
            order.Status = request.Status;
            context.Order.Add(order);
            context.SaveChanges();
            foreach (var item in request.orderDetail)
            {
                OrderDetail detail = context.OrderDetail.Where(s => s.ProductId == item.ProductId).FirstOrDefault();
                if(detail != null)
                {
                    detail.Quantity = item.Quantity;
                    detail.TotalPrice = item.TotalPrice;
                    context.SaveChanges();

                }
                else
                {
                    detail = new OrderDetail();
                    detail.OrderId = order.OrderId;
                    detail.ProductId = item.ProductId;
                    detail.TotalPrice = item.TotalPrice;
                    detail.Quantity = item.Quantity;
                    context.OrderDetail.Add(detail);
                    context.SaveChanges();
                }
            }
            return new OkObjectResult(order);
        }

        public IActionResult updateOrderStatus(UpdateStatusRequest request, int v)
        {
            Order result = context.Order.Where(s => s.UserId == v && s.OrderId == request.orderID).FirstOrDefault();
            if (result != null)
            {
                result.Status = request.status;
                context.SaveChanges();
                return new OkObjectResult(result);
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
