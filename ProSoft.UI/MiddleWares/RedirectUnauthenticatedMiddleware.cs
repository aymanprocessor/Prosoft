namespace ProSoft.UI.MiddleWares
{
    public class RedirectUnauthenticatedMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectUnauthenticatedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.Redirect("/Account/Login");
                return;
            }

            await _next(context);
        }
    }
}
//!context.Request.Path.StartsWithSegments("/Account/Login") &&
//                !context.Request.Path.StartsWithSegments("/Account/Register")
