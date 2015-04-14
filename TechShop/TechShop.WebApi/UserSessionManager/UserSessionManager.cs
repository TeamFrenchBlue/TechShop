namespace TechShop.WebApi.UserSessionManager
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Web;

    using Data.Data;
    using TechShop.Models;

    using Microsoft.AspNet.Identity;


    public class UserSessionManager
        : IUserSessionManager
    {
        public UserSessionManager(ITechShopData data)
        {
            this.Data = data;
        }

        private HttpRequestMessage CurrentRequest
        {
            get
            {
                return (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
            }
        }

        public ITechShopData Data { get; private set; }

        public void CreateUserSession(string username, string accessToken)
        {
            var user = this.Data.Users.All().First(u => u.UserName == username);
            UserSession userSession = new UserSession
            {
                UserId = user.Id,
                AccessToken = accessToken
            };
            
            userSession.ExpirationDate = DateTime.Now + Common.Constants.DefaultUserSessionTimeout;
            this.Data.UserSessions.Add(userSession);
            this.Data.SaveChanges();
        }

        public bool ValidateUserSession()
        {
            string authToken = this.GetAccessToken();
            var currentUserId = this.GetCurrentUserId();
            var userSession = this.Data.UserSessions.All().FirstOrDefault(session =>
                session.AccessToken == authToken && session.UserId == currentUserId);

            if (userSession == null)
            {
                return false;
            }

            if (userSession.ExpirationDate < DateTime.Now)
            {
                return false;
            }

            userSession.ExpirationDate = DateTime.Now + Common.Constants.DefaultUserSessionTimeout;
            this.Data.SaveChanges();

            return true;
        }

        public void RemoveUserSession()
        {
            string accessToken = GetAccessToken();
            var currentUserId = GetCurrentUserId();
            var userSession = this.Data.UserSessions.All().FirstOrDefault(session =>
                session.AccessToken == accessToken && session.UserId == currentUserId);
            if (userSession != null)
            {
                this.Data.UserSessions.Delete(userSession);
                this.Data.SaveChanges();
            }
        }

        public void RemoveExpiredSessions()
        {
            var expiredUsersSessions = this.Data.UserSessions.All().Where(
                session => session.ExpirationDate < DateTime.Now);

            this.Data.UserSessions.DeleteRange(expiredUsersSessions);
            this.Data.SaveChanges();
        }

        private string GetAccessToken()
        {
            string accessToken = null;
            if (CurrentRequest.Headers.Authorization != null)
            {
                if (CurrentRequest.Headers.Authorization.Scheme == "Bearer")
                {
                    accessToken = CurrentRequest.Headers.Authorization.Parameter;
                }
            }

            return accessToken;
        }

        private string GetCurrentUserId()
        {
            if (HttpContext.Current.User == null)
            {
                return null;
            }

            string userId = HttpContext.Current.User.Identity.GetUserId();
            return userId;
        }
    }
}