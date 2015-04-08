using Microsoft.Owin;

[assembly: OwinStartup(typeof(TechShop.WebApi.Startup))]

namespace TechShop.WebApi
{
    using System.Reflection;
    using System.Web.Http;

    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;
    using Owin;

    using Data.Context;
    using Data.Data;
    using UserSessionManager;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.UseNinjectMiddleware(this.CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
        }

        private IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            this.RegisterMappings(kernel);

            return kernel;
        }

        private void RegisterMappings(IKernel kernel)
        {
            kernel.Bind<ITechShopData>().To<TechShopData>();
            kernel.Bind<ITechShopDbContext>().To<TechShopDbContext>();
            kernel.Bind<IUserSessionManager>().To<UserSessionManager.UserSessionManager>();
        }
    }
}