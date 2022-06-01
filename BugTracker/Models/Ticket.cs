using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        [DisplayName("Title")]
        public string? Title { get; set; }

        [Required]
        [DisplayName("Description")]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created")]
        public DateTime Created { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated")]
        public DateTime? Updated { get; set; }

        [DisplayName("Archived")]
        public bool Archived { get; set; }

        [DisplayName("Archived By Project")]
        public bool ArchivedByProject { get; set; }

        public int ProjectId { get; set; }

        public int TicketPriorityId { get; set; }

        public int TicketStatusId { get; set; }

        public int TicketTypeId { get; set; }


        // Foreign Keys
        public string? SubmitterUserId { get; set; }

        public string? DeveloperUserId { get; set; }


        // Navigational Properties
        public virtual Project? Project { get; set; }

        [DisplayName("Priority")]
        public virtual TicketPriority? TicketPriority { get; set; }

        [DisplayName("Status")]
        public virtual TicketStatus? TicketStatus { get; set; }

        [DisplayName("Type")]
        public virtual TicketType? TicketType { get; set; }

        [DisplayName("Submitted By")]
        public virtual BTUser? SubmitterUser { get; set; }

        [DisplayName("Ticket Developer")]
        public virtual BTUser? DeveloperUser { get; set; }


        // Navigational Collections
        public virtual ICollection<TicketComment> Comments { get; set; } = new HashSet<TicketComment>();
        public virtual ICollection<TicketAttachment> Attachments { get; set; } = new HashSet<TicketAttachment>();
        public virtual ICollection<TicketHistory> History { get; set; } = new HashSet<TicketHistory>();
        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();

    }
}
