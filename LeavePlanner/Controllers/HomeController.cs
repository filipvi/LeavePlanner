using LeavePlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeavePlanner.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult LandPage()
        {
            return View();
        }

        public IActionResult GlobalError()
        {
            return View("ErrorAnnounced");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
