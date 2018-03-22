using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginTemp.Models;
using Microsoft.Extensions.DependencyInjection;
using DB;
using static Services.DBServices;
using Services;
using mSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using static Services.IdentityServices;
using System.Security.Principal;
using System.Security.Claims;

namespace LoginTemp.Controllers
{
    public class HomeController : Controller
    {
        #region controller constructor
        private readonly ServiceProvider IidentityProvider;
        private readonly CoreContext db;
        public HomeController(CoreContext _db)
        {
            this.IidentityProvider = new ServiceCollection()
                                .AddScoped<IIdentityAction, IdentityService>()
                                .BuildServiceProvider();
            this.db = _db;
        }
        #endregion

        [Authorize]
        public IActionResult Index()
        {
            //取得使用者Identity Claims Value
            var GetUserAccount = this.IidentityProvider.GetService<IIdentityAction>().GetClaim(this.User.Identity);
            
            return View(GetUserAccount);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
