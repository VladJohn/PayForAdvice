using Domain;
using Repository;
using System.Collections.Generic;
using WebAPI.Models;
using WebAPI.Mappings;

namespace WebAPI.Services
{
    public class CategoryService
    {
        //add a new category to the list
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

        public void AddCategoryToUser(int userId, int categoryId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var categoryRepository = unitOfWork.GetRepository<Category>();
                var userRepository = unitOfWork.GetRepository<User>();
                var category = categoryRepository.Find(categoryId);
                var user = userRepository.Find(userId);
                category.Users.Add(user);
                unitOfWork.Save();
            }

        }

        //get all the existent categories
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