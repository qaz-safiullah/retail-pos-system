using Microsoft.AspNetCore.Mvc;
using RetailPOS.Data;

[RoleAuthorize("Cashier")]
public class CashierController : Controller
{
    private readonly POSDBContext _context;

    public CashierController(POSDBContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        int cashierId = HttpContext.Session.GetInt32("UserId") ?? 0;

        ViewBag.TodayOrders = _context.Orders
            .Where(o => o.UserId == cashierId && o.OrderDate.Date == DateTime.Today)
            .Count();

        ViewBag.TodaySales = _context.Orders
            .Where(o => o.UserId == cashierId && o.OrderDate.Date == DateTime.Today)
            .Sum(o => (decimal?)o.TotalAmount) ?? 0;


        var lastOrders = _context.Orders
       .Where(o => o.UserId == cashierId)
       .OrderByDescending(o => o.OrderDate)
       .Take(5)
       .Select(o => new
       {
           o.OrderId,
           o.OrderDate,
           o.TotalAmount
       })
       .ToList();

        ViewBag.LastOrders = lastOrders;
        return View();
    }
}
