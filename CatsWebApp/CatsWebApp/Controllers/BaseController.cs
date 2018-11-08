namespace CatsWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class BaseController : Controller
    {
        public readonly ICatsService CatsService;

        public BaseController(ICatsService catsService)
        {
            this.CatsService = catsService;
        }
    }
}
