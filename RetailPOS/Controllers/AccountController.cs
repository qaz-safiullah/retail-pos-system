using Microsoft.AspNetCore.Mvc;
using RetailPOS.Data;
using RetailPOS.Models;

public class AccountController : Controller
{
    private readonly POSDBContext _context;

    public AccountController(POSDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password && u.IsActive);
        if (user != null)
        {
            // Store user role in session
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetInt32("UserId", user.UserId);

            if (user.Role == "Admin")
                return RedirectToAction("Dashboard", "Admin");
            else
                return RedirectToAction("Dashboard", "Cashier");
        }

        ViewBag.Error = "Invalid username or password!";
        return View();
    }

    public IActionResult RedirectDashboard()
    {
        var role = HttpContext.Session.GetString("Role");

        if (string.IsNullOrEmpty(role))
        {
            return RedirectToAction("Login");
        }

        if (role == "Admin")
        {
            return RedirectToAction("Dashboard", "Admin");
        }

        if (role == "Cashier")
        {
            return RedirectToAction("Dashboard", "Cashier");
        }

        return RedirectToAction("Login");
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
