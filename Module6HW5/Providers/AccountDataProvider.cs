using Module6HW5.Interfaces;
using Module6HW5.DB;
using Module6HW5.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Module6HW5.Providers
{
    public class AccountDataProvider : IAccountDataProvider
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountDataProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<User> GetUserByEmailAndPass(string email, string pass)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == pass);

            return user;
        }
    }
}
