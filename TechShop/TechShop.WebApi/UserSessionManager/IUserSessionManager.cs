namespace TechShop.WebApi.UserSessionManager
{
    using Data.Data;

    public interface IUserSessionManager
    {
        ITechShopData Data { get; }

        void CreateOrExtendUserSession(string username, string accessToken);

        void RemoveUserSession();

        void RemoveExpiredSessions();
    }
}