using Microsoft.AspNetCore.Mvc;
using Villa1.Contexts;
using Villa1.Models;

namespace Villa1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _AppDbContext;
        public HomeController(AppDbContext context)
        {
            _AppDbContext = context;    
        }

        public IActionResult Index()
        {

            List<Best> bests = _AppDbContext.Bests.ToList();
            return View(bests);
        }
    }
}
