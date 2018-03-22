using System;
using System.Data.Entity.Validation;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ST.Common.Logger
{
	public class LogHandler : ILogger
	{
		private static readonly log4net.ILog Log =
			log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void LogError(string message, DbEntityValidationException ex)
        {
            Log.Error($"Message: {message}, Exception: {ex.Message}");

            foreach (var eve in ex.EntityValidationErrors)
            {
                foreach (var ve in eve.ValidationErrors)
                {
                    Log.Error($"Property: {ve.PropertyName}, Message: {ve.ErrorMessage}");
                }
            }
        }


        public void LogError(string message, Exception ex)
		{
			Log.Error($"Message: {message}, Exception: {ex.Message}");
		}

		public void LogInfo(string message)
		{
			Log.Info(message);
		}

		public void LogDebug(string message)
		{
			Log.Debug(message);
		}
	}
}
