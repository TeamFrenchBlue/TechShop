namespace TechShop.WebApi.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Data.Context;
    using Data.Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Admin;
    using TechShop.Models;

    [Authorize(Roles = "Administrator")]
    [RoutePrefix("api/admin")]
    public class AdminController : BaseController
    {
        private ApplicationUserManager userManager;

        public AdminController(ITechShopData data)
            : base(data)
        {
            this.userManager = new ApplicationUserManager(
                new UserStore<User>(new TechShopDbContext()));
        }

        public ApplicationUserManager UserManager
        {
            get { return this.userManager; }
        }

        // PUT api/Admin/User/{username}
        [HttpPut]
        [Route("User/{username}")]
        public IHttpActionResult EditUserProfile(string username,
            AdminEditUserBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = this.Data.Users.All().FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return this.BadRequest("User not found: username = " + username);
            }

            user.Name = model.Name;
            user.Email = model.Email;

            if (model.IsAdmin.HasValue)
            {
                if (model.IsAdmin.Value)
                {
                    this.UserManager.AddToRole(user.Id, "Administrator");
                }
                else
                {
                    this.UserManager.RemoveFromRole(user.Id, "Administrator");
                }
            }

            this.Data.SaveChanges();

            return this.Ok(
                new
                {
                    message = "User " + user.UserName + " edited successfully.",
                }
            );
        }
    }
}