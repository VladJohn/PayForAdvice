using Domain;
using Repository;
using System;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class TokenService
    {
        public Token IsAuthorizedBase(string tokenText)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var tokenRepository = unitOfWork.GetRepository<Token>();
                var tokenList = tokenRepository.GetAll();
                foreach (var token in tokenList)
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
            using (var unitOfWork = new UnitOfWork())
            {
                var tokenRepository = unitOfWork.GetRepository<Token>();
                var tokenList = tokenRepository.GetAll();
                foreach (var token in tokenList)
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
            using (var unitOfWork = new UnitOfWork())
            {
                var tokenRepository = unitOfWork.GetRepository<Token>();
                var tokenList = tokenRepository.GetAll();
                foreach (var token in tokenList)
                {

                    if (token.TokenText.Equals(tokenText) && token.User.RoleId == 1)
                    {
                        return token;
                    }
                }
            }
            return null;
        }

        public string UnAuthorize(string tokenText)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var tokenRepository = unitOfWork.GetRepository<Token>();
                var token = tokenRepository.GetAll().Where(x=> x.TokenText == tokenText).FirstOrDefault();
                tokenRepository.Remove(token.Id);
                unitOfWork.Save();
                return "removed";
            }

        }
        public void Update(int tokenId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var tokenRepository = unitOfWork.GetRepository<Token>();
                var token = tokenRepository.Find(tokenId);
                token.Expiration = DateTime.Now.AddMinutes(30);
                tokenRepository.Update(token);
                unitOfWork.Save();
            }
               
        }

        public UserTokenDataModel getUserInfoByToken(string token)
        {
            UserTokenDataModel result = new UserTokenDataModel();
            using (var unitOfWork = new UnitOfWork())
            {
                var tokenRepository = unitOfWork.GetRepository<Token>();
                foreach (var tokenInList in tokenRepository.GetAll())
                {
                    if (tokenInList.TokenText.Equals(token))
                    {
                        result.Id = tokenInList.User.Id;
                        result.Role = tokenInList.User.RoleId;
                        return result;
                    }
                }
            }
            return result;
        }
    }
}