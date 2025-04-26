using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiteLogLibrary.Models
{
    public class CustomMealFood
    {
        public int Id { get; set; }

        public int CustomMealId { get; set; }
        public CustomMeal CustomMeal { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }

        public double Quantity { get; set; }


        public CustomMealFood(int id, int customMealId, CustomMeal customMeal, int foodId, Food food, double quantity)
        {
            Id = id;
            CustomMealId = customMealId;
            CustomMeal = customMeal;
            FoodId = foodId;
            Food = food;
            Quantity = quantity;
        }

        public CustomMealFood()
        {
        }
    }
}
