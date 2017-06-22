using Learner.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learner.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        User GetUser(int id);
        void Update(User user);
        void Delete(User user);

    }
}