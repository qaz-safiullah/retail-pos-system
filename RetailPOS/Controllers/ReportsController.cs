using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailPOS.Data;
using System.Linq;


[RoleAuthorize("Admin")]
public class ReportsController : Controller
{
    private readonly POSDBContext _context;

    public ReportsController(POSDBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Daily()
    {
        var today = DateTime.Today;

        var report = _context.Orders
            .Where(o => o.OrderDate.Date == today)
            .GroupBy(o => o.OrderDate.Date)
            .Select(g => new
            {
                Date = g.Key,
                TotalOrders = g.Count(),
                TotalRevenue = g.Sum(o => o.TotalAmount)
            })
            .FirstOrDefault();

        return View(report);
    }


    public IActionResult Monthly()
    {
        var report = _context.Orders
            .AsEnumerable() // 🔥 IMPORTANT FIX
            .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
            .Select(g => new
            {
                Month = $"{g.Key.Month:D2}-{g.Key.Year}",
                TotalOrders = g.Count(),
                TotalRevenue = g.Sum(o => o.TotalAmount)
            })
            .OrderByDescending(x => x.Month)
            .ToList();

        return View(report);
    }



    public IActionResult TopProducts()
    {
        var report = _context.OrderItems
            .Include(oi => oi.Product)
            .GroupBy(oi => oi.Product.Name)
            .Select(g => new
            {
                Product = g.Key,
                QuantitySold = g.Sum(x => x.Quantity),
                Revenue = g.Sum(x => x.SubTotal)
            })
            .OrderByDescending(x => x.QuantitySold)
            .Take(10)
            .ToList();

        return View(report);
    }

}
