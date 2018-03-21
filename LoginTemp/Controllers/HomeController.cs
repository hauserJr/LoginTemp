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

namespace LoginTemp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServiceProvider provider;
        private readonly CoreContext db;
        public HomeController(CoreContext _db)
        {
            this.provider = new ServiceCollection()
                                .AddScoped<IDBAction<DBRepo>, DBService<DBRepo>>()
                                .AddScoped<CoreContext>()
                                .AddDbContext<CoreContext>(options => options.UseSqlServer(SysBase.testConn1))
                                .BuildServiceProvider();
            this.db = _db;
        }
        [Authorize]
        public IActionResult Index()
        {
            var x = this.db.UserAccount.Select(o => o).ToList();
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
