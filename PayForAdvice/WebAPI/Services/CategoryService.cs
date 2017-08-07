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
        public CategoryModel AddCategory(CategoryModel newCategory)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var categoryRepository = unitOfWork.GetRepository<Category>();
                categoryRepository.Add(CategoryMapper.MapCategoryDataModel(newCategory));
                unitOfWork.Save();
            }
            return newCategory;
        }

        public List<CategoryModel> GetAllCategories()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var categoryRepository = unitOfWork.GetRepository<Category>();
                var categoryModels = new List<CategoryModel>();
                foreach (var category in categoryRepository.GetAll())
                {
                    var categoryModel = CategoryMapper.MapCategory(category);
                    categoryModels.Add(categoryModel);
                }
                return categoryModels;
            }
        }
    }
}