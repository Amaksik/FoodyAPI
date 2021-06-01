using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodyAPI.Models
{
    public class Product
    {


        [Key]
        public int ProdId { get; set; }


        public string Name { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }

        public int? Id { get; set; }

        [JsonIgnore]
        public User User { get; set; }





        //methods
         public Product SetProduct(string name, double cal, double prot, double fat, double carb )
        {
            Product p = new Product();
            p.Name = name;
            p.Calories = cal;
            p.Protein = prot;
            p.Carbs = carb;
            p.Fat = fat;
            return p;
        }
        public void SetProduct(string name)
        {
            Product p = new Product();
            p.Name = name;
            p.Calories = 0;
            p.Protein = 0;
            p.Carbs = 0;
            p.Fat = 0;
        }
        public Product GetProduct(Product pr)
        {
            Product result = new Product();
            result.Name = pr.Name;
            result.Calories = pr.Calories;
            result.Protein= pr.Protein;
            result.Carbs = pr.Carbs;
            result.Fat = pr.Fat;

            return result;

        }

        public bool Check()
        {
            if (Name != null && Calories != 0 && Protein != 0 && Carbs != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
