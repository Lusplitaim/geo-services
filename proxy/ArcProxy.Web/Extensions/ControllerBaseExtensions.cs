using Microsoft.AspNetCore.Mvc;

namespace ArcProxy.Web.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static void Forbidden(this ControllerBase controller)
        {
            controller.Response.StatusCode = 403;
        }
    }
}
