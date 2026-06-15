using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailPOS.Data;
using RetailPOS.Models;
using System.Data;

// [RoleAuthorize("Admin")]
public class OrdersController : Controller
{
    private readonly POSDBContext _context;

    public OrdersController(POSDBContext context)
    {
        _context = context;
    }

    // Orders List
    public IActionResult Index()
    {
        var orders = _context.Orders
            .Include(o => o.User)
            .OrderByDescending(o => o.OrderDate)
            .ToList();

        return View(orders);
    }

    public IActionResult Details(int id)
    {

        int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        string role = HttpContext.Session.GetString("Role") ?? "";

        var order = _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefault(o => o.OrderId == id);

        if (order == null)
            return NotFound();

        if (role == "Cashier" && order.UserId != userId)
            return Unauthorized();

        ViewBag.Role = HttpContext.Session.GetString("Role");


        return View(order);
    }

    public IActionResult MyOrders()
    {
        int cashierId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId")); // logged-in cashier
        var orders = _context.Orders
                             .Where(o => o.UserId == cashierId)
                             .OrderByDescending(o => o.OrderDate)
                             .Take(5)
                             .ToList();
        return View("Index", orders); // reuse Index.cshtml
    }


}
