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
    [RoutePrefix("api/Trades")]
    public class TradesController : BaseController
    {
        public TradesController(ITechShopData data)
            : base(data)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            var trade = this.Data.Trades.All()
                .OrderBy(c => c.Position)
                .ThenBy(c => c.Name)
                .Select(TradeBindingModel.FromTrade)
                .ToList();

            return this.Ok(trade);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetById(int id)
        {
            var trade = this.Data.Trades.All()
                .Where(c => c.Id == id)
                .Select(TradeBindingModel.FromTrade)
                .FirstOrDefault();

            this.CheckObjectForNull(trade, "trade", id);

            return this.Ok(trade);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetByCategoryName([FromUri]string name)
        {
            var trades = this.Data.Trades.All()
                .Where(c => c.Products.Any(p => p.Category.Name == name))
                .Select(TradeBindingModel.FromTrade);

            return this.Ok(trades);
        }

        [HttpPut]
        public IHttpActionResult Edit(int id, TradeBindingModel tradeModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (tradeModel == null)
            {
                return this.BadRequest("Invalid input parameters.");
            }

            var trade = this.Data.Trades.All()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            this.CheckObjectForNull(trade, "trade", id);

            trade.Name = tradeModel.Name;
            trade.Position = tradeModel.Position;


            this.Data.Trades.Update(trade);

            try
            {
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {

                return this.GetExceptionMessage(ex);
            }

            return Ok(string.Format("Trade with id {0} is changed successfully", id));
        }

        [HttpPost]
        public IHttpActionResult Add(TradeBindingModel tradeModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (tradeModel == null)
            {
                return this.BadRequest("Invalid input parameters.");
            }
            var trade = new Trade();
            trade.Name = tradeModel.Name;
            trade.Position = tradeModel.Position;

            try
            {
                this.Data.Trades.Add(trade);
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {

                return this.GetExceptionMessage(ex);
            }

            return this.Ok(string.Format("Trade name: {0} and id: {1} is created", trade.Name, trade.Id));
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var trade = this.Data.Trades.All()
                .Where(c => c.Id == id)
                .Select(TradeBindingModel.FromTrade)
                .FirstOrDefault();

            this.CheckObjectForNull(trade, "trade", id);

            this.Data.Trades.Delete(trade.Id);

            try
            {
                this.Data.SaveChanges();
            }
            catch (Exception ex)
            {

                return this.GetExceptionMessage(ex);
            }

            return this.Ok(trade);
        }
    }
}
