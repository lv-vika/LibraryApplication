using LibraryApplication.Data.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryApplication.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ExistingBookAttribute : TypeFilterAttribute
{
    public ExistingBookAttribute() : base(typeof(ExistingBookFilter))
    {
    }
}

public class ExistingBookFilter : IActionFilter
{
    private readonly IBookRepository bookRepository;

    public ExistingBookFilter(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Check if the user exists based on your logic
        var bookId = (int)(context.ActionArguments["id"] ?? 0);
        var bookExists = bookRepository.CheckIfBookExists(bookId);
            
        if (!bookExists)
        {
            context.Result = new NotFoundResult();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No action required after the action is executed
    }
}