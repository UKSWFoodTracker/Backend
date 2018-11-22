using System.Collections.Generic;
using System.Threading.Tasks;
using FoodTracker.Model;

namespace FoodTracker.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<Account> AuthenticateAsync(string username, string password);
        IEnumerable<Account> GetAll();
        Account GetById(int id);
        Account Create(Account account, string password);
    }
}
