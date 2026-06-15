using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailPOS.Data;
using RetailPOS.Models;

namespace RetailPOS.Controllers
{
    [RoleAuthorize("Cashier")]
    public class POSController : Controller
    {
        private readonly POSDBContext _context;

        public POSController(POSDBContext context)
        {
            _context = context;
        }

        public class PlaceOrderRequest
        {
            public List<CartItem> Cart { get; set; } = new List<CartItem>();
            public string PaymentMethod { get; set; } = "Cash";
            public decimal AmountReceived { get; set; } = 0;
        }

        //  POS Main Screen
        public IActionResult Index()
        {
            return View();
        }

        //  Product Search (Name or Barcode)
        [HttpGet]
        public IActionResult Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new List<object>());

            var products = _context.Products
                .Where(p => p.IsActive &&
                           (p.Name.Contains(term) || p.Barcode.Contains(term)))
                .Select(p => new
                {
                    p.ProductId,
                    p.Name,
                    p.Price,
                    p.StockQuantity
                })
                .Take(10)
                .ToList();

            return Json(products);
        }

        //  Place Order
        [HttpPost]
        public IActionResult PlaceOrder([FromBody] PlaceOrderRequest request)
        {
            if (request == null || request.Cart == null || !request.Cart.Any())
                return Json(new { success = false, message = "Cart is empty" });

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var totalAmount = request.Cart.Sum(i => i.Price * i.Quantity);

                if (request.PaymentMethod == "Cash" && request.AmountReceived < totalAmount)
                    return Json(new { success = false, message = "Amount received is less than total" });

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    PaymentMethod = request.PaymentMethod,
                    UserId = HttpContext.Session.GetInt32("UserId") ?? 0,

                    // ✅ SAVE CASH DATA IN DATABASE
                    AmountReceived = request.PaymentMethod == "Cash" ? request.AmountReceived : null,
                    ChangeAmount = request.PaymentMethod == "Cash"
                        ? request.AmountReceived - totalAmount
                        : null
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (var item in request.Cart)
                {
                    var product = _context.Products.First(p => p.ProductId == item.ProductId);

                    if (product.StockQuantity < item.Quantity)
                        throw new Exception($"Insufficient stock for {product.Name}");

                    product.StockQuantity -= item.Quantity;

                    _context.OrderItems.Add(new OrderItem
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.Price,
                        SubTotal = item.Price * item.Quantity
                    });
                }

                _context.SaveChanges();
                transaction.Commit();

                return Json(new
                {
                    success = true,
                    receiptUrl = Url.Action("Receipt", "POS", new { orderId = order.OrderId })
                });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return Json(new { success = false, message = ex.Message });
            }
        }


        public IActionResult Receipt(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
                return NotFound();

            return View(order);
        }



    }
}
