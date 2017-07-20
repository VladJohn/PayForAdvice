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

        public static List<UserModel> MapUserList(List<User> users)
        {
            var list = new List<UserModel>();
            foreach (var user in users)
            {
                list.Add(new UserModel { Id = user.Id, AvatarUrl = user.AvatarUrl, Bio = user.Bio, Email = user.Email, Name = user.Name, Password = user.Password, RoleId = user.RoleId, Status = user.Status, Username = user.Username, Website = user.Website });
            }
            return list;
        }

        public static User MapUserDataModel (UserModel user)
        {
            return new User { Id = user.Id, Name = user.Name, Email = user.Email, AvatarUrl = user.AvatarUrl, Bio = user.Bio, Website = user.Website, Username = user.Username, Status = user.Status, RoleId = user.RoleId, Password = user.Password };
        }
    }
}