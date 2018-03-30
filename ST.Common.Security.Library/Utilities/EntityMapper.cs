using ST.Common.Security.Library.Entities;
using ST.Common.Security.Library.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Utilities
{
    public static class EntityMapper
    {
        public static User ConvertToUser(this user userDbModel)
        {
            return new User()
            {
                Email= userDbModel.Email,
                Id= userDbModel.C_id,
                EmailVerification= userDbModel.EmailVerification,
                FacebookID= userDbModel.FacebookID,
                GooglePlus= userDbModel.GooglePlus,
                KeepMe= userDbModel.KeepMe,
                Mobile= userDbModel.Mobile,
                MobileVerificationOTP= userDbModel.MobileVerificationOTP,
                PasswordHash= userDbModel.PasswordHash,
                RecoverHash= userDbModel.RecoverHash,
                RecoverTimeStamp= userDbModel.RecoverTimeStamp,
                RecoverType= userDbModel.RecoverType,
                RegistrationIP= userDbModel.RegistrationIP,
                RegistrationTime= userDbModel.RegistrationTime,
                Salt= userDbModel.Salt,
                TempMobile= userDbModel.TempMobile
                
            };
        }
        public static Profile ConvertToProfile(this profile profileDbModel)
        {
            return new Profile()
            {
                AccountType= (int)profileDbModel.AccountType,
                Email= profileDbModel.Email,
                Mobile = profileDbModel.Mobile,
                FirstName= profileDbModel.FirstName,
                LastName= profileDbModel.LastName,
                ProfileName= profileDbModel.ProfileName,
                Id= profileDbModel.C_id,
                IsActive= profileDbModel.IsActive,
                Live= profileDbModel.Live,
                Location= profileDbModel.Location,
                ProfilePic= profileDbModel.ProfilePic,
                Status= (ProfileStatus)profileDbModel.Status
            };
        }


        public static Token ConvertToToken(this token tokendbModel)
        {
            return new Token()
            {
                Details =tokendbModel.Details,
                DeviceId=tokendbModel.DeviceId,
                 IsMobile=tokendbModel.IsMobile,
                 Profile=new Profile() { Id=tokendbModel.userId},
                 RefreshToken=tokendbModel.RefreshToken,
                 SessionToken=tokendbModel.Sessiontoken,
                 UserAgent=tokendbModel.UserAgent,
                 Id= tokendbModel.C_id
            };
        }
    }
}
