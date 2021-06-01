using FoodyAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodyAPI.Models
{
    public class User
    {
        [Key]
        public string ID { get; set; }
        public double callories { get; set; }


        public ICollection<DayIntake> Statistics { get; set; }

        public ICollection<Product> Favourite { get; set; }
        public User()
        {
            Favourite = new List<Product>();
            Statistics = new List<DayIntake>();
        }

       


     
        public void RemoveProduct(Product p)
        {
            this.Favourite.Remove(p);
        }

    }
}
