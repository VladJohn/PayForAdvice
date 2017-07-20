using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        public IHttpActionResult Get()
        {
            var service = new CategoryService();

            var categories = service.GetAllCategories();
            if (categories.Count == 0)
            {
                return NotFound();
            }
            return Ok(categories);
        }
    }
}