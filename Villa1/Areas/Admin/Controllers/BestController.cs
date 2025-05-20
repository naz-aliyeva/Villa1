using Microsoft.AspNetCore.Mvc;
using Villa1.Contexts;
using Villa1.Models;

namespace Villa1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BestController : Controller
    {
        private readonly AppDbContext _context;
        public BestController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Best> bests = _context.Bests.ToList();
            return View(bests);
        } 
        public IActionResult Detail(int id)
        {
            Best? best = _context.Bests.Find( id);
            if(best is null)
            {
                return BadRequest("Bu id-e uygun data tapilmadi");
            }
            return View(best);
        }

    }
}
