using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class UserMapper
    {
        public static UserModel MapUser (User user)
        {
            return new UserModel { Id = user.Id, AvatarUrl = user.AvatarUrl, Bio = user.Bio, Email = user.Email, Name = user.Name, Password = user.Password, RoleId = user.RoleId, Status = user.Status, Username = user.Username, Website = user.Website };
        }

        public static User MapUserDataModel (UserModel user)
        {
            return new User { Id = user.Id, Name = user.Name, Email = user.Email, AvatarUrl = user.AvatarUrl, Bio = user.Bio, Website = user.Website, Username = user.Username, Status = user.Status, RoleId = user.RoleId, Password = user.Password };
        }

        public static UserModelForCategoryView MapUserForCategoryView(User user)
        {
            return new UserModelForCategoryView { Id = user.Id, AvatarUrl = user.AvatarUrl, Bio = user.Bio, Name = user.Name, Username = user.Username };
        }

        public static UserModelForProfile MapUserForProfile (User user)
        {
            return new UserModelForProfile { Id = user.Id, Bio = user.Bio, AvatarUrl = user.AvatarUrl, Email = user.Email, Name = user.Name, Password = user.Password, Website = user.Website };
        }

        public static User MapUserDataModelFromProfile (UserModelForProfile user)
        {
            return new User { Id = user.Id, Bio = user.Bio, AvatarUrl = user.AvatarUrl, Email = user.Email, Name = user.Name, Password = user.Password, Website = user.Website };
        }

        public static User MapUserFromSignUp (UserModelForSignUp user)
        {
            return new User { Id = user.Id, Username = user.Username, Password = user.Password, Email = user.Email, Name = user.Name };
        }

        public static List<UserModel> MapUserList(List<User> users)
        {
            var list = new List<UserModel>();
            foreach (var user in users)
            {
                list.Add(new UserModel { Id = user.Id, AvatarUrl = user.AvatarUrl, Bio = user.Bio, Email = user.Email, Name = user.Name, Password = user.Password, RoleId = user.RoleId, Status = user.Status, Username = user.Username, Website = user.Website });
            }
            return list;
        }

    }
}