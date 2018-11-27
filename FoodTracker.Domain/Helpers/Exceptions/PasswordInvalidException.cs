using System;

namespace FoodTracker.Domain.Helpers.Exceptions
{
    public class PasswordInvalidException : Exception
    {
        public PasswordInvalidException() : base("Password is invalid")
        {
        }
    }
}
