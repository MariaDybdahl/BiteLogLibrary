using BiteLogLibrary.Database;
using BiteLogLibrary.Interface.CRUD;
using BiteLogLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Repository
{
    public class DailyLogRepository : IAdd<DailyLog>, IGetAll<DailyLog>, IGetById<DailyLog>
    {
        private readonly BiteLogDbContext _context;

        public DailyLogRepository(BiteLogDbContext dbContext)
        {
            _context = dbContext;
        }
        public DailyLogRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BiteLogDbContext>();
            optionsBuilder.UseMySql(DBSecrets.ConnectionStringSimply, new MySqlServerVersion(new Version(8, 0, 30)));
            _context = new BiteLogDbContext(optionsBuilder.Options);
        }

        public async Task<DailyLog> AddAsync(DailyLog dailyLog)
        {
            dailyLog.Id = 0;
            await _context.DailyLogs.AddAsync(dailyLog);
            await _context.SaveChangesAsync();
            return dailyLog;
        }
       
        public async Task<DailyLog> GetByIdAsync(int id)
        {
            DailyLog? existingDailyLog = await _context.DailyLogs.FirstOrDefaultAsync(c => c.Id == id);
            if (existingDailyLog == null) throw new KeyNotFoundException($"User with ID {id} was not found.");

            return existingDailyLog;
        }

   
        public async Task<List<DailyLog>> GetAllAsync()
        {

            return new List<DailyLog>(await _context.DailyLogs.ToListAsync());
        }
    }
}
