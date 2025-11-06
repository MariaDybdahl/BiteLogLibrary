using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.DTO
{
    public class ResetPasswordRequest
    {
        public int UserId { get; set; }
        public string Token { get; set; } = default!;
        public string NewPassword { get; set; } = default!;

    }
}
