using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Services
{
    public class TokenService
    {
        public Token IsAuthorized(string tokenText)
        {
            using (var uw = new UnitOfWork())
            {
                var tokenRepo = uw.GetRepository<Token>();
                var tokens = tokenRepo.GetAll();
                foreach (var token in tokens)
                {
                    if(token.TokenText.Equals(tokenText))
                    {
                        return token;
                    }
                }
            }
            return null;
        }

        public Token IsAuthorizedAdviser(string tokenText)
        {
            using (var uw = new UnitOfWork())
            {
                var tokenRepo = uw.GetRepository<Token>();
                var tokens = tokenRepo.GetAll();
                foreach (var token in tokens)
                {
                    if (token.TokenText.Equals(tokenText) && token.User.RoleId == 2)
                    {
                        return token;
                    }
                }
            }
            return null;
        }

        public Token IsAuthorizedAdmin(string tokenText)
        {
            using (var uw = new UnitOfWork())
            {
                var tokenRepo = uw.GetRepository<Token>();
                var tokens = tokenRepo.GetAll();
                foreach (var token in tokens)
                {

                    if (token.TokenText.Equals(tokenText) && token.User.RoleId == 1)
                    {
                        return token;
                    }
                }
            }
            return null;
        }

        public void UnAuthorize(string tokenText)
        {
            using (var uw = new UnitOfWork())
            {
                var tokenRepo = uw.GetRepository<Token>();
                var token = tokenRepo.GetAll().Where(x=> x.TokenText == tokenText).FirstOrDefault();
                tokenRepo.Remove(token.Id);
            }

        }
        public void Update(int idToken)
        {
            using (var uw = new UnitOfWork())
            {
                var tokenRepo = uw.GetRepository<Token>();
                var token = tokenRepo.Find(idToken);
                token.Expiration = DateTime.Now.AddMinutes(30);
                tokenRepo.Update(token);
                uw.Save();
            }
               
        }
    }
}