using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ST.Common.Security.Library.Utilities
{
    public static class ConfigurationStore
    {
        public static string GetTokenKey()
        {
            return ConfigurationManager.AppSettings[Constants.TokenKey];
        }


        public static int GetRefreshTokenExpireLength()
        {
            return int.Parse(ConfigurationManager.AppSettings[Constants.RefreshTokenExpireLength]);
        }


        public static int GetSessionTokenExpireLength()
        {
            return int.Parse(ConfigurationManager.AppSettings[Constants.SessionTokenExpireLength]);
        }

        public static string GetMongoConnection()
        {
            return ConfigurationManager.AppSettings[Constants.MongoConnection];
        }
        
        public static string GetMongoDatabase()
        {
            return ConfigurationManager.AppSettings[Constants.MongoDatabase];
        }
    }
}
