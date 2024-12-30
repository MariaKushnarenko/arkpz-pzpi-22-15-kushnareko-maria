using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreApp.DatabaseProvider.Models;
using StoreApp.Services.Interfaces;

namespace StoreApp.Actions
{
    public class AdminAuthorizationFilter : Attribute, IActionFilter
    {
       
        public void OnActionExecuting(ActionExecutingContext context)
        {
           var _userService = context.HttpContext.RequestServices.GetService<IUserService>();
            //Перевірка, чи присутній заголовок userId
            if (!context.HttpContext.Request.Headers.TryGetValue("userId", out var userIdHeader))
            {
                context.Result = new BadRequestObjectResult("UserId header is missing.");
                return;
            }

            //Спроба перетворити userId в int
            if (!int.TryParse(userIdHeader, out var userId))
            {
                context.Result = new BadRequestObjectResult("Invalid UserId.");
                return;
            }

            //Асинхронний виклик для отримання користувача
            var user = _userService.GetUserByIdAsync(userId).Result;
            if (user == null)
            {
                context.Result = new NotFoundObjectResult("User not found.");
                return;
            }

            //Перевірка ролі користувача
            if (user.Role != Role.Admin)
            {
                context.Result = new ForbidResult("Access denied. Admins only.");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }
    }
}
