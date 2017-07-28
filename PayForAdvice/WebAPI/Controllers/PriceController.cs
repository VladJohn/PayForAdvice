using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    public class PriceController : ApiController
    {
        //update
        public IHttpActionResult Put(PriceModel price)
        {
            var service = new PriceService();
            var updatePrice = service.UpdatePrice(price);
            if (updatePrice == null)
                return NotFound();
            return Ok(updatePrice);
        }

        //add 
        public IHttpActionResult Post(PriceModel price)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new PriceService();
            var addPrice = service.Add(price);
            if (addPrice == null)
                return BadRequest();
            return Ok(addPrice);
        }

        //getAllByUser
        public IHttpActionResult GetAllPricesForUsers(int idUser)
        {
            var service = new PriceService();
            var prices = service.GetAllPricesByUser(idUser);
            if (prices == null)
                return NotFound();
            return Ok(prices);
        }
    }
}