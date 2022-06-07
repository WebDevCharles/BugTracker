using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Models.ViewModels
{
    public class DeveloperUserViewModel
    {
        public SelectList? Developers { get; set; }
        public string? DeveloperId { get; set; }
        public Ticket? Ticket { get; set; }

    }
}
