using System;

namespace ST.Common.ErrorLogger.Exceptions
{
    public class DeleteFailedException<T> :
        Exception where T : class
    {
        public DeleteFailedException(int id) :
            base($"Delete failed for {typeof(T)} with {id}.")
        { }


        public DeleteFailedException(string id) :
            base($"Delete failed for {typeof(T)} with {id}.")
        { }
    }
}
