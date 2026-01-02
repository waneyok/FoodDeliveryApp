using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sessionId = GetSessionId();
            var cartItems = _context.CartItems
                .Include(c => c.MenuItem)
                .Where(c => c.SessionId == sessionId)
                .ToList();

            decimal total = 0;
            foreach (var item in cartItems)
            {
                total += item.MenuItem.Price * item.Quantity;
            }

            ViewBag.CartTotal = total;
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int menuItemId, int quantity = 1)
        {
            var sessionId = GetSessionId();
            var cartItem = _context.CartItems
                .FirstOrDefault(c => c.SessionId == sessionId && c.MenuItemId == menuItemId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    SessionId = sessionId,
                    MenuItemId = menuItemId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            _context.SaveChanges();
            TempData["SuccessMessage"] = "Товар добавлен в корзину";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCart(int id, int quantity)
        {
            var cartItem = _context.CartItems.Find(id);
            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.Quantity = quantity;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cartItem = _context.CartItems.Find(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            var sessionId = GetSessionId();
            var cartItems = _context.CartItems
                .Where(c => c.SessionId == sessionId)
                .ToList();

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private string GetSessionId()
        {
            var sessionId = HttpContext.Session.GetString("SessionId");
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("SessionId", sessionId);
            }
            return sessionId;
        }
    }
}