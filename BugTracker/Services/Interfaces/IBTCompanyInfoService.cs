using BugTracker.Models;

namespace BugTracker.Services.Interfaces
{
    public interface IBTCompanyInfoService
    {
        public Task<List<BTUser>> GetAllMembersAsync(int companyId);

    }
}
