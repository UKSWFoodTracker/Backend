using System;

namespace FoodTracker.Domain.Helpers.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("Password is invalid")
        {
        }
    }
}
