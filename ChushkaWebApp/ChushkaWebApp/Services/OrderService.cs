namespace ChushkaWebApp.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class OrderService : PageModel, IOrderService
    {
        private readonly ApplicationDbContext db;

        public OrderService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Order[] GetAllOrders()
        {
            var orders = this.db.Orders
                             .Include(o => o.User)
                             .Include(o => o.Product)
                             .ToArray();

            return orders;
        }

        public IActionResult OrderProduct(Product product, ApplicationUser user)
        {
            var order = new Order()
            {
                Product = product,
                ProductId = product.Id,
                User = user,
                UserId = user.Id,
                OrderedOn = DateTime.UtcNow
            };

            this.db.Orders.Add(order);
            this.db.SaveChanges();

            return this.Redirect("/");
        }
    }
}