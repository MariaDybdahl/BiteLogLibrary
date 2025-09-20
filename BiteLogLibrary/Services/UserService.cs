using BiteLogLibrary.DTO;
using BiteLogLibrary.Helper;
using BiteLogLibrary.Interface.Repository;
using BiteLogLibrary.Interface.Services;
using BiteLogLibrary.Models;
using BiteLogLibrary.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly CustomPasswordHasher _passwordHasher;

      
        public UserService(IUserRepository userRepository, CustomPasswordHasher passwordHasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

    

        private async Task<string?> ValidateEmailNotTaken(string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            var existing = await _userRepository.GetByEmailAsync(email);
          
            if (existing is not null)
                throw new InvalidOperationException("Email already exists");

            return email;

        }
        private async Task ValidateUsernameNotTaken(string username)
        {
            if (await _userRepository.GetByUsernameAsync(username) != null)
            {
                throw new InvalidOperationException("Username already exists");
            }
        }

        public async Task<User> RegisterAsync(RegisterRequest registerRequest)
        {
          await ValidateEmailNotTaken(registerRequest.Email);
          await  ValidateUsernameNotTaken(registerRequest.Username);
            var user = new User 
            {
                Id = 0,
                SignupDate = DateTime.Now.ToString(),
                Password = _passwordHasher.HashPassword(registerRequest.Password),
                Email = registerRequest.Email,
                LastName = registerRequest.LastName,
                FirstName = registerRequest.FirstName,
                Username = registerRequest.Username,
                Gender = registerRequest.Gender,
                Height = registerRequest.Height,
                Weight = registerRequest.Weight,

            };
         
       
          return await _userRepository.AddAsync(user);
        }



    }
}
