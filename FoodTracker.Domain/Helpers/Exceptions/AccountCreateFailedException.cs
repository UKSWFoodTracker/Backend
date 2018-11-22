using System;

namespace FoodTracker.Domain.Helpers.Exceptions
{
    public class AccountCreateFailedException : Exception
    {
        public AccountCreateFailedException() : base("Can't create user account")
        {
        }
    }
}
