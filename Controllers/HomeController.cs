using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Hotels.Controllers
{
    public class HomeController : Controller
    {

       
		private readonly ApplicationDbContext _context;
        List<Hotel> Hotels;
        

        public HomeController(ApplicationDbContext context)
        {
			_context = context;
            Hotels = new List<Hotel>();
		}
		public IActionResult CreateNewRecord(Hotel hotels)
		{
            if (ModelState.IsValid)
            {
				_context.hotel.Add(hotels);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
            var hotel = _context.hotel.ToList();
            return View("Index",hotel);
			
		}
        public IActionResult Delete(int id)
        {
            var hotledelete = _context.hotel.SingleOrDefault(x => x.id == id);
            _context.hotel.Remove(hotledelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var hoteledit=_context.hotel.SingleOrDefault(x=>x.id == id);
            return View(hoteledit);
        }

        public IActionResult Update(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.hotel.Update(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit");
                
        }
        [HttpPost]
		public IActionResult Index(string city)
		{
			var hotel = _context.hotel.Where(x=>x.City.Equals(city));
			return View(hotel);
		}
		public IActionResult Index()
        {
            var hotel= _context.hotel.ToList();
            return View(hotel);
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
    }
}