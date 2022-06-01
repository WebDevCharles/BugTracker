using BugTracker.Models;

namespace BugTracker.Services.Interfaces
{
    public interface IBTRolesService
    {
        public Task<bool> IsUserInRoleAsync(BTUser user, string roleName);
        public Task<List<BTUser>> GetUsersInRoleAsync(string roleName, int companyId);
    }
}
