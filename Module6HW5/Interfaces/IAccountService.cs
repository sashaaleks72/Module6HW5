using Module6HW5.Models;
using Module6HW5.ViewModels.AuthModels;
using System.Threading.Tasks;

namespace Module6HW5.Interfaces
{
    public interface IAccountService
    {
        public Task<User> CreateNewAccount(RegisterModel newUser);

        public Task<User> FindAccount(LoginModel loginModel);
    }
}
