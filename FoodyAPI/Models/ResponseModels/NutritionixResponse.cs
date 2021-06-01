using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodyAPI.Models
{
    public class NutritionixResponse
    {

        public List<Food> foods { get; set; }


        
    }
    public class Food
    {
        public string food_name { get; set; }
        public double nf_calories { get; set; }
        public double nf_total_fat { get; set; }
        public double nf_total_carbohydrate { get; set; }
        public double nf_protein { get; set; }
        public Product MakeProduct()
        {
            Product pr = new Product();

            var prod = pr.SetProduct
                 (
                 food_name,
                 nf_calories,
                 nf_protein,
                 nf_total_carbohydrate,
                 nf_total_fat
                 );

            return prod;
        }
    }
}
