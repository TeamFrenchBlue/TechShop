namespace TechShop.WebApi.Controllers
{
    using System.Web.Http;

    using Data.Data;

    using Microsoft.AspNet.Identity;

    public class BaseController : ApiController
    {
        public BaseController(ITechShopData data)
        {
            this.Data = data;
        }

        protected ITechShopData Data { get; private set; }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return this.BadRequest();
                }

                return this.BadRequest(this.ModelState);
            }

            return null;
        }
    }
}