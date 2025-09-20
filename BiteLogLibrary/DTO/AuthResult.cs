using BiteLogLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.DTO
{
    public class AuthResult
    {

        public bool Success { get; init; }
        public string? Message { get; init; }
        public UserDto? User { get; init; }   
        public string? Token { get; init; } 
    }
}
