using Microsoft.AspNetCore.Mvc;
using RetailPOS.Data;
using RetailPOS.Models;


public class LoginController : Controller
{
    private readonly POSDBContext _context;

    public LoginController(POSDBContext context)
    {
        _context = context;
    }

    private string HashPassword(string password)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(sha.ComputeHash(bytes));
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string username, string password)
    {
        string hashedPassword = HashPassword(password);
        var user = _context.Users
            .FirstOrDefault(u => u.Username == username.Trim().ToLower() && u.PasswordHash == hashedPassword);

        if (user == null)
        {
            ViewBag.Error = "Invalid email or password";
            return View();
        }

        HttpContext.Session.SetInt32("UserId", user.UserId);
        HttpContext.Session.SetString("Role", user.Role);
        HttpContext.Session.SetString("UserName", user.Username);

        if (user.Role == "Admin")
            return RedirectToAction("Dashboard", "Admin");

        return RedirectToAction("Dashboard", "Cashier");


    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }
}
