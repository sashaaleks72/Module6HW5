using Microsoft.EntityFrameworkCore;
using Module6HW5.DB;
using Module6HW5.Interfaces;
using Module6HW5.Models;
using System.Threading.Tasks;

namespace Module6HW5.Providers
{
    public class RoleDataProvider : IRoleDataProvider
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleDataProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

            return role;
        }
    }
}
