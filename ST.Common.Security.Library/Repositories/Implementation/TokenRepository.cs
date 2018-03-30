using ST.Common.Security.Library.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ST.Common.Security.Library.Entities;
using ST.Common.Security.Library.EntityFramework.MongoDAL;
using ST.Common.Security.Library.EntityFramework;
using ST.Common.Security.Library.Utilities;
using System.Data.Entity.Validation;

namespace ST.Common.Security.Library.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {


        private readonly usermanagementEntities _entityFramework;

        public TokenRepository()
        {
            _entityFramework = new usermanagementEntities();
        }
        
        public Token Add(Token token)
        {

            token tokendbModel = new EntityFramework.token() {
                Details=token.Details,
                DeviceId=token.DeviceId,
                IsMobile=token.IsMobile,
                RefreshToken=token.RefreshToken,
                Sessiontoken=token.SessionToken,
                userId=token.Profile.Id,
                UserAgent=token.UserAgent

            };


            try
            {
                _entityFramework.tokens.Add(tokendbModel);
                _entityFramework.SaveChanges();
                token.Id = tokendbModel.C_id;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {

            }


            return token;
        }

        public Token GetById(int id)
        {
            token token = _entityFramework.tokens.FirstOrDefault(x => x.C_id == id);

            if (token != null)
                return token.ConvertToToken();

            return null;
        }

        public Token GetByRefreshToken(string refreshToken)
        {
            token token = _entityFramework.tokens.FirstOrDefault(x => x.RefreshToken == refreshToken);

            if (token != null)
                return token.ConvertToToken();

            return null;
        }

        public Token GetByToken(string token)
        {
            token userToken = _entityFramework.tokens.FirstOrDefault(x => x.Sessiontoken == token);

            if (userToken != null)
                return userToken.ConvertToToken();

            return null;
        }

        public Token GetByUserId(string id)
        {
            token token = _entityFramework.tokens.FirstOrDefault(x => x.userId == id);

            if (token != null)
                return token.ConvertToToken();

            return null;
        }

        public Token Update(Token token)
        {
            token tokenDbModel = _entityFramework.tokens.FirstOrDefault(x => x.C_id == token.Id);

            if (tokenDbModel != null)
            {
                tokenDbModel.Sessiontoken = token.SessionToken == null ? tokenDbModel.Sessiontoken : token.SessionToken;
            }

            try
            {
                _entityFramework.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
