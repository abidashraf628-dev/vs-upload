using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isSignedUp = context.HttpContext.Session.GetString("IsSignedUp");
            if (isSignedUp != "true")
            {
                // Redirect to SignIn page if not logged in
                context.Result = new RedirectToActionResult("Index", "SignIn", null);
            }
        }
    }
}
