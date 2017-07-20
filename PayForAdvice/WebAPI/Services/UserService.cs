using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Mappings;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService
    {
        public UserModel AddUser(UserModel user)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                repo.Add(UserMapper.MapUserDataModel(user));
                uw.Save();
                return user;
            }
        }


        public User LogIn(string username, string password)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                var users = repo.GetAll();
                User found = users.ToList().Where(x => x.Username.Equals(username) && x.Password.Equals(password)).FirstOrDefault();
                if (found != null)
                {
                    return found;
                }
                return null;
            }
        }

        public List<UserModel> GetAdvicersByCategory(int idCategory)
        {
            var foundUsers = new List<UserModel>();
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                var users = repo.GetAll().Where(x => x.Role.Name=="advicer" && x.Status=="active" && x.Categories.Any(c => c.Id == idCategory)).ToList();
                return UserMapper.MapUserList(users);
            }
        }

        public double GetRatingForUser(int idUser)
        {
           double rating = 0;
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                var user = repo.Find(idUser);
                if (user.Role.Name == "advicer")
                {
                    foreach( var a in user.Answers)
                    {
                        rating += a.Rating; 
                    }
                }
                rating = rating / user.Answers.Count;
            }
            return rating;
        }


        public UserModel DeleteUser(int idUser)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                var user = repo.Find(idUser);
                user.Status = "deleted";
                repo.Update(user);
                uw.Save();
                return UserMapper.MapUser(user);
            }
        }










    }
}