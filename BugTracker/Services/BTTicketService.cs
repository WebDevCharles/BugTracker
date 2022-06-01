using BugTracker.Data;
using BugTracker.Models;
using BugTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Services
{
    public class BTTicketService : IBTTicketService
    {
        private readonly ApplicationDbContext _context;

        public BTTicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Add New Ticket
        public async Task AddNewTicketAsync(Ticket ticket)
        {
            try
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Archive Ticket
        public async Task ArchiveTicketAsync(Ticket ticket)
        {
            try
            {
                ticket.Archived = true;
                await UpdateTicketAsync(ticket);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get All Tickets By Company Id
        public async Task<List<Ticket>> GetAllTicketsByCompanyIdAsync(int companyId)
        {
            List<Ticket> tickets = new List<Ticket>();

            tickets = await _context.Projects
                            .Where(p => p.CompanyId == companyId)
                            .SelectMany(p => p.Tickets)
                                .Include(t => t.Comments)
                                .Include(t => t.DeveloperUser)
                                .Include(t => t.History)
                                .Include(t => t.SubmitterUser)
                                .Include(t => t.TicketPriority)
                                .Include(t => t.TicketStatus)
                                .Include(t => t.TicketType)
                                .Include(t => t.Project)
                                .ToListAsync();
                                    

            return tickets;
        }
        #endregion

        #region Update Ticket
        public async Task UpdateTicketAsync(Ticket ticket)
        {
            try
            {
                _context.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Get Ticket By Id
        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            try
            {
                Ticket? ticket = new();
                ticket = await _context.Tickets
                                        .Include(t => t.DeveloperUser)
                                        .Include(t => t.Project)
                                        .Include(t => t.SubmitterUser)
                                        .Include(t => t.TicketPriority)
                                        .Include(t => t.TicketStatus)
                                        .Include(t => t.TicketType)
                                        .Include(t => t.Comments)
                                        .Include(t => t.Attachments)
                                        .Include(t => t.History)
                                        .FirstOrDefaultAsync(t => t.Id == id);

                return ticket;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
