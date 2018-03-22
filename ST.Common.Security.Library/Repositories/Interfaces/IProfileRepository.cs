using ST.Common.Security.Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Profile Add(Profile profile);

        IList<Profile> GetAll();

        Profile GetById(string id);

        Profile GetByEmailorMobile(string name);

        IList<Profile> GetByName(string name);
        
        Profile Update(Profile profile);

    }
}
