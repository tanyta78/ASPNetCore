namespace ChushkaWebApp.Services.Contracts
{
    using Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models.Account;

    public interface IAccountService
    {
        IActionResult Register(RegisterViewModel user);

        IActionResult Login(LoginViewModel model);

        IActionResult Logout();

        ApplicationUser GetUser(string username);
    }
}
