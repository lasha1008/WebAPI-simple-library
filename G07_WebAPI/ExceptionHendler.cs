using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace G07_WebAPI;

public class ExceptionHendler : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Log.Error(context.Exception, "An unhandled exception occurred: {Exception}", context.Exception?.ToString());

        context.Result = new StatusCodeResult(500);
        context.ExceptionHandled = true;
    }
}
