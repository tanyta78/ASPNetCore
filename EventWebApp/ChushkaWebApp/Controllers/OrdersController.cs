namespace EventWebApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Orders;
    using Services.Contracts;

    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IAccountService accService;

        public OrdersController(IOrderService orderService, IAccountService accService)
        {
            this.orderService = orderService;
            this.accService = accService;
        }

        // GET: Orders
        [Authorize("Admin")]
        public ActionResult All()
        {
            var orders = this.orderService.GetAllOrders();

            return this.View(orders);
        }


        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(OrderViewModel model)
        {

            if (this.ModelState.IsValid)
            {
                var user = this.accService.GetUser(this.User.Identity.Name);
                model.CustomerId = user.Id;
                model.Customer = user;
                return this.orderService.CreateOrder(model);
            }

            return this.RedirectToPage("/Events/All");

        }


    }
}