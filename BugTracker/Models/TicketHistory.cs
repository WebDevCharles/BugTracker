using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class TicketHistory
    {
        // Primary Key
        public int Id { get; set; }

        [DisplayName("Updated Ticket Property")]
        public string? Property { get; set; }

        [DisplayName("Description Of Change(s)")]
        public string? Description { get; set; }


        [DataType(DataType.Date)]
        public DateTime Created { get; set; }


        [DisplayName("Previous Value")]
        public string? OldValue { get; set; }

        [DisplayName("Current Value")]
        public string? NewValue { get; set; }


        public int TicketId { get; set; }


        [Required]
        public string? UserId { get; set; }


        // Navigational Properties
        public virtual Ticket? Ticket { get; set; }
        public virtual BTUser? User { get; set; }

    }
}
