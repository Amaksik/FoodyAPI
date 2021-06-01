﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodyAPI.Models
{
    public class DayIntake
    {
        [Key]
        public int DayId { get; set; }
        public double DayCalories { get; set; }
        public DateTime Date { get; set; }

        [JsonIgnore]
        public int? PointerId { get; set; }
        [JsonIgnore]
        public User user{ get; set; }

    }
}
    