namespace ChushkaWebApp.Services.Contracts
{
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;

    public interface IOrderService
    {
        Order[] GetAllOrders();

        IActionResult OrderProduct(Product product, ApplicationUser user);
    }
}
