using Module6HW5.Interfaces;
using Module6HW5.ViewModels.AuthModels;
using Module6HW5.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Module6HW5.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataProvider _accountDataProvider;
        private readonly IRoleDataProvider _roleDataProvider;

        public AccountService(IAccountDataProvider accountDataProvider, IRoleDataProvider roleDataProvider)
        {
            _accountDataProvider = accountDataProvider;
            _roleDataProvider = roleDataProvider;
        }

        public async Task<User> CreateNewAccount(RegisterModel registerModel)
        {
            var user = await _accountDataProvider.GetUserByEmail(registerModel.Email);

            if (user == null)
            {
                var defaultRole = await _roleDataProvider.GetRoleByName("user");

                if (defaultRole != null)
                {
                    var newUser = new User { Email = registerModel.Email, Password = registerModel.Password, Role = defaultRole };
                    await _accountDataProvider.AddUser(newUser);

                    return newUser;
                }
            }

            return null;
        }

        public async Task<User> FindAccount(LoginModel loginModel)
        {
            var user = await _accountDataProvider.GetUserByEmailAndPass(loginModel.Email, loginModel.Password);

            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}
