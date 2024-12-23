using ArcProxy.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ArcProxy.Web.Filters
{
    internal class RestExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            IActionResult? result = null;

            switch (context.Exception)
            {
                case NotFoundCoreException:
                    result = new NotFoundResult();
                    break;
                case ForbiddenCoreException:
                    result = new ForbidResult();
                    break;
            }

            if (result is not null)
            {
                context.Result = result;
                context.ExceptionHandled = true;
            }
        }
    }
}
