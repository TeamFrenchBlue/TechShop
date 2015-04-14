namespace TechShop.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.Script.Serialization;

    using Data.Context;
    using Data.Data;
    using Microsoft.AspNet.Identity;
    using Models.Users;
    using TechShop.Models;
    using UserSessionManager;

    using Microsoft.AspNet.Identity.EntityFramework;

    [RoutePrefix("api/User")]
    public class UserController : BaseController
    {
        private ApplicationUserManager userManager;
        private IUserSessionManager userSessionManager;

        public UserController(ITechShopData data, IUserSessionManager userSessionManager) : base(data)
        {
            this.userManager = new ApplicationUserManager(
                new UserStore<User>(new TechShopDbContext()));
            this.userSessionManager = userSessionManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager;
            }
        }

        // POST api/User/Register
        [HttpPost]
        [AllowAnonymous]
        [ActionName("Register")]
        public async Task<HttpResponseMessage> RegisterUser(RegisterBindingModel userData)
        {
            if (!ModelState.IsValid)
            {
                return await this.BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            var user = new User
            {
                UserName = userData.Username,
                Email = userData.Email,
                Name = userData.Name
            };

            var identityResult = await this.UserManager.CreateAsync(user, userData.Password);

            if (!identityResult.Succeeded)
            {
                return await this.GetErrorResult(identityResult).ExecuteAsync(new CancellationToken());
            }

            var loginResult = this.LoginUser(new LoginBindingModel()
            {
                Username = userData.Username,
                Password = userData.Password
            });

            return await loginResult;
        }

        // POST api/User/Login
        [HttpPost]
        [AllowAnonymous]
        [ActionName("Login")]
        public async Task<HttpResponseMessage> LoginUser(LoginBindingModel loginData)
        {
            if (loginData == null)
            {
                loginData = new LoginBindingModel();
            }

            var request = HttpContext.Current.Request;
            var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) +
                request.ApplicationPath + Startup.TokenEndpointPath;
            using (var client = new HttpClient())
            {
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", loginData.Username),
                    new KeyValuePair<string, string>("password", loginData.Password)
                };
                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
                var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                var responseCode = tokenServiceResponse.StatusCode;

                if (responseCode == HttpStatusCode.OK)
                {
                    var jsSerializer = new JavaScriptSerializer();
                    var responseData =
                        jsSerializer.Deserialize<Dictionary<string, string>>(responseString);
                    var accessToken = responseData["access_token"];
                    var username = responseData["userName"];
                    this.userSessionManager.CreateUserSession(username, accessToken);

                    this.userSessionManager.RemoveExpiredSessions();
                }

                var responseMessage = new HttpResponseMessage(responseCode)
                {
                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
                };

                return responseMessage;
            }
        }

        [HttpPost]
        [SessionAuthorize]
        [Route("cart/add")]
        public IHttpActionResult AddProductToCart(AddProductToCartBindingModel cartItemData)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == cartItemData.Username);
            if (user == null)
            {
                return this.NotFound();
            }

            var product = this.Data.Products.All().FirstOrDefault(p => p.Id == cartItemData.ProductId);
            if (product == null)
            {
                return this.NotFound();
            }

            var newCartItem = new Cart
            {
                Product = product,
                User = user,
                Price = cartItemData.Price,
                State = this.Data.States.All().First(s => s.Name == "Waiting approval")
            };

            this.Data.Carts.Add(newCartItem);
            this.Data.SaveChanges();

            return this.Ok("New item added to cart successfully");
        }

        [HttpDelete]
        [SessionAuthorize]
        [Route("cart/remove/{id}")]
        public IHttpActionResult RemoveFromCart(int id)
        {
            var cartItem = this.Data.Carts.All().FirstOrDefault(c => c.Id == id);
            if (cartItem == null)
            {
                return this.NotFound();
            }

            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            if (cartItem.User.Id != currentUserId)
            {
                return this.BadRequest("This product does not belong to your cart.");
            }

            this.Data.Carts.Delete(cartItem);
            this.Data.SaveChanges();

            return this.Ok("Cart item removed successfully");
        }

        [HttpDelete]
        [SessionAuthorize]
        [Route("cart/products/remove/{productId}")]
        public IHttpActionResult RemoveFromCartByProductId(int productId)
        {
            var cartItem = this.Data.Carts.All().FirstOrDefault(c => c.Product.Id == productId);
            if (cartItem == null)
            {
                return this.NotFound();
            }

            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            if (cartItem.User.Id != currentUserId)
            {
                return this.BadRequest("This product does not belong to your cart.");
            }

            this.Data.Carts.Delete(cartItem);
            this.Data.SaveChanges();

            return this.Ok("Cart item removed successfully");
        }
    }
}