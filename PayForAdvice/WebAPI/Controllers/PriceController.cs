using System;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Services;
using WebAPI.ValidatorsModel;

namespace WebAPI.Controllers
{
    public class PriceController : ApiController
    {
        //update the price for a user
        public IHttpActionResult PutPrice(PriceModel price)
        {
            var service = new PriceService();
            PriceModel updatePrice;
            try
            {
                updatePrice = service.UpdatePrice(price);
            }
            catch (ModelException exception)
            {
                return BadRequest(exception.Message);
            }
            if (updatePrice == null)
                return NotFound();
            return Ok(updatePrice);
        }

        //add a new price for a user 
        public IHttpActionResult PostPrice(PriceModel price)
        {
            var service = new PriceService();
            PriceModel addPrice;
            try
            {
                addPrice = service.AddNewPrice(price);
            }
            catch (ModelException exception)
            {
                return BadRequest(exception.Message);
            }
            if (addPrice == null)
                return BadRequest();
            return Ok(addPrice);
        }

        //get all prices for a user with the id = userId
        //if there are no prices it returns NotFound()
        public IHttpActionResult GetAllPricesForUsers(int userId)
        {
            var service = new PriceService();
            var prices = service.GetAllPricesByUser(userId);
            if (prices == null)
                return NotFound();
            return Ok(prices);
        }
    }
}