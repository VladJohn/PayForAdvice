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
        public UserModelForSignUp AddUser(UserModelForSignUp user)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                var userToAdd = UserMapper.MapUserFromSignUp(user);
                userToAdd.Status = "active";
                userToAdd.RoleId = 3;
                repo.Add(userToAdd);
                uw.Save();
                return user;
            }
        }


        public UserModel LogIn(string username, string password)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                var users = repo.GetAll();
                User found = users.ToList().Where(x => x.Username.Equals(username) && x.Password.Equals(password)).FirstOrDefault();
                if (found != null)
                {
                    return UserMapper.MapUser(found);
                }
                return null;
            }
        }

        public UserModelForProfile GetUser(int idUser)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                var user = UserMapper.MapUserForProfile(repo.Find(idUser));
                user.Rating = GetRatingForUser(idUser);
                return user;
            }
        }

        public List<UserModelForCategoryView> GetAdvicersByCategory(int idCategory)
        {
            var advicers = new List<UserModelForCategoryView>();
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                var users = repo.GetAll().Where(x => x.Role.Name == "adviser" && x.Status == "active" && x.Categories.Any(c => c.Id == idCategory)).ToList();
                foreach ( var user in users)
                {
                    var advicer = UserMapper.MapUserForCategoryView(user);
                    advicer.Rating = GetRatingForUser(advicer.Id);
                    advicers.Add(advicer); 
                }
                return advicers;
                
            }
        }

        public UserModelForProfile UpdateUserProfile(UserModelForProfile user)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                var userToUpdate = repo.Find(user.Id);
                userToUpdate.Password = user.Password;
                userToUpdate.Website = user.Website;
                userToUpdate.Bio = user.Bio;
                userToUpdate.AvatarUrl = user.AvatarUrl;
                repo.Update(userToUpdate);
                uw.Save();
                return user;
            }
        }

        public double GetRatingForUser(int idUser)
        {
            double rating = 0;
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<User>();
                var user = repo.Find(idUser);
                if (user.RoleId == 2)
                {
                    foreach (var a in user.Answers)
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

