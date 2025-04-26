using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Models
{
    public class DailyLog
    {
        public DateTime LogDate { get; set; }
       
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [NotMapped]
        public List<CustomMeal> Meals { get; set; } = new List<CustomMeal>();

        public DailyLog(DateTime date, List<CustomMeal> meals, int id)
        {
            LogDate = date;
            Meals = meals;
            Id = id;
        }

        public DailyLog()
        {
        }
    }
}
