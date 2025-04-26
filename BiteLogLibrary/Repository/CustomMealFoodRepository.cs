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
    public class CustomMealFoodRepository : IAdd<CustomMealFood>, IGetById<CustomMealFood>, IGetAll<CustomMealFood>
    {
        private readonly BiteLogDbContext _context;

        public CustomMealFoodRepository(BiteLogDbContext dbContext)
        {
            _context = dbContext;
        }
        public CustomMealFoodRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BiteLogDbContext>();
            optionsBuilder.UseMySql(DBSecrets.ConnectionStringSimply, new MySqlServerVersion(new Version(8, 0, 30)));
            _context = new BiteLogDbContext(optionsBuilder.Options);
        }

        public async Task<CustomMealFood> AddAsync(CustomMealFood customMealFood)
        {
            customMealFood.Id = 0;
            await _context.CustomMealFoods.AddAsync(customMealFood);
            await _context.SaveChangesAsync();
            return customMealFood;
        }
        public async Task<CustomMealFood> GetByIdAsync(int id)
        {
            CustomMealFood? existingCustomMealFood = await _context.CustomMealFoods.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCustomMealFood == null) throw new KeyNotFoundException($"User with ID {id} was not found.");

            return existingCustomMealFood;
        }
        public async Task<List<CustomMealFood>> GetAllAsync()
        {

            return new List<CustomMealFood>(await _context.CustomMealFoods.ToListAsync());
        }
    }
}
