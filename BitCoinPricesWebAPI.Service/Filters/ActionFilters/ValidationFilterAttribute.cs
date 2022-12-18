using BitCoinPricesWebAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace BitCoinPricesWebAPI.Service.Filters.ActionFilters;
public class ValidationFilterAttribute : IActionFilter
{
    private readonly ILoggerManagerService _loggerService;
    public ValidationFilterAttribute(ILoggerManagerService loggerService)
    {
        _loggerService = loggerService;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];
        var param = context.ActionArguments
            .SingleOrDefault(x => x.Value.ToString()
            .Contains("Dto")).Value;
        if (param is null)
        {
            _loggerService.LogError($"Object sent from client is null. Controller: {controller}, " +
                $"action: {action}");
            context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}");
            return;
        }
        if (!context.ModelState.IsValid)
        {
            _loggerService.LogError($"Invalid model state for the object. Controller: {controller}, action: {action}");
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }
    public void OnActionExecuted(ActionExecutedContext context) { }
}
