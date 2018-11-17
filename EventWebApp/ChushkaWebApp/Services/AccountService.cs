﻿namespace EventWebApp.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using Models.Account;

    public class AccountService : PageModel, IAccountService
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<RegisterViewModel> logger;

        public AccountService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, ILogger<RegisterViewModel> logger, SignInManager<ApplicationUser> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }


        public ApplicationUser GetUser(string username)
        {
            var user = this.db.Users.FirstOrDefault(x => x.UserName == username);
            return user;
        }

        public IActionResult Login(LoginViewModel model)
        {
            return this.LoginPostAsync(model).Result;
        }

        private async Task<IActionResult> LoginPostAsync(LoginViewModel model)
        {
            if (!this.ModelState.IsValid) return this.Page();
            var user = this.db.Users.FirstOrDefault(x => x.UserName == model.Username);
            if (user == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return this.Page();
            }

            var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                this.logger.LogInformation("User logged in.");
                return this.Redirect("/");
            }

            if (result.IsLockedOut)
            {
                this.logger.LogWarning("User account locked out.");
                return this.RedirectToPage("./Lockout");
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return this.Page();
            }
        }

        public IActionResult Logout()
        {
            return this.LogoutGetAsync().Result;
        }

        private async Task<IActionResult> LogoutGetAsync()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect("/");
        }

        public IActionResult Register(RegisterViewModel user)
        {
            return this.RegisterPostAsync(user).Result;
        }

        private async Task<IActionResult> RegisterPostAsync(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid) return this.Page();
            var user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UniqueCitizenNumber = model.UniqueCitizenNumber
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (this.db.Users.Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }

                this.logger.LogInformation("User created a new account with password.");

                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                await this.signInManager.SignInAsync(user, isPersistent: false);
                return this.Redirect("/");
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.Page();
        }
    }
}
