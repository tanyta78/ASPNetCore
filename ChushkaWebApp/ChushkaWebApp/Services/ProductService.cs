namespace ChushkaWebApp.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Models.Products;

    public class ProductService : PageModel, IProductService
    {
        private readonly ApplicationDbContext db;

        public ProductService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult CreateProduct(ProductViewModel model)
        {
            //if (!Enum.TryParse(model.ProductType, out ProductType type))
            //{
            //   return this.View("_Error", new ErrorViewModel { Description = "Invalid type" });
            //}

            var product = new Product
            {
                Description = model.Description,
                Name = model.Name,
                Price = model.Price,
                Type = (ProductType)Enum.Parse(typeof(ProductType), model.ProductType),
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();

            return this.Redirect($"/products/details?id={product.Id}");
        }

        public IActionResult DeleteProduct(string id)
        {
            var product = this.db.Products.FirstOrDefault(p => p.Id == int.Parse(id));
            if (product == null) return this.Redirect("/");
            //this.db.Products.Remove(product).State = EntityState.Deleted;

            //todo: when trying to delete ordered product an exception has been thrown. Make soft delete. product.IsDeleted = true;
            this.db.Products.Remove(product);

            this.db.SaveChanges();

            return this.Redirect("/");
        }

        public IActionResult EditProduct(ProductViewModel model)
        {
            var product = this.db.Products.FirstOrDefault(p => p.Id == model.Id);

            if (product == null) return this.Redirect("/");
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Type = (ProductType) Enum.Parse(typeof(ProductType), model.ProductType);

            this.db.Entry(product).State = EntityState.Modified;
            this.db.SaveChanges();

            return this.Redirect("/");
        }

        public ProductsListViewModel GetAllProducts(string username)
        {
            var products = this.db.Products
                               .Select(x =>
                                   new ProductViewModel
                                   {
                                       Id = x.Id,
                                       Description = x.Description,
                                       Name = x.Name,
                                       Price = x.Price,
                                       ProductType = x.Type.ToString()
                                   })
                               .ToArray();

           var viewModel = new ProductsListViewModel()
            {
                Products = products,
            };

            return viewModel;
        }

        public Product GetProductById(int id)
        {
            var product = this.db.Products
                               .FirstOrDefault(x => x.Id == id);

            return product;
        }
    }
}
