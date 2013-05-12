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

            if (price != null)
            {
                return new PriceDto(price);

            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            
        }

        // PUT api/Prices/5
        public HttpResponseMessage PutPrice(int id, PriceDto priceDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState));
            }

            if (id != priceDto.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest));
            }

            Price priceEntity = priceDto.ToEntity();

            Location FromLocation = db.Locations.Find(priceEntity.LocationFromId);
            Location ToLocation = db.Locations.Find(priceEntity.LocationToId);

            
            db.Entry(FromLocation).State = EntityState.Detached;
            db.Entry(ToLocation).State = EntityState.Detached;
            db.Entry(priceEntity).State = EntityState.Modified;

            

            try
            {
                db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, priceDto);
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                //throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex));
            }

            
            //return priceDto;
        }

        // POST api/Prices
        public HttpResponseMessage PostPrice(PriceDto priceDto)
        {
            if (ModelState.IsValid)
            {
                Price price = priceDto.ToEntity();
                db.Prices.Add(price);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, priceDto);
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

            return Request.CreateResponse(HttpStatusCode.OK, price);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}