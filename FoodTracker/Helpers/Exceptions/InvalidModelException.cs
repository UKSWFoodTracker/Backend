using System;

namespace FoodTracker.Helpers.Exceptions
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException() : base("Passed data are invalid")
        {
        }
    }
}
