using BugTracker.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models
{
    public class Project
    {
        // Primary Key
        public int Id { get; set; }

        public int CompanyId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        [DisplayName("Project Name")]
        public string? Name { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        [DisplayName("Description")]
        public string? Description { get; set; }


        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTime Created { get; set; }


        [DataType(DataType.Date)]
        [DisplayName("Project Start Date")]
        public DateTime StartDate { get; set; }


        [DataType(DataType.Date)]
        [DisplayName("Project End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Project Priority")]
        public int ProjectPriorityId { get; set; }


        [NotMapped]
        [DataType(DataType.Upload)]
        [MaxFileSize(1024 * 1024)]
        public IFormFile? ImageFormFile { get; set; }


        [DisplayName("File Name")]
        public string? ImageFileName { get; set; }


        [DisplayName("Project Image")]
        public byte[]? ImageFileData { get; set; }


        [DisplayName("File Extension")]
        public string? ImageContentType { get; set; }

        public bool Archived { get; set; }


        // Navigational Properties
        public virtual Company? Company { get; set; }

        [DisplayName("Priority")]
        public virtual ProjectPriority? ProjectPriority { get; set; }


        // Navigational Collections
        public virtual ICollection<BTUser> Members { get; set; } = new HashSet<BTUser>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

    }
}
