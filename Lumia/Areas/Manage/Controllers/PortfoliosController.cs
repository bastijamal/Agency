using Lumia.DAL;
using Lumia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Agency.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class PortfoliosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public PortfoliosController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var portfolios = _context.portfolios.ToList();
            return View(portfolios);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Portfolios portfolios)
        {
          

            if (!portfolios.ImgFile.ContentType.Contains("image/")) return View(portfolios);

            string path = _hostEnvironment.WebRootPath + @"/upload";
            string filename = Guid.NewGuid().ToString() + portfolios.ImgFile.FileName;

            using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                await portfolios.ImgFile.CopyToAsync(stream);
            }
            portfolios.PhotoUrl = filename;
            _context.portfolios.Add(portfolios);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            var port = _context.portfolios.FirstOrDefault(x => x.Id == id);
            _context.portfolios.Remove(port);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Update(int id)
        {

            var portfolios = _context.portfolios.FirstOrDefault(x => x.Id == id);

            if (portfolios == null)
            {
                return NotFound();
            }
            return View(portfolios);
        }


        [HttpPost]
        public IActionResult Update(Portfolios portfolios)
        {
            if (!ModelState.IsValid && portfolios.ImgFile != null)
            {
                return View();
            }



            return View();
        }

    }


}

