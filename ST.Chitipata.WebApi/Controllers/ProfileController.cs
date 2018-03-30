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
using System.Text.RegularExpressions;
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
       
        [HttpPost]
        public IHttpActionResult SignUpE(Profile profile)
        {
            return Ok(signup(profile, 1));
        }

        [HttpPost]
        public IHttpActionResult SignUpM(Profile profile)
        {
            return Ok(signup(profile,2));
        }


        public Response signup(Profile profile,int type)
        {
            try
            {
                //Validation
                if (type == 1)
                {
                    Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = emailRegex.Match(profile.User.Email);

                    if (!match.Success)
                    {
                        throw new ApplicationException("Incorrect EmailId.");
                    }

                    if (profile.User.PasswordHash.Length < 8)
                        throw new ApplicationException("Password should be greater than 7 charactors.");

                }
                else if(type==2)
                {
                    Regex mobileRegex = new Regex(@"^(0|\+91)?[789]\d{9}$");
                    Match match = mobileRegex.Match(profile.User.Mobile);

                    if (!match.Success)
                    {
                        throw new ApplicationException("Incorrect Mobile Number.");
                    }

                    if (profile.User.PasswordHash.Length < 8)
                        throw new ApplicationException("Password should be greater than 7 charactors.");

                }
                else if(type==3)
                {

                    Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = emailRegex.Match(profile.User.Email);

                    if (!match.Success)
                    {
                        throw new ApplicationException("Incorrect EmailId.");
                    }
                }
                



                var emailOrMobile =( profile.User.Mobile!=null && profile.User.Mobile != "") ? profile.User.Mobile : profile.User.Email;
                
                User existUser = _userService.GetByEmailorMobile(emailOrMobile);

                if (existUser != null)
                    throw new ApplicationException("User already existed with \"" + emailOrMobile + "\".");

                profile.Email = profile.User.Email;
                profile.Mobile = profile.User.Mobile;

                MongoProfile mongoProfile = _dbManager.Add(new MongoProfile()
                {
                    Email = profile.Email,
                    Mobile = profile.Mobile,
                    Name = profile.ProfileName
                });

                profile.Id = mongoProfile.Id.ToString();
               

                if (type == 1)
                {
                    profile.User.Salt = Common.Security.Library.Utilities.Random.RandomString(32);
                    profile.User.PasswordHash = PasswordMnager.hashPassword(profile.User.PasswordHash, Encoding.ASCII.GetBytes(profile.User.Salt));

                    profile.Status = ProfileStatus.CWE;
                    profile.User.EmailVerification = Common.Security.Library.Utilities.Random.RandomString(32);
                    profile.IsActive = true;

                }
                else if(type == 2)
                {
                    profile.User.Salt = Common.Security.Library.Utilities.Random.RandomString(32);
                    profile.User.PasswordHash = PasswordMnager.hashPassword(profile.User.PasswordHash, Encoding.ASCII.GetBytes(profile.User.Salt));

                    profile.Status = ProfileStatus.CWM;
                    profile.User.MobileVerificationOTP = Common.Security.Library.Utilities.Random.RandomString(8, true);
                    profile.IsActive = true;

                }
                else if (type == 3)
                {
                    profile.IsActive = true;
                }

                profile = _profileService.Add(profile);

                if(profile==null)
                    throw new ApplicationException("Unable to process request. Please try later.");

                profile.User = null;

                return new Response(profile);

            }
            catch(Exception ex)
            {
                return new Response(ex);
            }
            
        }
        

        public IHttpActionResult LoginWithSocialAccount(Profile profile)
        {
            try
            {
                User user = _userService.GetByEmailorMobile(profile.User.Email);

                if (user == null)
                {
                    Response response = signup(profile, 3);
                    profile = (Profile)response.data;
                }
                else
                {
                    profile = _profileService.GetById(user.Id);
                }

                if (profile.IsActive==true)
                {
                    Token token = new Token()
                    {
                        DeviceId = "",
                        IsMobile = false,
                        UserAgent = "",
                        Details = "",
                        Profile = new Profile() { Id = profile.Id },
                        RefreshToken = TokenManager.GenerateToken(profile.Id, _refreshTokenExpireTime),
                        SessionToken = TokenManager.GenerateToken(profile.Id, _sessionTokenExpireTime)
                    };

                    profile.Token = _tokenService.Add(token);

                    return Ok(new Response(profile));

                }
                else
                {
                    throw new ApplicationException("Your account is inactive. Please contact our support team.");
                }
            }
            catch(Exception ex)
            {
                return Ok(new Response(ex));
            }
        }


        public IHttpActionResult Login(Profile profile)
        {
            try
            {
                var emailOrMobile = (profile.User.Mobile != null && profile.User.Mobile != "") ? profile.User.Mobile : profile.User.Email;
                User user = _userService.GetByEmailorMobile(emailOrMobile);

                if (user!=null)
                {
                    var passwordHash = PasswordMnager.hashPassword(profile.User.PasswordHash, Encoding.ASCII.GetBytes(user.Salt));

                    if (user.PasswordHash == passwordHash)
                    {
                        var userProfile = _profileService.GetById(user.Id);

                        if (!userProfile.IsActive.GetValueOrDefault())
                        {
                            if(userProfile.Status==ProfileStatus.CWM)
                                throw new ApplicationException("Verify your Mobile.");
                            else if (userProfile.Status == ProfileStatus.CWE)
                                throw new ApplicationException("Verify your Email.");
                            else if (userProfile.Status == ProfileStatus.ENVMV)
                                throw new ApplicationException("Verify your Email.");
                            else if (userProfile.Status == ProfileStatus.EVMNV)
                                throw new ApplicationException("Verify your Mobile.");
                            else if (userProfile.Status == ProfileStatus.Locked)
                                throw new ApplicationException("Your account has been locked. Please contact our Support team.");
                            else if (userProfile.Status == ProfileStatus.Deactivated)
                                throw new ApplicationException("Your account has been Deactivated. Please contact our Support team.");
                            else
                                throw new ApplicationException("Oops! Please try some time later.");
                        }

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

                        userProfile.Token = _tokenService.Add(token);

                        return Ok(new Response(userProfile));

                    }
                    else
                    {
                        throw new ApplicationException("Invalid credentials.");
                    }
                }
                else
                {
                    throw new ApplicationException("Invalid credentials.");
                }
            }
            catch(Exception ex)
            {
                return Ok(new Response(ex));
            }
            
        }


        [HttpGet]
        [Route("api/Profile/VerifyEmail/{verificationToken}")]
        public IHttpActionResult VerifyEmail(string verificationToken)
        {
            try
            {

                var user = _userService.GetByVerifictionToken(verificationToken);
                if(user!=null)
                {

                    var profile = _profileService.GetById(user.Id);

                    if (profile.Status == ProfileStatus.ENVMV)
                    {
                        profile.Status = ProfileStatus.EVMV;
                        profile.IsActive = true;
                    }
                    else if (profile.Status == ProfileStatus.CWE)
                    {
                        profile.Status = ProfileStatus.EVMNV;
                    }

                    _profileService.Update(profile);
                    user.EmailVerification = "";
                    _userService.Update(user);

                    return Ok("Your email verified sucessfully.");

                }
                else
                {
                    return Ok("Invalid email activation code.");
                }
            }
            catch(Exception ex)
            {
                return Ok("Your email activation code expired.");
            }

        }


        [HttpPost]
        [Route("api/Profile/VerifyMobile")]
        public IHttpActionResult VerifyMobile(Profile profile)
        {
            try
            {
                string uToken = "";
                var headers = Request.Headers;

                if (headers.Contains("SessionToken"))
                {
                    uToken = headers.GetValues("SessionToken").First();
                }

                var userToken = _tokenService.GetByToken(uToken);
                var user = _userService.GetById(userToken.Profile.Id);

                profile = _profileService.GetById(userToken.Profile.Id);

                if (profile.Status == ProfileStatus.EVMNV)
                {
                    profile.Status = ProfileStatus.EVMV;
                    profile.IsActive = true;
                }
                else if (profile.Status == ProfileStatus.CWM)
                {
                    profile.Status = ProfileStatus.ENVMV;
                }

                user.MobileVerificationOTP = "";


                _profileService.Update(profile);
                _userService.Update(user);

                return Ok("Your mobile verified sucessfully.");

            }
            catch (Exception ex)
            {
                return Ok("Invalid OTP.");
            }

        }



        [HttpGet]
        public IHttpActionResult RefreshToken()
        {
            try
            {
                string rToken = "";
                var headers = Request.Headers;

                if (headers.Contains("RefreshToken"))
                {
                    rToken = headers.GetValues("RefreshToken").First();
                }

                var refreshToken = _tokenService.GetByRefreshToken(rToken);

                if (refreshToken == null)
                    throw new ApplicationException("Invalid token. Please login again");

                if (TokenManager.ValidateToken(refreshToken.RefreshToken))
                {
                    refreshToken.SessionToken = TokenManager.GenerateToken(refreshToken.Profile.Id, _sessionTokenExpireTime);
                    _tokenService.Update(refreshToken);
                    return Ok(refreshToken);
                }
                else
                {
                    throw new ApplicationException("Invalid token. Please login again");
                }

            }
            catch(Exception ex)
            {
                return Ok(new Response(ex));
            }
        }
    }
}
