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
    public class CustomMealRepository : IAdd<CustomMeal>, IUpdate<CustomMeal>, IDelete<CustomMeal>, IGetAll<CustomMeal>, IGetById<CustomMeal>
    {
        private readonly BiteLogDbContext _context;

        public CustomMealRepository(BiteLogDbContext dbContext)
        {
            _context = dbContext;
        }
        public CustomMealRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BiteLogDbContext>();
            optionsBuilder.UseMySql(DBSecrets.ConnectionStringSimply, new MySqlServerVersion(new Version(8, 0, 30)));
            _context = new BiteLogDbContext(optionsBuilder.Options);
        }

        public async Task<CustomMeal> AddAsync(CustomMeal customMeal)
        {
            customMeal.Id = 0;
            await _context.CustomMeals.AddAsync(customMeal);
            await _context.SaveChangesAsync();
            return customMeal;
        }
        //TODO
        public async Task<CustomMeal> UpdateAsync(int id, CustomMeal customMeal)
        {
            CustomMeal? existingCustomMeal = await GetByIdAsync(id);


                if (existingCustomMeal == null) throw new KeyNotFoundException($"User with ID {id} was not found.");




                await _context.SaveChangesAsync();


                return existingCustomMeal;
            
        }
        public async Task<CustomMeal> GetByIdAsync(int id)
        {
            CustomMeal? existingCustomMeal = await _context.CustomMeals.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCustomMeal == null) throw new KeyNotFoundException($"User with ID {id} was not found.");

            return existingCustomMeal;
        }
        public async Task<List<CustomMeal>> GetAllAsync()
        {

            return new List<CustomMeal>(await _context.CustomMeals.ToListAsync());
        }

        public async Task<CustomMeal> DeleteAsync(int id)
        {
            CustomMeal? existingCustomMeal = await GetByIdAsync(id);
            if (existingCustomMeal == null) throw new KeyNotFoundException($"User with ID {id} was not found.");
            _context.CustomMeals.Remove(existingCustomMeal);
            _context.SaveChanges();
            return existingCustomMeal;
        }

      
    }
}
