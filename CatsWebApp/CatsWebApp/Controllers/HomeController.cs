using System.Diagnostics;
using CatsWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatsWebApp.Controllers
{
    using Services;

    public class HomeController : BaseController
    {
        public HomeController(ICatsService catsService) : base(catsService)
        {
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                Cats = this.CatsService.GetAllCats()
            };

            return this.View(model);
        }

        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }


    }
}
