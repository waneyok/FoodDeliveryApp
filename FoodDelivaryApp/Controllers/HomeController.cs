using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var restaurants = _context.Restaurants.ToList();
            return View(restaurants);
        }

        public IActionResult Restaurants()
        {
            var restaurants = _context.Restaurants.ToList();
            return View(restaurants);
        }

        public IActionResult Menu(int id)
        {
            var restaurant = _context.Restaurants
                .Include(r => r.MenuItems)
                .ThenInclude(m => m.Category)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        public IActionResult Categories()
        {
            var categories = _context.Categories
                .Include(c => c.MenuItems)
                .ToList();
            return View(categories);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}