using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class TicketStatus
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Ticket Status Name")]
        public string? Name { get; set; }
    }
}
