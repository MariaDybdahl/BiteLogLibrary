using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.DTO
{
    public class LoginRequest
    {
        public required string Identifier { get; set; } // email eller username
        public required string Password { get; set; }
    }
}
