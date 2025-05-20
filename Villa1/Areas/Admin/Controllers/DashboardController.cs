using Microsoft.AspNetCore.Mvc;

namespace Villa1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
