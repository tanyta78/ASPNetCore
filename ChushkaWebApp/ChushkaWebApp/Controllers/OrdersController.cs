namespace ChushkaWebApp.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;

    [Authorize("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IAccountService accountService;

        public OrdersController(IOrderService orderService, IProductService productService, IAccountService accountService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.accountService = accountService;
        }

        public IActionResult Index()
        {
            Order[] orders = this.orderService.GetAllOrders();
            return this.View(orders);
        }

        [AllowAnonymous]
        public IActionResult Order(string id)
        {
            var product = this.productService.GetProductById(int.Parse(id));
            var user = this.accountService.GetUser(this.User.Identity.Name);

            if (user == null)
            {
                return this.BadRequest("Invalid user id.");
            }

            if (product == null)
            {
                return this.BadRequest("Invalid product id.");
            }

            return this.orderService.OrderProduct(product, user);
        }
    }
}