namespace TechShop.WebApi.Controllers
{
    using System.Web.Http;

    using Data.Data;

    using Microsoft.AspNet.Identity;
    using System.Net.Http;

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

        /// <summary>
        /// Checks object for null reference
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <param name="objName">Name of the object, goes in exception message</param>
        /// <param name="objId">Id of the object, goes in exception message</param>
        /// <exception cref="HttpResponseException">Thrown when obj is null</exception>
        protected void CheckObjectForNull(object obj, string objName, int? objId)
        {
            if (obj == null)
            {
                string errorMessage = string.Empty;

                if (objId.HasValue)
                {
                    errorMessage = string.Format("There is no {0} with id {1}", objName, objId.Value);
                }
                else
                {
                    errorMessage = string.Format("There is no such {0}", objName);
                }

                throw new HttpResponseException(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Content = new StringContent(errorMessage)
                });
            }
        }
    }
}