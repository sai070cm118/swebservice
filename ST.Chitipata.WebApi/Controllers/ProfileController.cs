using ST.Common.Security.Library.Entities;
using ST.Common.Security.Library.EntityFramework.MongoDAL;
using ST.Common.Security.Library.Services.Interfaces;
using ST.Common.Security.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ST.UserManagement.WebApi.Controllers
{
    public class ProfileController : ApiController
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly int _refreshTokenExpireTime;
        private readonly int _sessionTokenExpireTime;
        private readonly DBManager _dbManager;

        public ProfileController(IProfileService profileService, IUserService userService, ITokenService tokenService)
        {
            _profileService = profileService;
            _userService = userService;
            _tokenService = tokenService;
            _dbManager = new DBManager();
            _refreshTokenExpireTime = ConfigurationStore.GetRefreshTokenExpireLength();
            _sessionTokenExpireTime = ConfigurationStore.GetSessionTokenExpireLength();
        }


        //[ApplicationAutherization]
        [HttpGet]
        public Profile Register(Profile profile)
        {
            profile=_profileService.Add(profile);

            return profile;

        }


        [HttpPost]
        public Profile SignUp(Profile profile)
        {
            var emailOrMobile = profile.User.Mobile != "" ? profile.User.Mobile : profile.User.Email;

            User existUser = _userService.GetByEmailorMobile(emailOrMobile);


            if (existUser != null)
                return null;

            profile.Email = profile.User.Email;
            profile.Mobile = profile.User.Mobile;
            
            MongoProfile mongoProfile =_dbManager.Add(new MongoProfile() {
                Email= profile.Email,
                Mobile= profile.Mobile,
                Name= profile.ProfileName
            });

            profile.Id = mongoProfile.Id.ToString();
            profile.Status = ProfileStatus.CWE;
            profile.User.EmailVerification = Common.Security.Library.Utilities.Random.RandomString(32);
            profile.User.Salt= Common.Security.Library.Utilities.Random.RandomString(32);
            profile.User.PasswordHash = PasswordMnager.hashPassword(profile.User.PasswordHash, Encoding.ASCII.GetBytes(profile.User.Salt));
            
            profile = _profileService.Add(profile);
            profile.User = null;

            return profile;
        }
        
        

        


        public Token Login(Profile profile)
        {

            var emailOrMobile = profile.User.Mobile != "" ? profile.User.Mobile : profile.User.Email;

            User user = _userService.GetByEmailorMobile(emailOrMobile);


            var passwordHash = PasswordMnager.hashPassword(profile.User.PasswordHash, Encoding.ASCII.GetBytes(user.Salt));

            if (user.PasswordHash == passwordHash)
            {
                Token token = new Token()
                {
                    DeviceId = "",
                    IsMobile = false,
                    UserAgent = "",
                    Details = "",
                    Profile = new Profile() { Id = user.Id },
                    RefreshToken = TokenManager.GenerateToken(user.Id, _refreshTokenExpireTime),
                    SessionToken = TokenManager.GenerateToken(user.Id, _sessionTokenExpireTime)
                };

                token=_tokenService.Add(token);

                return token;

            }
             
            return null;
        }
        


        [HttpGet]
        public Token RefreshToken()
        {
            string rToken = "";
            var headers = Request.Headers;

            if (headers.Contains("RefreshToken"))
            {
                rToken = headers.GetValues("RefreshToken").First();
            }

            var refreshToken=_tokenService.GetByRefreshToken(rToken);

            if (refreshToken==null)
                return null;

            if (TokenManager.ValidateToken(refreshToken.RefreshToken))
            {
                refreshToken.SessionToken= TokenManager.GenerateToken(refreshToken.Profile.Id, _sessionTokenExpireTime);
                _tokenService.Update(refreshToken);
                return refreshToken;
            }

            return null;
        }

        

    }
}
