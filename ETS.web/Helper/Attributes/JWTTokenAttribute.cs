using ETSystem.Model.LOGINREG;
using ETSystem.Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ETS.web.Helper.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JWTTokenAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var UserId = context.HttpContext.Items["UserId"];
            var StatusCode = context.HttpContext.Items["StatusCode"];
            var StatusMessage = context.HttpContext.Items["StatusMessage"];
            if (StatusCode == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            if(StatusCode != null)
            {
                var statuscode = (int)StatusCode;
                if(statuscode == 1)
                {
                    context.Result = new JsonResult(new { message = "Token Expired" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
