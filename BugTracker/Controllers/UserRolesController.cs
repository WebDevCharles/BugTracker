using BugTracker.Extensions;
using BugTracker.Models;
using BugTracker.Models.ViewModels;
using BugTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly IBTCompanyInfoService _companyInfoService;
        private readonly IBTRolesService _rolesService;

        public UserRolesController(IBTCompanyInfoService companyInfoService, IBTRolesService rolesService)
        {
            _companyInfoService = companyInfoService;
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles()
        {
            List<ManageUserRolesViewModel> model = new();
            int companyId = User.Identity!.GetCompanyId();

            List<BTUser> btUsers = await _companyInfoService.GetAllMembersAsync(companyId);

            foreach (BTUser btUser in btUsers)
            {
                ManageUserRolesViewModel viewModel = new();
                viewModel.BTUser = btUser;

                IEnumerable<string> currentRoles = await _rolesService.GetUserRolesAsync(btUser);
                viewModel.Roles = new MultiSelectList(await _rolesService.GetBTRolesAsync(), "Name", "Name", currentRoles);

                model.Add(viewModel);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(ManageUserRolesViewModel member)
        {
            int companyId = User.Identity!.GetCompanyId();

            BTUser? btUser = (await _companyInfoService.GetAllMembersAsync(companyId)).FirstOrDefault(c => c.Id == member.BTUser!.Id);
            IEnumerable<string> currentRoles = await _rolesService.GetUserRolesAsync(btUser!);

            string? selectedUserRole = member.SelectedRoles!.FirstOrDefault();

            if (!string.IsNullOrEmpty(selectedUserRole))
            {
                if (await _rolesService.RemoveUserFromRolesAsync(btUser!, currentRoles))
                {
                    await _rolesService.AddUserToRoleAsync(btUser!, selectedUserRole);
                }
            }
            else
            {
                return RedirectToAction(nameof(ManageUserRoles));
            }

            return RedirectToAction(nameof(ManageUserRoles));
        }
    }
}
