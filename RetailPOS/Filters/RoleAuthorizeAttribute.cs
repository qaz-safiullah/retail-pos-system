using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RoleAuthorizeAttribute : ActionFilterAttribute
{
    private readonly string _role;

    public RoleAuthorizeAttribute(string role)
    {
        _role = role;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var role = context.HttpContext.Session.GetString("Role");

        if (string.IsNullOrEmpty(role) || role != _role)
        {
            context.Result = new RedirectToActionResult(
                "Index", "Login", null);
        }

        base.OnActionExecuting(context);
    }
}
