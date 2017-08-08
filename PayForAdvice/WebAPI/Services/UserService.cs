using Domain;
using Domain.Enums;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WebAPI.Mappings;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService
    {
        public UserModelForSignUp AddUser(UserModelForSignUp user)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var userToBeAdded = UserMapper.MapUserFromSignUp(user);
                userToBeAdded.Status = (int)UserStatusEnum.Active;
                userRepository.Add(userToBeAdded);
                unitOfWork.Save();
                return user;
            }
        }


        public TokenModel LogIn(string username, string password)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var userList = userRepository.GetAll();
                var tokenRepository = unitOfWork.GetRepository<Token>();
                User userFound = userList.ToList().Where(x => x.Username.Equals(username) && x.Password.Equals(password)).FirstOrDefault();
                if (userFound != null)
                {
                    var tokenToBeAdded = new Token { Ip = GetClientIp(), Expiration = DateTime.Now.AddMinutes(30), UserId = userFound.Id, TokenText = Guid.NewGuid().ToString()};
                    tokenRepository.Add(tokenToBeAdded);
                    var tokenToBeReturned = new TokenModel { Ip = tokenToBeAdded.Ip, Expiration = tokenToBeAdded.Expiration, UserId = tokenToBeAdded.UserId, TokenText = tokenToBeAdded.TokenText };
                    unitOfWork.Save();
                    return tokenToBeReturned;
                }
                return null;
            }
        }

        public UserModelForProfile GetUser(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var userToBeReturned = UserMapper.MapUserForProfile(userRepository.Find(userId));
                userToBeReturned.Rating = GetRatingForUser(userId);
                return userToBeReturned;
            }
        }

        public List<UserModelForCategoryView> GetAdvicersByCategory(int categoryId)
        {
            var advicerList = new List<UserModelForCategoryView>();
            using (var uw = new UnitOfWork())
            {
                var userRepository = uw.GetRepository<User>();
                var userList = userRepository.GetAll().Where(x => x.RoleId == 2 && x.Status == (int)UserStatusEnum.Active && x.Categories.Any(c => c.Id == categoryId)).ToList();
                foreach ( var user in userList)
                {
                    var advicer = UserMapper.MapUserForCategoryView(user);
                    advicer.Rating = GetRatingForUser(advicer.Id);
                    advicerList.Add(advicer); 
                }
                return advicerList;
                
            }
        }

        public UserModelForProfile UpdateUserProfile(UserModelForProfile user)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var userToUpdate = userRepository.Find(user.Id);
                userToUpdate.Password = user.Password;
                userToUpdate.Website = user.Website;
                userToUpdate.Bio = user.Bio;
                userToUpdate.AvatarUrl = user.AvatarUrl;
                userToUpdate.Email = user.Email;
                userToUpdate.Name = user.Name;
                userRepository.Update(userToUpdate);
                unitOfWork.Save();
                return user;
            }
        }

        public double GetRatingForUser(int userId)
        {
            int counterForRatingsHigherThatZero = 0;
            double ratingSum = 0;
            double ratingAverage = 0;
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var foundUser = userRepository.Find(userId);
                if (foundUser.RoleId == 2)
                {
                    foreach (var answer in foundUser.Answers)
                    {
                        ratingSum += answer.Rating;
                        if (answer.Rating != 0)
                            counterForRatingsHigherThatZero++;
                    }
                }
                if (counterForRatingsHigherThatZero == 0)
                    return 0;
                ratingAverage = ratingSum / counterForRatingsHigherThatZero;
            }
            ratingAverage = Math.Round(ratingAverage, 2);
            return ratingAverage;
        }



        public UserModel DeleteUser(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var userList = userRepository.Find(userId);
                userList.Status = (int)UserStatusEnum.Deleted;
                userRepository.Update(userList);
                unitOfWork.Save();
                return UserMapper.MapUser(userList);
            }
        }

        private string GetClientIp()
        {
            var ip = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "";
            var context = new HttpContextWrapper(HttpContext.Current);
            var request = (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
            return ip;
        }
    }
}

