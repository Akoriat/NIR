using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NIR.Data;
using NIR.Models;
using System.Diagnostics;

namespace NIR.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly NIRContext _context;

        public HomeController(NIRContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Catalog()
        {
            var genres = _context.Genre.ToList();
            ViewBag.GenreList = new SelectList(genres, "Id", "NameGenre");
            return View();
        }
        public IActionResult GetGenreData(int genreid) 
        {
            var genres = _context.Book
                .Where(x => x.GenreId == genreid)
                .ToList();
            return PartialView("~/Views/PartialView/_TablePartialView.cshtml", genres);
        }
        public async Task<IActionResult> Bestseller()
        {
            return View(await _context.Book.ToListAsync());
        }
    }
}
