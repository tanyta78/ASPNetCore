namespace EventWebApp.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models.Orders;

    [Authorize]
    public class OrderService : PageModel, IOrderService
    {
        private readonly ApplicationDbContext db;


        public OrderService(ApplicationDbContext db)
        {
            this.db = db;

        }


        public IActionResult CreateOrder(OrderViewModel model)
        {
            var custId = model.CustomerId;
            var order = new Order()
            {
                Id = new Guid(),
                CustomerId = custId,
                EventId = model.EventId,
                TicketsCount = model.TicketsCount,
                OrderedOn = DateTime.UtcNow
            };

            this.db.Orders.Add(order);
            this.db.SaveChanges();

            return this.Redirect("/Events/My");

        }


        public OrderListViewModel[] GetAllOrders()
        {
            var orders = this.db.Orders
                            .Select(x => new OrderListViewModel()
                            {
                                Customer = x.Customer.UserName,
                                Event = x.Event.Name,
                                OrderedOn = x.OrderedOn
                            }
              ).ToArray();

            return orders;
        }

        public Order GetOrderById(string id)
        {
            throw new System.NotImplementedException();
        }



        public IActionResult EditOrder(OrderViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public IActionResult DeleteOrder(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}

