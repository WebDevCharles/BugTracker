using BugTracker.Data;
using BugTracker.Models;
using BugTracker.Models.Enums;
using BugTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Services
{
    public class BTProjectService : IBTProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBTRolesService _rolesService;

        public BTProjectService(ApplicationDbContext context,
                                            IBTRolesService rolesService)
        {
            _context = context;
            _rolesService = rolesService;
        }

        #region Add New Project
        public async Task AddNewProjectAsync(Project project)
        {
            try
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Archive Project
        public async Task ArchiveProjectAsync(Project project)
        {
            try
            {
                project.Archived = true;
                await UpdateProjectAsync(project);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Projects By Company Id
        public async Task<List<Project>> GetAllProjectsByCompanyIdAsync(int companyId)
        {
            try
            {
                List<Project>? projects = new();

                projects = await _context.Projects
                                    .Where(p => p.CompanyId == companyId && p.Archived == false)
                                    .Include(p => p.Members)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.Comments)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.Attachments)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.History)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.Notifications)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.DeveloperUser)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.SubmitterUser)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.TicketStatus)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.TicketPriority)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.TicketType)
                                    .Include(p => p.ProjectPriority)!
                                    .ToListAsync();

                return projects;

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get Project Manager
        public async Task<BTUser> GetProjectManagerAsync(int projectId)
        {
            try
            {
                Project? project = await _context.Projects
                                                                .Include(p => p.Members)
                                                                .FirstOrDefaultAsync(p => p.Id == projectId);

                foreach (BTUser member in project?.Members!)
                {
                    if (await _rolesService.IsUserInRoleAsync(member, nameof(BTRoles.ProjectManager)))
                    {
                        return member;
                    }
                }
                return null!;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Add User To Project
        public async Task<bool> AddUserToProjectAsync(string userId, int projectId)
        {
            BTUser? btUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (btUser != null)
            {
                Project? project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
                if (!await IsUserOnProjectAsync(userId, projectId))
                {
                    try
                    {
                        project!.Members!.Add(btUser);
                        await _context.SaveChangesAsync();

                        return true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
             
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Remove User From Project
        public async Task<bool> RemoveUserFromProjectAsync(string userId, int projectId)
        {
            try
            {
                BTUser? btUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                Project? project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

                try
                {
                    if (await IsUserOnProjectAsync(userId, projectId))
                    {
                        project?.Members?.Remove(btUser!);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Is User On Project
        public async Task<bool> IsUserOnProjectAsync(string userId, int projectId)
        {
            try
            {
                Project? project = await _context.Projects
                                                                .Include(p => p.Members)
                                                                .FirstOrDefaultAsync(p => p.Id == projectId);
                bool result = false;

                if (project != null)
                {
                    result = project.Members.Any(m => m.Id == userId);
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Add Project Manager
        public async Task<bool> AddProjectManagerAsync(string userId, int projectId)
        {
            BTUser currentPM = await GetProjectManagerAsync(projectId);
            if (currentPM == null)
            {
                try
                {
                    // Remove Project Managers



                }
                catch (Exception)
                {

                    throw;
                }
            }

            // Add the new PM
            try
            {
                // Add Project Manager
                return await AddUserToProjectAsync(userId, projectId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Remove Project Manager
        public async Task RemoveProjectManagerAsync(int projectId)
        {
            try
            {
                Project? project = await _context.Projects
                                                              .Include(p => p.Members)
                                                               .FirstOrDefaultAsync(p => p.Id == projectId);

                foreach(BTUser member in project?.Members!)
                {
                    if(await _rolesService.IsUserInRoleAsync(member, nameof(BTRoles.ProjectManager)))
                    {
                        // Remove User from Project
                        await RemoveUserFromProjectAsync(member.Id, project.Id);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Update Project
        public async Task UpdateProjectAsync(Project project)
        {
            try
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get Project By Id
        public async Task<Project> GetProjectByIdAsync(int id, int companyId)
        {
            try
            {
                Project? project = new();
                project = await _context.Projects
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.TicketPriority)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.TicketStatus)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.TicketType)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.DeveloperUser)
                                    .Include(p => p.Tickets)!
                                        .ThenInclude(t => t.SubmitterUser)
                                    .Include(p => p.Members)!
                                    .Include(p => p.ProjectPriority)!
                                    .FirstOrDefaultAsync(p => p.Id == id && p.CompanyId == companyId);

                return project!;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
