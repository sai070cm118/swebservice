using ST.Common.Security.Library.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ST.Common.Security.Library.Entities;
using ST.Common.Security.Library.EntityFramework;
using ST.Common.Security.Library.Utilities;

namespace ST.Common.Security.Library.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly usermanagementEntities _entityFramework;

        public UserRepository()
        {
            _entityFramework = new usermanagementEntities();
        }

        public User Add(User user)
        {
            user userDbModel = new user()
            {
                C_id= user.Id,
                Email= user.Email,
                EmailVerification= user.EmailVerification,
                FacebookID= user.FacebookID,
                GooglePlus= user.GooglePlus,
                KeepMe= user.KeepMe,
                Mobile= user.Mobile,
                MobileVerificationOTP= user.MobileVerificationOTP,
                PasswordHash= user.PasswordHash,
                RecoverHash= user.RecoverHash,
                RecoverTimeStamp= user.RecoverTimeStamp,
                RecoverType= user.RecoverType,
                RegistrationIP= user.RegistrationIP,
                RegistrationTime= user.RegistrationTime,
                Salt= user.Salt,
                TempMobile= user.TempMobile
            };


            try
            {
                _entityFramework.users.Add(userDbModel);
                _entityFramework.SaveChanges();
                user.Id = userDbModel.C_id;
            }
            catch (Exception ex)
            {

            }


            return user;
        }

        public IList<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByEmailorMobile(string emailOrMobile)
        {
            user user = _entityFramework.users.FirstOrDefault(x => x.Email == emailOrMobile || x.Mobile == emailOrMobile);

            if (user != null)
                return user.ConvertToUser();

            return null;

        }

        public User GetById(string id)
        {
            user user = _entityFramework.users.FirstOrDefault(x => x.C_id == id);

            if (user != null)
                return user.ConvertToUser();

            return null;
        }

        public IList<User> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public User Update(User user)
        {
            user userDbModel = _entityFramework.users.FirstOrDefault(x => x.C_id== user.Id);

            if (userDbModel != null)
            {

                userDbModel.KeepMe = user.KeepMe != null ? user.KeepMe : userDbModel.KeepMe;
                userDbModel.RegistrationIP = user.RegistrationIP != null ? user.RegistrationIP : userDbModel.RegistrationIP;
                userDbModel.RegistrationTime = user.RegistrationTime != null ? user.RegistrationTime : userDbModel.RegistrationTime;

                userDbModel.PasswordHash = user.PasswordHash != null ? user.PasswordHash : userDbModel.PasswordHash;
                userDbModel.Salt = user.Salt != null ? user.Salt : userDbModel.Salt;

                userDbModel.FacebookID = user.FacebookID != null ? user.FacebookID : userDbModel.FacebookID;
                userDbModel.GooglePlus = user.GooglePlus != null ? user.GooglePlus : userDbModel.GooglePlus;

                userDbModel.EmailVerification = user.EmailVerification!= null ? user.EmailVerification : userDbModel.EmailVerification;
                userDbModel.TempMobile = user.TempMobile != null ? user.TempMobile : userDbModel.TempMobile;
                userDbModel.MobileVerificationOTP = user.MobileVerificationOTP != null ? user.MobileVerificationOTP : userDbModel.MobileVerificationOTP;
                userDbModel.RecoverHash = user.RecoverHash != null ? user.RecoverHash : userDbModel.RecoverHash;
                userDbModel.RecoverTimeStamp = user.RecoverTimeStamp != null ? user.RecoverTimeStamp : userDbModel.RecoverTimeStamp.GetValueOrDefault();
                userDbModel.RecoverType = user.RecoverType != null ? user.RecoverType : userDbModel.RecoverType;


            }


            try
            {
                _entityFramework.SaveChanges();
            }
            catch(Exception ex)
            {

            }

            return null;
        }
    }
}
