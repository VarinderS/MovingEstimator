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
    public class PriceController : ApiController
    {
        private EstimateContext db = new EstimateContext();
        // GET api/Price
        public IEnumerable<Price> GetPrices()
        {
            var prices = db.Prices.Include(p => p.Location);
            return prices.AsEnumerable();
        }

        // GET api/Price/5
        public Price GetPrice(int id)
        {
            Price price = db.Prices.Find(id);
            if (price == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return price;
        }

        // PUT api/Price/5
        public HttpResponseMessage PutPrice(int id, Price price)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != price.PriceId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(price).State = EntityState.Modified;

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

        // POST api/Price
        public HttpResponseMessage PostPrice(Price price)
        {
            if (ModelState.IsValid)
            {
                db.Prices.Add(price);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, price);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = price.PriceId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Price/5
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

            return Request.CreateResponse(HttpStatusCode.OK, price);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}