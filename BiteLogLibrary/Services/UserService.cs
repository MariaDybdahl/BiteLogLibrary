using BiteLogLibrary.DTO;
using BiteLogLibrary.Helper;
using BiteLogLibrary.Interface.Repository;
using BiteLogLibrary.Interface.Services;
using BiteLogLibrary.Models;
using BiteLogLibrary.Repository;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
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
        // private readonly CustomPasswordHasher _passwordHasher;
        private readonly IPasswordHasher<User> _hasher;


        public UserService(IUserRepository userRepository, IPasswordHasher<User> hasher) //CustomPasswordHasher passwordHasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            //_passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
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
                Email = registerRequest.Email,
                LastName = registerRequest.LastName,
                FirstName = registerRequest.FirstName,
                Username = registerRequest.Username,
                Gender = registerRequest.Gender,
                Height = registerRequest.Height,
                Weight = registerRequest.Weight,
                DateOfBirth = registerRequest.DateOfBirth

            };
            user.Password = _hasher.HashPassword(user, registerRequest.Password);
          
         


            return await _userRepository.AddAsync(user);
        }

        public async Task<AuthResult> AuthenticateAsync(LoginRequest registerRequest)
        {
            if (string.IsNullOrWhiteSpace(registerRequest.Identifier) || string.IsNullOrWhiteSpace(registerRequest.Password))
                return await Task.FromResult(new AuthResult { Success = false, Message = "Identifier and password are required." });

            // slå op på email eller username (trim + evt. ToLower)
            var id = registerRequest.Identifier.Trim();
            var user = await _userRepository.GetByEmailAsync(id)
                   ?? await _userRepository.GetByUsernameAsync(id);

            if (user is null)
                return await Task.FromResult(new AuthResult { Success = false, Message = "Invalid credentials." });

            var result = _hasher.VerifyHashedPassword(user, user.Password, registerRequest.Password);
            if (result == PasswordVerificationResult.Failed)
                return await Task.FromResult(new AuthResult { Success = false, Message = "Invalid credentials." });

            // Opgrader hash hvis nødvendigt
            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = _hasher.HashPassword(user, registerRequest.Password);
                await _userRepository.UpdatePasswordAsync(user); // tilføj i repo (se nedenfor)
            }

            // Returnér kun sikre felter (ingen password)
            user.Password = string.Empty;
            
            return new AuthResult { Success = true, Message = "Logged in", User = user.ToDto() };
        }

        public bool VerifyPassword(User user, string password)
        {
            var r = _hasher.VerifyHashedPassword(user, user.Password, password);
            return r is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;

        }
    }
}
