using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Models
{
    public class CustomMeal
    {
        public string Name { get; set; }
        public int Id { get; set; }
  
        public int DailyLogId { get; set; }
        public DailyLog DailyLog { get; set; }

        [NotMapped]
        public List<CustomMealFood> CustomMealFoods { get; set; } = new();


        public CustomMeal(string name, int id)
        {
            Name = name;
            
            Id = id;
        }

        public CustomMeal()
        {
        }
    }
}
