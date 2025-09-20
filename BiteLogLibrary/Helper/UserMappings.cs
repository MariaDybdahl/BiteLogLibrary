using BiteLogLibrary.DTO;
using BiteLogLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Helper
{
    public static class UserMappings
    {
        public static UserDto ToDto(this User u) => new UserDto
        {
            Id = u.Id,
            Username = u.Username,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            SignupDate = u.SignupDate,
            Weight = u.Weight,
            Height = u.Height,
            DateOfBirth = u.DateOfBirth,
            Gender = u.Gender,
            DailyCalorieGoal = u.DailyCalorieGoal,
            ProteinGoal = u.ProteinGoal,
            CarbGoal = u.CarbGoal,
            FatGoal = u.FatGoal,
            Age = u.Age
        };
    }
}
