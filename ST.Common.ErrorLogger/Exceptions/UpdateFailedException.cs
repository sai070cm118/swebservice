using System;

namespace ST.Common.ErrorLogger.Exceptions
{
    public class UpdateFailedException<T> :
        Exception where T : class
    {
        public UpdateFailedException(int id) :
            base($"Update failed for {typeof(T)} with {id}.")
        { }


        public UpdateFailedException(string name) :
            base($"Update failed for {typeof(T)} with {name}.")
        { }
    }
}
