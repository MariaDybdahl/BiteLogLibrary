using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.DTO
{
    public class ForgotPasswordRequest  
    { 
        public string Identifier { get; set; } = default!;  // email eller username
    }
}
