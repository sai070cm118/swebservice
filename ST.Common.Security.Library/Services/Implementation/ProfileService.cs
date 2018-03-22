using ST.Common.Security.Library.Entities;
using ST.Common.Security.Library.Repositories.Interfaces;
using ST.Common.Security.Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Services.Implementation
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public Profile Add(Profile profile)
        {
            return _profileRepository.Add(profile);
        }

        public IList<Profile> GetAll()
        {
            return _profileRepository.GetAll();
        }

        public Profile GetByEmailorMobile(string name)
        {
            return _profileRepository.GetByEmailorMobile(name);
        }

        public Profile GetById(string id)
        {
            return _profileRepository.GetById(id);
        }

        public IList<Profile> GetByName(string name)
        {
            return _profileRepository.GetByName(name);
        }

        public Profile Update(Profile profile)
        {
            return _profileRepository.Update(profile);
        }
    }
}
