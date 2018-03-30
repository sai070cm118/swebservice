using ST.Common.Security.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        Token Add(Token token);

        Token GetById(int id);

        Token GetByUserId(string id);

        Token GetByRefreshToken(string refreshToken);

        Token Update(Token token);

        Token GetByToken(string token);

    }
}
