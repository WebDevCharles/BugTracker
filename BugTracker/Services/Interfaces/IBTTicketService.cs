using BugTracker.Models;

namespace BugTracker.Services.Interfaces
{
    public interface IBTTicketService
    {
        public Task AddNewTicketAsync(Ticket ticket);
        public Task ArchiveTicketAsync(Ticket ticket);
        public Task<List<Ticket>> GetAllTicketsByCompanyIdAsync(int companyId);
        public Task UpdateTicketAsync(Ticket ticket);
        public Task<Ticket> GetTicketByIdAsync(int id);
    }
}
