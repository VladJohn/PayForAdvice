using System.Web.Http;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        //get all the categories
        public IHttpActionResult Get()
        {
            var service = new CategoryService();
            var categories = service.GetAllCategories();
            if (categories.Count == 0)
                return NotFound();
            return Ok(categories);
        }
    }
}