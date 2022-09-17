using Module6HW5.Models;
using System.Threading.Tasks;

namespace Module6HW5.Interfaces
{
    public interface IAccountDataProvider
    {
        public Task AddUser(User user);

        public Task<User> GetUserByEmail(string email);

        public Task<User> GetUserByEmailAndPass(string email, string pass);
    }
}
