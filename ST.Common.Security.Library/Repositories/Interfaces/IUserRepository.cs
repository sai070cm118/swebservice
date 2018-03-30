using ST.Common.Security.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Add(User profile);

        IList<User> GetAll();

        User GetById(string id);

        User GetByVerifictionToken(string token);

        User GetByEmailorMobile(string name);

        IList<User> GetByName(string name);

        User Update(User profile);
    }
}
