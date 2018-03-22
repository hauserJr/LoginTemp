using System.Collections.Generic;
using System.Security.Claims;
using DB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using mSystem;
using Services;
using static Services.AccountServices;
using static Services.DBServices;

namespace LoginTemp.Controllers
{
    public class LoginController : Controller
    {
        #region controller constructor
        private readonly ServiceProvider AccountProvider;
        private readonly ServiceProvider DBProvider;
        private readonly CoreContext db;
        public LoginController(CoreContext _db)
        {
            this.AccountProvider = new ServiceCollection()
                                .AddScoped<IAccountAction, AccountService>()
                                .AddScoped<CoreContext>()
                                .AddDbContext<CoreContext>(options => options.UseSqlServer(SysBase.testConn2))
                                .BuildServiceProvider();

            this.DBProvider = new ServiceCollection()
                                .AddScoped<IDBAction<DBRepo>, DBService<DBRepo>>()
                                .AddScoped<CoreContext>()
                                .AddDbContext<CoreContext>(options => options.UseSqlServer(SysBase.testConn2))
                                .BuildServiceProvider();
            this.db = _db;
        }
        #endregion

        public IActionResult Login()
        {
            //UserAccount _ua = new UserAccount()
            //{
            //    Account = "fA@gmail.com"
            //    ,Pwd = "1234"
            //};
            
            //DBProvider.GetService<IDBAction<DBRepo>>().InsertData(_ua);

          // DBProvider.GetService<IDBAction<UserAccount>>().GetAllData(new UserAccount());

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CheckUser(string Account, string Pwd)
        {
            //驗證使用者帳戶
            bool UserPass = this.AccountProvider.GetService<IAccountAction>().LoginCheck(Account,Pwd);
            if (UserPass)
            {
                //賦予Claim,可自訂
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, ""),
                    new Claim(ClaimTypes.Role, ""),
                    new Claim("Account",Account),
                    new Claim("AccountPass",UserPass.ToString())
                };
                //注入Cookie
                HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, SysBase.cookieName, ClaimTypes.Name, ClaimTypes.Role)));
            }
            return Redirect("../Home/Index");
        }
    }
}