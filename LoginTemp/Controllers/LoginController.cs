using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using mSystem;
using Services;
using static Services.AccountServices;

namespace LoginTemp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ServiceProvider AccountProvider;
        public LoginController()
        {
            this.AccountProvider = new ServiceCollection()
                                .AddScoped<IAccountAction, AccountService>()
                                .AddScoped<CoreContext>()
                                .AddDbContext<CoreContext>(options => options.UseSqlServer(SysBase.testConn1))
                                .BuildServiceProvider();
        }

        public IActionResult Login()
        {
            var x = User.Identity.IsAuthenticated;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CheckUser(string Account, string Pwd)
        {
            bool UserPass = this.AccountProvider.GetService<IAccountAction>().LoginCheck(Account,Pwd);
            if (UserPass)
            {
                var claims = new List<Claim>
                {
                    new Claim("user", ""),
                    new Claim("role", "Member")
                };

                HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "user", "role")));
            }
            return Redirect("../Home/Index");
        }
    }
}