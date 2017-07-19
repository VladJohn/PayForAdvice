using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class CategoryMapper
    {
        public static CategoryModel MapCategory (Category category)
        {
            return new CategoryModel { Description = category.Description, Id = category.Id, ImageUrl = category.ImageUrl, Name = category.Name };
        }

        public static Category MapCategoryDataModel (CategoryModel category)
        {
            return new Category { Id = category.Id, Name = category.Name, ImageUrl = category.ImageUrl, Description = category.Description };
        }
    }
}