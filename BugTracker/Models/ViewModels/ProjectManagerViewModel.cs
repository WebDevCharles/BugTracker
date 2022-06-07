using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Models.ViewModels
{
    public class ProjectManagerViewModel
        {
            public Project? Project { get; set; } = new();
            public SelectList? PMList { get; set; }
            public string? PMID { get; set; }
        }
}
