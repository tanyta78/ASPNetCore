namespace EventWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models;
    using Models.Account;
    using Services.Contracts;

    public class AccountController : Controller
    {

        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return this.View();
        }

        // POST: Account/Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var result = this.accountService.Login(model);
            if (result is PageResult)
            {
                result = this.View("_Error", new ErrorViewModel { Description = "Invalid login attempt" });
            }

            return result;
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return this.View();
        }

        // POST: Account/Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            var result = this.accountService.Register(model);
            if (result is PageResult)
            {
                result = this.View("_Error", new ErrorViewModel { Description = "Invalid registration attempt" });
            }

            return result;
        }

        public IActionResult Logout()
        {
            return this.accountService.Logout();
        }


    }
}