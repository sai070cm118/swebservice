using ST.Common.Security.Library.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ST.Common.Security.Library.Entities;
using ST.Common.Security.Library.EntityFramework;
using ST.Common.Security.Library.Utilities;
using System.Data.Entity.Validation;

namespace ST.Common.Security.Library.Repositories.Implementation
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly usermanagementEntities _entityFramework;

        public ProfileRepository()
        {
            _entityFramework = new usermanagementEntities();
        }

        public Profile Add(Profile profile)
        {
            profile profileDbModel = new profile()
            {
                C_id= profile.Id,
                Mobile=profile.Mobile,
                AccountType =profile.AccountType.GetValueOrDefault(),
                Email=profile.Email,
                FirstName= profile.FirstName,
                IsActive= profile.IsActive.GetValueOrDefault(),
                LastName= profile.LastName,
                Live= profile.Live.GetValueOrDefault(),
                Location= profile.Location,
                ProfileName= profile.ProfileName,
                ProfilePic= profile.ProfilePic,
                Status=(int)profile.Status.GetValueOrDefault()

            };

            profileDbModel.user = new user()
            {
                C_id=profile.User.Id,
                Email= profile.User.Email,
                EmailVerification= profile.User.EmailVerification,
                FacebookID= profile.User.FacebookID,
                GooglePlus= profile.User.GooglePlus,
                KeepMe= profile.User.KeepMe,
                Mobile= profile.User.Mobile,
                MobileVerificationOTP= profile.User.MobileVerificationOTP,
                PasswordHash= profile.User.PasswordHash,
                RecoverHash= profile.User.RecoverHash,
                RecoverTimeStamp= null,
                RecoverType= profile.User.RecoverType,
                RegistrationIP= profile.User.RegistrationIP,
                RegistrationTime= DateTime.UtcNow,
                Salt= profile.User.Salt,
                TempMobile= profile.User.TempMobile
            };

            try
            {
                _entityFramework.profiles.Add(profileDbModel);
                _entityFramework.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
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


            return profile;
        }

        public IList<Profile> GetAll()
        {
            throw new NotImplementedException();
        }

        public Profile GetByEmailorMobile(string emailOrMobile)
        {
            profile profile = _entityFramework.profiles.FirstOrDefault(x => x.Email == emailOrMobile);

            if (profile != null)
                return profile.ConvertToProfile();

            return null;
        }

        public Profile GetById(string id)
        {
            profile profile = _entityFramework.profiles.FirstOrDefault(x => x.C_id == id);

            if (profile != null)
                return profile.ConvertToProfile();

            return null;
        }

        public IList<Profile> GetByName(string name)
        {
            IList<profile> profiles = _entityFramework.profiles.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name) || x.ProfileName.Contains(name)).ToList();

            if (profiles != null)
                return profiles.Select(x=>x.ConvertToProfile()).ToList();

            return null;
        }

        public Profile Update(Profile profile)
        {
            profile profileDbModel = _entityFramework.profiles.FirstOrDefault(x => x.C_id == profile.Id);

            if (profileDbModel != null)
            {
                profileDbModel.AccountType = profile.AccountType != null ? profile.AccountType.GetValueOrDefault() : profileDbModel.AccountType;

                profileDbModel.FirstName = profile.FirstName != null ? profile.FirstName : profileDbModel.FirstName;
                profileDbModel.LastName = profile.LastName != null ? profile.LastName : profileDbModel.LastName;
                profileDbModel.IsActive = profile.IsActive != null ? profile.IsActive.GetValueOrDefault() : profileDbModel.IsActive;
                profileDbModel.Live = profile.Live != null ? profile.Live.GetValueOrDefault() : profileDbModel.Live;
                profileDbModel.Location = profile.Location != null ? profile.Location : profileDbModel.Location;
                profileDbModel.ProfileName = profile.ProfileName != null ? profile.ProfileName : profileDbModel.ProfileName;
                profileDbModel.ProfilePic = profile.ProfilePic != null ? profile.ProfilePic : profileDbModel.ProfilePic;
                profileDbModel.Status = profile.Status != null ? (int)profile.Status.GetValueOrDefault() :profileDbModel.Status;
                profileDbModel.user = new user()
                {
                    Email = profile.User.Email,
                    EmailVerification = profile.User.EmailVerification,
                    FacebookID = profile.User.FacebookID,
                    GooglePlus = profile.User.GooglePlus,
                    KeepMe = profile.User.KeepMe,
                    Mobile = profile.User.Mobile,
                    MobileVerificationOTP = profile.User.MobileVerificationOTP,
                    PasswordHash = profile.User.PasswordHash,
                    RecoverHash = profile.User.RecoverHash,
                    RecoverTimeStamp = profile.User.RecoverTimeStamp,
                    RecoverType = profile.User.RecoverType,
                    RegistrationIP = profile.User.RegistrationIP,
                    RegistrationTime = profile.User.RegistrationTime,
                    Salt = profile.User.Salt,
                    TempMobile = profile.User.TempMobile
                };
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
