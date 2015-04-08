namespace TechShop.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
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
                    this.userSessionManager.CreateOrExtendUserSession(username, accessToken);

                    this.userSessionManager.RemoveExpiredSessions();
                }

                var responseMessage = new HttpResponseMessage(responseCode)
                {
                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
                };

                return responseMessage;
            }
        }
    }
}