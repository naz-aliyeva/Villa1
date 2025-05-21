using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Villa1.Contexts;
using Villa1.Models;
using Villa1.ViewModels;

namespace Villa1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BestController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;   
        public BestController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment= webHostEnvironment;
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
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BestCreateVM bestCreateVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            //File Upload 

            if (!bestCreateVM.ImgFile.ContentType.Contains("images"))
            {
                ModelState.AddModelError("ImgFile", "Gonderilen file img tipinde deyil");
                return View();
            }


            string fileName = Path.GetFileNameWithoutExtension(bestCreateVM.ImgFile.FileName);
            string extension = Path.GetExtension(bestCreateVM.ImgFile.FileName);
            string resultFIleName = fileName + Guid.NewGuid() + extension;
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,"assets","UploadImages",resultFIleName);
            using FileStream stream = new FileStream(uploadPath,FileMode.Create);
            bestCreateVM.ImgFile.CopyTo(stream);

         
            //Mapping
            Best best = new Best();
            best.CAtegory = bestCreateVM.CAtegory;
            best.Price = bestCreateVM.Price;
            best.Address= bestCreateVM.Address;
            best.ImgUrl = resultFIleName;

            _context.Bests.Add(best);   
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Update(int id)
        {
            Best? best = _context.Bests.Find(id);
            if (best is null)
            {
                return BadRequest("Bu id-e uygun data tapilmadi");
            }
            BestUpdateVM bestUpdateVM = new BestUpdateVM();
            bestUpdateVM.Id=best.Id;
            bestUpdateVM.ImgUrl=best.ImgUrl;
            bestUpdateVM.CAtegory = best.CAtegory;
            bestUpdateVM.Price = best.Price;
            bestUpdateVM.Address = best.Address;
            return View(bestUpdateVM);
        }

        [HttpPost]
        public IActionResult Update(int id,BestUpdateVM bestUpdateVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Best best = _context.Bests.AsNoTracking().SingleOrDefault(b=>b.Id==id);
            if(best is null)
            {
                return BadRequest("Id-e uygun data tapilmadi");
            }
            best.CAtegory = bestUpdateVM.CAtegory;
            best.Price = bestUpdateVM.Price;
            best.Address = bestUpdateVM.Address;
            //File Upload 

            if (bestUpdateVM.ImgFile is not null)
            {
                if (!bestUpdateVM.ImgFile.ContentType.Contains("images"))
                {
                    ModelState.AddModelError("ImgFile", "Gonderilen file img tipinde deyil");
                    return View();
                }


                string fileName = Path.GetFileNameWithoutExtension(bestUpdateVM.ImgFile.FileName);
                string extension = Path.GetExtension(bestUpdateVM.ImgFile.FileName);
                string resultFIleName = fileName + Guid.NewGuid() + extension;
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "UploadImages", resultFIleName);
                using FileStream stream = new FileStream(uploadPath, FileMode.Create);
                bestUpdateVM.ImgFile.CopyTo(stream);

                best.ImgUrl = resultFIleName;
            }
         


                _context.Bests.Update(best);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); 
        }
        #endregion

        #region Delete 
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Best? best = _context.Bests.Find(id);
            if (best is null)
            {
                return BadRequest("Bu id-e uygun data tapilmadi");

            }

            _context.Bests.Remove(best);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
