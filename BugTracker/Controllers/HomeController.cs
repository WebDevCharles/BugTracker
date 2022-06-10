using BugTracker.Extensions;
using BugTracker.Models;
using BugTracker.Models.ViewModels;
using BugTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<BTUser> _userManager;
        private readonly IBTTicketService _ticketService;
        private readonly IBTProjectService _projectService;
        private readonly IBTCompanyInfoService _companyInfoService;
        private readonly IBTRolesService _rolesService;

        public HomeController(ILogger<HomeController> logger, 
                              UserManager<BTUser> userManager,
                              IBTTicketService ticketService,
                              IBTProjectService projectService,
                              IBTCompanyInfoService companyInfoService,
                              IBTRolesService rolesService)
        {
            _logger = logger;
            _userManager = userManager;
            _ticketService = ticketService;
            _projectService = projectService;
            _companyInfoService = companyInfoService;
            _rolesService = rolesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            //string userId = _userManager.GetUserId(User);
            if (User.Identity!.IsAuthenticated)
            {
                BTUser user = await _userManager.GetUserAsync(User);
                int companyId = User.Identity.GetCompanyId();
                var company = await _companyInfoService.GetCompanyInfoById(companyId);
                var members = await _companyInfoService.GetAllMembersAsync(companyId);
                var tickets = await _ticketService.GetAllTicketsByCompanyIdAsync(companyId);
                var projects = await _projectService.GetAllProjectsByCompanyIdAsync(companyId);

                var model = new DashboardViewModel()
                {
                    Company = company,
                    Tickets = tickets,
                    Projects = projects,
                    Members = members,
                };
                return View(model);
            }
            else
            {
                return View();
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}