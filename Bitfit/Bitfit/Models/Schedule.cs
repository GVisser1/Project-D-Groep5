using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bitfit.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Rank field is required")]
        [Range(1, 6, ErrorMessage = "The Rank field must be between 1 and 6")]
        public int Rank { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Workout> Workouts { get; set; }
    }
}
