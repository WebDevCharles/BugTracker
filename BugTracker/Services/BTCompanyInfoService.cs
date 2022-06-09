﻿using BugTracker.Data;
using BugTracker.Models;
using BugTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Services
{
    public class BTCompanyInfoService : IBTCompanyInfoService
    {
        private readonly ApplicationDbContext _context;

        public BTCompanyInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BTUser>> GetAllMembersAsync(int companyId)
        {
            try
            {
                List<BTUser> members = new();
                members = (await _context.Companies.Include(c => c.Members).FirstOrDefaultAsync(c => c.Id == companyId))!.Members.ToList();

                return members;


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
