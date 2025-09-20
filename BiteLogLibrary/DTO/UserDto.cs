using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BiteLogLibrary.DTO
{
    public class UserDto
    {

        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? SignupDate { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = default!;
        public double? DailyCalorieGoal { get; set; }
        public double? ProteinGoal { get; set; }
        public double? CarbGoal { get; set; }
        public double? FatGoal { get; set; }
        public int? Age { get; set; }
    }

}


