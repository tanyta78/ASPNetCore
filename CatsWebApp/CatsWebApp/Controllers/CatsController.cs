using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatsWebApp.Controllers
{
    using System;
    using Data;
    using Services;

    public class CatsController : BaseController
    {
       public CatsController(ICatsService catsService) : base(catsService)
        {
        }

        // GET: Cats/Details/5
        [HttpGet("/cats/{id}")]
        public ActionResult Details(int id)
        {
            var cat = this.CatsService.GetCatById(id);

            return this.View(cat);
        }

        // GET: Cats/Add
        [HttpGet("cats/add")]
        public ActionResult Add()
        {
            return this.View();
        }

        // POST: Cats/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Cat cat)
        {
            this.CatsService.AddCat(cat);
           
            return this.Redirect("/");
        }

    }
}