using Bitfit.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bitfit.Models
{
    public class Workout
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Type field is required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "The Rank field is required")]
        [Range(1, 6, ErrorMessage = "The Rank field must be between 1 and 6")]
        public int Rank { get; set; }
        [Required(ErrorMessage = "The Description field is required")]
        public string Description { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
