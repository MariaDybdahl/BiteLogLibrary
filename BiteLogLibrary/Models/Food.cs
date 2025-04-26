namespace BiteLogLibrary.Models
{
    public class Food
    {
        // Grundlæggende info
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double ServingSizeGrams { get; set; }
        public string ServingUnit { get; set; }

        // Kalorier
        public double Calories { get; set; }

        // Makronæringsstoffer (pr. portion)
        public double Protein { get; set; }
        public double Carbohydrates { get; set; }
        public double Sugar { get; set; }
        public double Fat { get; set; }
        public double SaturatedFat { get; set; }
        public double Fiber { get; set; }

        // Mikronæringsstoffer (pr. portion)
        public double SodiumMg { get; set; }
        public double PotassiumMg { get; set; }
        public double CalciumMg { get; set; }
        public double IronMg { get; set; }
        public double VitaminCMg { get; set; }
        public double VitaminAMcg { get; set; }
        public string? QrCodeContent { get; set; }

        // Brugerdefineret?
        public bool IsCustom { get; set; }

        public List<CustomMealFood> CustomMealFoods { get; set; } = new();



        public Food(string name, string brand, double servingSizeGrams, string servingUnit, double calories, double protein, double carbohydrates, double sugar, double fat, double saturatedFat, double fiber, double sodiumMg, double potassiumMg, double calciumMg, double ironMg, double vitaminCMg, double vitaminAMcg, bool isCustom, int id)
        {
            Name = name;
            Brand = brand;
            ServingSizeGrams = servingSizeGrams;
            ServingUnit = servingUnit;
            Calories = calories;
            Protein = protein;
            Carbohydrates = carbohydrates;
            Sugar = sugar;
            Fat = fat;
            SaturatedFat = saturatedFat;
            Fiber = fiber;
            SodiumMg = sodiumMg;
            PotassiumMg = potassiumMg;
            CalciumMg = calciumMg;
            IronMg = ironMg;
            VitaminCMg = vitaminCMg;
            VitaminAMcg = vitaminAMcg;
          
            IsCustom = isCustom;
       
            Id = id;
        }

        public Food()
        {
        }
    }

}
