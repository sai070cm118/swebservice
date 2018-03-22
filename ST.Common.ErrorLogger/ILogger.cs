using System;
using System.Data.Entity.Validation;

namespace ST.Common.Logger
{
	public interface ILogger
	{
		void LogError(string message, Exception ex);

        void LogInfo(string message);
	}
}