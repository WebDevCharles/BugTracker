using BugTracker.Models;

namespace BugTracker.Services.Interfaces
{
    public interface IBTProjectService
    {
        public Task AddNewProjectAsync(Project project);
        public Task<bool> AddProjectManagerAsync(string userId, int projectId);
        public Task<BTUser> GetProjectManagerAsync(int projectId);
        public Task<List<Project>> GetUserProjectsAsync(string userId);
        public Task<bool> AddUserToProjectAsync(string userId, int projectId);
        public Task<bool> RemoveUserFromProjectAsync(string userId, int projectId);
        public Task RemoveProjectManagerAsync(int projectId);
        public Task<bool> IsUserOnProjectAsync(string userId, int projectId);
        public Task ArchiveProjectAsync(Project project);
        public Task<List<BTUser>> GetProjectMembersByRoleAsync(int projectId, string roleName);
        public Task<List<Project>> GetUnassignedProjectsAsync(int companyId);
        public Task<List<BTUser>> GetAllProjectMembersExceptPMAsync(int projectId);
        public Task<List<BTUser>> GetUsersNotOnProjectAsync(int projectId, int companyId);
        public Task <List<Project>> GetArchivedProjectsByCompanyIdAsync(int companyId);
        public Task RestoreProjectAsync(Project project);
        public Task<List<Project>> GetAllProjectsByCompanyIdAsync(int companyId);
        public Task UpdateProjectAsync(Project project);
        public Task<Project> GetProjectByIdAsync(int id, int companyId);
    }
}
