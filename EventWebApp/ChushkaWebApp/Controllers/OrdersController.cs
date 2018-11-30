namespace EventWebApp.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Orders;
    using Services.Contracts;

    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IAccountService accService;
        private readonly IEventService eventService;

        public OrdersController(IOrderService orderService, IAccountService accService, IEventService eventService)
        {
            this.orderService = orderService;
            this.accService = accService;
            this.eventService = eventService;
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

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All", "Events", model);
            }

            var availableTickets = this.eventService.GetEventById(model.EventId.ToString()).TotalTickets;

            if (availableTickets < model.TicketsCount)
            {
                this.ModelState.AddModelError("tickets","There are not enough tickets!");
                this.ViewBag.Error = "There are not enough tickets!";
                return this.RedirectToAction("All", "Events", model);
            }

            var user = this.accService.GetUser(this.User.Identity.Name);
            model.CustomerId = user.Id;
            model.Customer = user;
            return this.orderService.CreateOrder(model);
        }
    }
}