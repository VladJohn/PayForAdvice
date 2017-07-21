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
        public Token Authorize(string tokenText)
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