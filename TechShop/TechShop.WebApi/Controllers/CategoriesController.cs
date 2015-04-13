namespace TechShop.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using TechShop.Data.Data;
    using TechShop.Models;
    using TechShop.WebApi.Models;

    [Authorize(Roles = "Administrator")]
    [RoutePrefix("api/Categories")]
    public class CategoriesController : BaseController
    {
        public CategoriesController(ITechShopData data)
            : base(data)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var categories = this.Data.Categories.All()
                .OrderBy(c => c.Position)
                .ThenBy(c => c.Name)
                .Select(CategoryBindingModel.FromCategory)
                .ToList();

            return this.Ok(categories);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetById(int id)
        {
            var category = this.Data.Categories.All()
                .Where(c => c.Id == id)
                .Select(CategoryBindingModel.FromCategory)
                .FirstOrDefault();

            this.CheckObjectForNull(category, "trade", id);

            return this.Ok(category);
        }

        [HttpPut]
        public IHttpActionResult Edit(int id, CategoryBindingModel categoryModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var category = this.Data.Categories.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            this.CheckObjectForNull(category, "trade", id);

            category.Name = categoryModel.Name;
            category.Position = category.Position;

            this.Data.Categories.Update(category);

            try
            {
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {

                return this.GetExceptionMessage(ex);
            }

            return Ok(string.Format("Category with id {0} is changed successfully", id));
        }

        [HttpPost]
        public IHttpActionResult Add(CategoryBindingModel categoryModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (categoryModel == null)
            {
                return this.BadRequest("Invalid input parameters.");
            }

            var category = new Category();

            category.Name = categoryModel.Name;
            category.Position = categoryModel.Position;

            this.Data.Categories.Add(category);

            try
            {
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {

                return this.GetExceptionMessage(ex);
            }

            return this.Ok(string.Format("Category name: {0} and id: {1} is created", category.Name, category.Id));
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var category = this.Data.Categories.All()
                .Where(c => c.Id == id)
                .Select(CategoryBindingModel.FromCategory)
                .FirstOrDefault();

            this.CheckObjectForNull(category, "trade", id);

            this.Data.Categories.Delete(category.Id);

            try
            {
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {

                return this.GetExceptionMessage(ex);
            }

            return this.Ok(category);
        }
    }
}
