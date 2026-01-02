using FoodDeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout()
        {
            var sessionId = HttpContext.Session.GetString("SessionId");
            var cartItems = _context.CartItems
                .Include(c => c.MenuItem)
                .Where(c => c.SessionId == sessionId)
                .ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            decimal total = 0;
            foreach (var item in cartItems)
            {
                total += item.MenuItem.Price * item.Quantity;
            }

            ViewBag.CartItems = cartItems;
            ViewBag.TotalAmount = total;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                var sessionId = HttpContext.Session.GetString("SessionId");
                var cartItems = _context.CartItems
                    .Include(c => c.MenuItem)
                    .Where(c => c.SessionId == sessionId)
                    .ToList();

                if (!cartItems.Any())
                {
                    return RedirectToAction("Index", "Cart");
                }

                // Calculate total
                decimal total = 0;
                foreach (var cartItem in cartItems)
                {
                    total += cartItem.MenuItem.Price * cartItem.Quantity;
                }
                order.TotalAmount = total;

                // Save order
                _context.Orders.Add(order);
                _context.SaveChanges();

                // Save order items
                foreach (var cartItem in cartItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        MenuItemId = cartItem.MenuItemId,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.MenuItem.Price
                    };
                    _context.OrderItems.Add(orderItem);
                }

                // Clear cart
                _context.CartItems.RemoveRange(cartItems);
                _context.SaveChanges();

                // Clear session
                HttpContext.Session.Remove("SessionId");

                return RedirectToAction("Confirmation", new { id = order.Id });
            }

            return View(order);
        }

        public IActionResult Confirmation(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Track(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}