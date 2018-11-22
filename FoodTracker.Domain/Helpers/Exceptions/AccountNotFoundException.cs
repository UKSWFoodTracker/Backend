using System;

namespace FoodTracker.Domain.Helpers.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() : base("User doesn't exist")
        {
        }
    }
}
