using Microsoft.AspNet.Mvc.Filters;
using Serilog;
public class GlobalExceptionFilter : ActionFilterAttribute, IExceptionFilter
{
    ILogger _logger;
    public GlobalExceptionFilter(ILogger logger){
        _logger = logger;
    }
   public void OnException(ExceptionContext context)
   {
       _logger.Error(context.Exception,context.Exception.Message);
   }
}