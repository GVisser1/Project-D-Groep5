using System;
using System.ComponentModel.DataAnnotations;

namespace Bitfit.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Rank field is required")]
        [Range(1, 6, ErrorMessage = "The Rank field must be between 1 and 6")]
        public int Rank { get; set; }
        public int Workout1Id { get; set; }
        public int Workout2Id { get; set; }
        public int Workout3Id { get; set; }
        public string Type { get; set; }
    }
}
