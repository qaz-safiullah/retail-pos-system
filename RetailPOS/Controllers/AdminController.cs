using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailPOS.Data;


[RoleAuthorize("Admin")]
    public class AdminController : Controller
{
    private readonly POSDBContext _context;

    public AdminController(POSDBContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        // Basic dashboard stats
        ViewBag.TotalProducts = _context.Products.Count();
        ViewBag.TotalOrders = _context.Orders.Count();
        ViewBag.TotalSales = _context.Orders.Sum(o => (decimal?)o.TotalAmount) ?? 0;

        ViewBag.LowStockProducts = _context.Products
            .Where(p => p.StockQuantity <= 5)
            .ToList();

        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public JsonResult RestockProduct([FromBody] RestockRequest request)
    {
        if (request == null || request.ProductId <= 0 || request.QuantityToAdd <= 0)
            return Json(new { success = false, message = "Invalid request" });

        var product = _context.Products.FirstOrDefault(p => p.ProductId == request.ProductId);
        if (product == null)
            return Json(new { success = false, message = "Product not found" });

        product.StockQuantity += request.QuantityToAdd;
        _context.SaveChanges();

        return Json(new { success = true, newStock = product.StockQuantity });
    }

    public class RestockRequest
    {
        public int ProductId { get; set; }
        public int QuantityToAdd { get; set; }
    }


}
