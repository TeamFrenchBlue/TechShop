namespace TechShop.WebApi.UserSessionManager
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    using Data.Context;
    using Data.Data;

    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected ITechShopData Data { get; private set; }

        public SessionAuthorizeAttribute(ITechShopData data)
        {
            this.Data = data;
        }

        public SessionAuthorizeAttribute() : this(new TechShopData(new TechShopDbContext()))
        {
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext))
            {
                return;
            }

            var userSessionManager = new UserSessionManager(new TechShopData(new TechShopDbContext()));
            if (userSessionManager.ValidateUserSession())
            {
                base.OnAuthorization(actionContext);
            }
            else
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
                    HttpStatusCode.Unauthorized, "Access token expried or not valid.");
            }
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}