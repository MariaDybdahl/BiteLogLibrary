using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.DTO
{
    public class RegisterRequest
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required]
        public float Height { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public required string Gender { get; set; }
    }
}
