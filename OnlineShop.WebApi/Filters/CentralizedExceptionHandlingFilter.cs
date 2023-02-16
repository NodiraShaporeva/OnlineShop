using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineShop.Domain.Exceptions;
using OnlineShop.HttpModels.Response;

namespace OnlineShop.WebApi.Filters;

public class CentralizedExceptionHandlingFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var message = TryGetUserMessageFromException(context);
        if (message != null)
        {
            context.Result = new ObjectResult(new ErrorResponse(message));
            context.ExceptionHandled = true;
        }
    }

    private string? TryGetUserMessageFromException(ExceptionContext context)
    {
        return context.Exception switch
        {
            EmailNotFoundException => "Аккаунт с таким Email не найден",
            IncorrectPasswordException => "Неверный пароль",
            _ => null
        };
    }
}