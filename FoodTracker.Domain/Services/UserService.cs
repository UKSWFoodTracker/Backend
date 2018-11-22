using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTracker.Domain.Helpers.Exceptions;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.Model;
using Microsoft.AspNetCore.Identity;

namespace FoodTracker.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Account> _userManager;

        public UserService(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Account> AuthenticateAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new AccountNotFoundException();

            var validPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!validPassword)
                throw new InvalidPasswordException();

            return user;
        }

        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Account Create(Account account, string password)
        {
            throw new NotImplementedException();
        }
    }
}
