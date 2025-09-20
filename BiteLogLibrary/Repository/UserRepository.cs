using BiteLogLibrary.Database;
using BiteLogLibrary.Interface.CRUD;
using BiteLogLibrary.Interface.Repository;
using BiteLogLibrary.Interface.Services;
using BiteLogLibrary.Models;
using BiteLogLibrary.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Repository
{
    public class UserRepository :  IUserRepository
    {

        private readonly BiteLogDbContext _context;
        

        public UserRepository(BiteLogDbContext dbContext)
        {
            _context = dbContext;
            
        }
        public UserRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BiteLogDbContext>();
            optionsBuilder.UseMySql(DBSecrets.ConnectionStringSimply, new MySqlServerVersion(new Version(8, 0, 30)));
            _context = new BiteLogDbContext(optionsBuilder.Options);
        }
        public async Task<User> DeleteAsync(int id)
        {
            User? existingUser = await GetByIdAsync(id);
            if (existingUser == null) throw new KeyNotFoundException($"User with ID {id} was not found.");
            _context.Users.Remove(existingUser);
            _context.SaveChanges();
            return existingUser;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return  new List<User>(await _context.Users.ToListAsync());
        }

        public async Task<User?> GetByIdAsync(int id)
        {
           
            //if (existingUser == null) throw new KeyNotFoundException($"User with ID {id} was not found.");
            
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<User> UpdateAsync(int id, User user)
        {
            User? existingUser = await GetByIdAsync(id);

       
            if (existingUser == null) throw new KeyNotFoundException($"User with ID {id} was not found.");

        
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Username = user.Username;
            existingUser.DateOfBirth = user.DateOfBirth;



            await _context.SaveChangesAsync();

            
            return existingUser;
        }

        public async Task<User> AddAsync(User user)
        {
         
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
     
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }


    }
}
