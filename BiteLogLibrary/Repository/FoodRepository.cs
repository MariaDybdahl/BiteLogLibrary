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
    public class FoodRepository : IAdd<Food>, IUpdate<Food>, IDelete<Food>, IGetAll<Food>, IGetById<Food>
    {
        private readonly BiteLogDbContext _context;

        public FoodRepository(BiteLogDbContext dbContext)
        {
            _context = dbContext;
        }
        public FoodRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BiteLogDbContext>();
            optionsBuilder
         .UseMySql(DBSecrets.ConnectionStringSimply, new MySqlServerVersion(new Version(8, 0, 30)));

            _context = new BiteLogDbContext(optionsBuilder.Options);
        }

        public async Task<Food> AddAsync(Food food)
        {
            food.Id = 0;
           
           
            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();

            return food;
        }

        public async Task<Food> UpdateAsync(int id, Food food)
        {
            Food? existingFood = await GetByIdAsync(id);


            if (existingFood == null) throw new KeyNotFoundException($"User with ID {existingFood.Id} was not found.");


            existingFood.Name = food.Name;
            existingFood.Brand = food.Brand;
            existingFood.Carbohydrates = food.Carbohydrates;
            existingFood.Fiber = food.Fiber;
            existingFood.Fat = food.Fat;
            existingFood.Protein = food.Protein;



            await _context.SaveChangesAsync();


            return existingFood;
        }

        public async Task<Food> GetByIdAsync(int id)
        {
            Food? existingFood = await _context.Foods.FirstOrDefaultAsync(f => f.Id == id);
            if (existingFood == null) throw new KeyNotFoundException($"User with ID {id} was not found.");

            return existingFood;
        }

        public async Task<Food> DeleteAsync(int id)
        {
            Food? existingFood = await GetByIdAsync(id);
            if (existingFood == null) throw new KeyNotFoundException($"User with ID {id} was not found.");
            _context.Foods.Remove(existingFood);
            _context.SaveChanges();
            return existingFood;
        }

        public async Task<List<Food>> GetAllAsync()
        {
           
            return new List<Food>(await _context.Foods.ToListAsync());
        }
    }
}
