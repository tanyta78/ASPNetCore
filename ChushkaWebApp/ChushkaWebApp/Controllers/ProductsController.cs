namespace ChushkaWebApp.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Products;
    using Services.Contracts;

    [Authorize("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            return this.productService.CreateProduct(model);
        }

        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            Product product = this.productService.GetProductById(int.Parse(id));
            if (product == null)
            {
                return this.BadRequest("Invalid product id");
            }

            return this.View(product);
        }

        public IActionResult Delete(string id)
        {
            Product product = this.productService.GetProductById(int.Parse(id));
            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                ProductType = product.Type.ToString()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(ProductViewModel model)
        {
            return this.productService.DeleteProduct(model.Id.ToString());
        }

        public IActionResult Edit(string id)
        {
            Product product = this.productService.GetProductById(int.Parse(id));
            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                ProductType = product.Type.ToString()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model)
        {
            return this.productService.EditProduct(model);
        }
    }
}