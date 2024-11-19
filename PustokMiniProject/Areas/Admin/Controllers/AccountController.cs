using Microsoft.AspNetCore.Mvc;

namespace PustokMiniProject.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
