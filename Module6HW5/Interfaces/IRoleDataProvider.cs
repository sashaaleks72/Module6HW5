using Module6HW5.Models;
using System.Threading.Tasks;

namespace Module6HW5.Interfaces
{
    public interface IRoleDataProvider
    {
        public Task<Role> GetRoleByName(string roleName);
    }
}
