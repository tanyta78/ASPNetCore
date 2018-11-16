namespace ChushkaWebApp.Services.Contracts
{
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.Products;

    public interface IProductService
    {
        ProductsListViewModel GetAllProducts(string username);

        Product GetProductById(int id);

        IActionResult CreateProduct(ProductViewModel model);

        IActionResult EditProduct(ProductViewModel model);

        IActionResult DeleteProduct(string id);

    }
}
