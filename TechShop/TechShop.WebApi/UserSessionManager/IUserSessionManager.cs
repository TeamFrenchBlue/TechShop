namespace TechShop.WebApi.UserSessionManager
{
    using Data.Data;

    public interface IUserSessionManager
    {
        ITechShopData Data { get; }

        void CreateUserSession(string username, string accessToken);

        bool ValidateUserSession();

        void RemoveUserSession();

        void RemoveExpiredSessions();
    }
}