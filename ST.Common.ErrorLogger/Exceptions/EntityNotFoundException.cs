using System;

namespace ST.Common.ErrorLogger.Exceptions
{
	public class EntityNotFoundException<T> :
		Exception where T : class
	{
		public EntityNotFoundException(long id) :
			base($"{typeof(T)} not found with {id}.")
		{ }

		public EntityNotFoundException(string name) :
			base($"{typeof(T)} not found with {name}.")
		{ }
	}
}
