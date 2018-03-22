using System;

namespace ST.Common.ErrorLogger.Exceptions
{
    
    public class AddFailedException<T> :
        Exception where T : class
    {
        public AddFailedException() :
            base($"Adding failed for {typeof(T)}.")
        { }
    }
}
