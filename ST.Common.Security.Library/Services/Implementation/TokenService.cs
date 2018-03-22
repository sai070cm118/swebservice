using ST.Common.Security.Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ST.Common.Security.Library.Entities;
using ST.Common.Security.Library.Repositories.Interfaces;

namespace ST.Common.Security.Library.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }



        public Token Add(Token token)
        {
            return _tokenRepository.Add(token);
        }

        public Token GetById(int id)
        {
            return _tokenRepository.GetById(id);
        }

        public Token GetByRefreshToken(string refreshToken)
        {
            return _tokenRepository.GetByRefreshToken(refreshToken);
        }

        public Token GetByUserId(string id)
        {
            return _tokenRepository.GetByUserId(id);
        }

        public Token Update(Token token)
        {
            return _tokenRepository.Update(token);
        }
    }
}
