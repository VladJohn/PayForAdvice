using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;
using WebAPI.Mappings;

namespace WebAPI.Service
{
    public class CategoryService
    {
        public CategoryModel Add(CategoryModel newCategory)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<Category>();
                repo.Add(CategoryMapper.MapCategoryDataModel(newCategory));
                uw.Save();
            }
            return newCategory;
        }

        public List<CategoryModel> GetAllCategories()
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<Category>();
                var categories = new List<CategoryModel>();
                foreach (var element in repo.GetAll())
                {
                    var categoryElement = CategoryMapper.MapCategory(element);
                    categories.Add(categoryElement);
                }
                return categories;
            }
        }
    }
}