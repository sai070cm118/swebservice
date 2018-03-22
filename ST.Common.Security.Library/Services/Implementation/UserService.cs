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
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Add(User profile)
        {
            return _userRepository.Add(profile);
        }

        public IList<User> GetAll()
        {
            return GetAll();
        }

        public User GetByEmailorMobile(string name)
        {
            return _userRepository.GetByEmailorMobile(name);
        }

        public User GetById(string id)
        {
            return _userRepository.GetById(id);
        }

        public IList<User> GetByName(string name)
        {
            return _userRepository.GetByName(name);
        }

        public User Update(User profile)
        {
            return _userRepository.Update(profile);
        }
    }
}
