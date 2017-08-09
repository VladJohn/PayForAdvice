using Domain;
using Domain.Enums;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WebAPI.FacebookIntegration.Models;
using WebAPI.Mappings;
using WebAPI.Models;
using WebAPI.ValidatorsModel;
using static System.Web.HttpContext;

namespace WebAPI.Services
{
    public class UserService
    {
        private UserValidator validatorUser = new UserValidator();
        private AdviserValidator validatorAdviser = new AdviserValidator();

        public UserModelForSignUp AddUser(UserModelForSignUp user)
        {
            var errors = validatorUser.Check(user);
            if (errors.Count != 0)
            {
                throw new ModelException(string.Join(Environment.NewLine, errors));
            }
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var userToBeAdded = UserMapper.MapUserFromSignUp(user);
                userToBeAdded.Status = (int)UserStatusEnum.Active;
                var addedUser = userRepository.Add(userToBeAdded);

                unitOfWork.Save();
                return new UserModelForSignUp { Id = addedUser.Id, Email= addedUser.Email, Name = addedUser.Name, Password =addedUser.Password, RoleId = addedUser.RoleId, Username = addedUser.Username};
            }
        }

        public UserModelForSignUpAdviser AddAdviser(UserModelForSignUpAdviser adviser)
        {
            var errors = validatorAdviser.Check(adviser);
            if (errors.Count != 0)
            {
                throw new ModelException(string.Join(Environment.NewLine, errors));
            }
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var userToBeAdded = new User { AvatarUrl = adviser.AvatarUrl, Email = adviser.Email, Name = adviser.Name, Password = adviser.Password, RoleId = adviser.RoleId, Website = adviser.Website, Username = adviser.Username };
                userToBeAdded.Status = (int)UserStatusEnum.Active;
                var addedUser = userRepository.Add(userToBeAdded);

                unitOfWork.Save();
                return new UserModelForSignUpAdviser { Website = addedUser.Website, AvatarUrl = addedUser.AvatarUrl, Id = addedUser.Id, Email = addedUser.Email, Name = addedUser.Name, Password = addedUser.Password, RoleId = addedUser.RoleId, Username = addedUser.Username };
            }
        }

        public UserModel AddUserFromFacebook(string name, string email, string pictureUrl)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var userToBeAdded = new User();
                userToBeAdded.Name = name;
                userToBeAdded.Email = email;
                userToBeAdded.Username = email;
                userToBeAdded.Password = Guid.NewGuid().ToString();
                userToBeAdded.AvatarUrl = pictureUrl;
                userToBeAdded.Status = (int)UserStatusEnum.Active;
                userToBeAdded.RoleId = 3;
                userRepository.Add(userToBeAdded);
                unitOfWork.Save();
                return UserMapper.MapUser(userToBeAdded);
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

        public TokenModel LogInWithFacebook(string facebookToken, string email)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var userList = userRepository.GetAll();
                var tokenRepository = unitOfWork.GetRepository<Token>();
                User userFound = userList.ToList().Where(x => x.Email.Equals(email)).FirstOrDefault();
                if (userFound != null)
                {
                    var tokenToBeAdded = new Token { Ip = GetClientIp(), Expiration = DateTime.Now.AddMinutes(30), UserId = userFound.Id, TokenText = facebookToken };
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
            var errors = new UserModelForProfileValidator().Check(user);
            if (errors.Count != 0)
            {
                throw new ModelException(string.Join(Environment.NewLine, errors));
            }
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

        private static string GetClientIp()
        {
            var ip = Current != null ? Current.Request.UserHostAddress : "";
            if (Current != null)
            {
                var context = new HttpContextWrapper(Current);
            }
            if (Current != null)
            {
                var request = (HttpRequestMessage) Current.Items["MS_HttpRequestMessage"];
            }
            return ip;
        }

        public bool FindUserByEmail(UserFacebookModel user)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var userList = userRepository.GetAll();
                foreach(var userInList in userList)
                {
                    if(user.Email.Equals(userInList.Email))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

