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
        private IUserRepository _userRepository;
        private object @object;
        private readonly PasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, PasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public UserService()
        {
        }

        public UserService(IUserRepository @object)
        {
            this.@object = @object;
        }

        private async Task<string?> ValidateEmailNotTaken(string email)
        {
          User? existingEmail =  await _userRepository.GetByEmailAsync(email);
          
            if (existingEmail == null) 
            {
                return email; 
            }
             throw new InvalidOperationException("Email already exists"); 

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
