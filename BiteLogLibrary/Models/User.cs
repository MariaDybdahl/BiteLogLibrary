using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BiteLogLibrary.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? SignupDate { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        [NotMapped]
        public int? Age => CalculateAge();
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public double? DailyCalorieGoal { get; set; }
        public double? ProteinGoal { get; set; }
        public double? CarbGoal { get; set; }
        public double? FatGoal { get; set; }
      

        //[NotMapped]
        //public List<Food> LoggedFoods { get; set; } = new List<Food>();
        //[NotMapped]
        //public List<DailyLog> Logs { get; set; } = new List<DailyLog>();

        public User(int id, string username, string firstName, string lastName, string email, string passwordHash, float weight, float height, DateTime dayOfBirth, string gender, double dailyCalorieGoal, double proteinGoal, double carbGoal, double fatGoal) //, List<Food> loggedFoods, List<DailyLog> logs
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = passwordHash;
            Weight = weight;
            Height = height;
            DateOfBirth = dayOfBirth;
            
            Gender = gender;
            DailyCalorieGoal = dailyCalorieGoal;
            ProteinGoal = proteinGoal;
            CarbGoal = carbGoal;
            FatGoal = fatGoal;
            //LoggedFoods = loggedFoods;
            //Logs = logs;
        }

        public User()
        {
        }

    

        private int CalculateAge()
        {
            var today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;

            // Træk 1 fra hvis fødselsdagen ikke er nået i år endnu
            if (DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

    }

}
