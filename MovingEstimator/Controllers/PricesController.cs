using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MovingEstimator.Models;

namespace MovingEstimator.Controllers
{
    public class PricesController : ApiController
    {
        private EstimateContext db = new EstimateContext();

        // GET api/Prices
        public IEnumerable<PriceDto> GetPrices()
        {
            var prices = db.Prices.Include(p => p.From).Include(p => p.To);
            return prices.AsEnumerable().Select(price => new PriceDto(price));
        }

        // GET api/Prices/5
        public PriceDto GetPrice(int id)
        {
            Price price = db.Prices.Find(id);
            if (price == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return new PriceDto(price);
        }

        // PUT api/Prices/5
        public HttpResponseMessage PutPrice(int id, PriceDto price)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != price.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Price priceEntity = price.ToEntity();
            Location fromLocationEntity = db.Locations.Find(price.LocationFromId);
            Location toLocationEntity = db.Locations.Find(price.LocationToId);

            db.Entry(fromLocationEntity).State = EntityState.Detached;
            db.Entry(toLocationEntity).State = EntityState.Detached;
            db.Entry(priceEntity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Prices
        public HttpResponseMessage PostPrice(PriceDto price)
        {
            if (ModelState.IsValid)
            {
                Price priceEntity = price.ToEntity();
                db.Prices.Add(priceEntity);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, price);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = price.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Prices/5
        public HttpResponseMessage DeletePrice(int id)
        {
            Price price = db.Prices.Find(id);
            if (price == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }


            db.Prices.Remove(price);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            price.From = db.Locations.Find(price.LocationFromId);
            price.To = db.Locations.Find(price.LocationToId);
            return Request.CreateResponse(HttpStatusCode.OK, new PriceDto(price));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}