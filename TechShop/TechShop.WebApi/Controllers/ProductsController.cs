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
    using TechShop.WebApi.Providers;

    [RoutePrefix("api/products")]
    public class ProductsController : BaseController
    {
        public ProductsController(ITechShopData data)
            : base(data)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAll([FromUri] ProductRequestBindingModel model)
        {
            if (model == null)
            {
                model = new ProductRequestBindingModel();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var products = this.Data.Products.All();

            if (model.Id.HasValue)
            {
                products = products.Where(p => p.Id == model.Id.Value);
            }

            if (model.CategoryName != null)
            {
                products = products.Where(p => p.Category.Name == model.CategoryName);
            }

            if (model.TradeName != null)
            {
                products = products.Where(p => p.Trade.Name == model.TradeName);
            }

            if (model.CategoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == model.CategoryId.Value);
            }

            if (model.TradeId.HasValue)
            {
                products = products.Where(p => p.TradeId == model.TradeId);
            }

            if (model.Name != null)
            {
                products = products.Where(p => p.Name.Contains(model.Name));
            }

            if (model.FromPrice.HasValue)
            {
                products = products.Where(p => p.Price >= model.FromPrice.Value);
            }

            if (model.ToPrice.HasValue)
            {
                products = products.Where(p => p.Price <= model.ToPrice.Value);
            }

            if (model.InPromotion.HasValue)
            {
                if (model.InPromotion.Value)
                {
                    products = products.Where(p => p.Promotions.Any());
                }
                else
                {
                    products = products.Where(p => !p.Promotions.Any());
                }

            }

            var productToRetutn = products
                .Select(ProductBindingModel.FromProduct)
                .AsEnumerable();

            var promotionDecorator = new ItemPromotionProvider(productToRetutn, this.Data);
            promotionDecorator.Decorate();

            return Ok(promotionDecorator.ResultSet);
        }

        [HttpPut]
        public IHttpActionResult Edit(int id, ProductBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (model == null)
            {
                return this.BadRequest("Invalid input parameters.");
            }

            var product = this.Data.Products.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            this.CheckObjectForNull(product, "product", id);

            product.Name = model.Name;
            product.ImageUrl = model.ImageUrl;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;
            product.TradeId = model.TradeId;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            try
            {
                this.Data.Products.Update(product);
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {

                return this.GetExceptionMessage(ex);
            }

            return Ok(string.Format("Product with id {0} is changed successfully", id));
        }

        [HttpPost]
        public IHttpActionResult Add(ProductBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (model == null)
            {
                return this.BadRequest("Invalid input parameters.");
            }
            var product = new Product();

            product.Name = model.Name;
            product.ImageUrl = model.ImageUrl;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;
            product.TradeId = model.TradeId;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            try
            {
                this.Data.Products.Add(product);
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {

                return this.GetExceptionMessage(ex);
            }

            return this.Ok(string.Format("Product name: {0} and id: {1} is created", product.Name, product.Id));
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var product = this.Data.Products.All()
                .Where(c => c.Id == id)
                .Select(ProductBindingModel.FromProduct)
                .FirstOrDefault();

            this.CheckObjectForNull(product, "product", id);

            this.Data.Trades.Delete(product.Id);

            try
            {
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {

                return this.GetExceptionMessage(ex);
            }

            return this.Ok(product);
        }
    }
}
