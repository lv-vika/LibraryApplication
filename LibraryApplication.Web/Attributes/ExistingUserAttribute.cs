using LibraryApplication.Data.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryApplication.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ExistingUserAttribute : TypeFilterAttribute
{
    public ExistingUserAttribute() : base(typeof(ExistingUserFilter))
    {
    }
}

public class ExistingUserFilter : IActionFilter
{
    private readonly IUserRepository userRepository;

    public ExistingUserFilter(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Check if the user exists based on your logic
        var userId = (int)(context.ActionArguments["id"] ?? 0);
        var userExists = userRepository.CheckIfUserExists(userId);
            
        if (!userExists)
        {
            context.Result = new NotFoundResult();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No action required after the action is executed
    }
}