using System;
using System.ComponentModel.DataAnnotations;

namespace Bitfit.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "The Rank field is required")]
        [Range(1, 6, ErrorMessage = "The Rank field must be between 1 and 6")]
        public int Rank { get; set; }
        public int ScheduleId { get; set; }
        public string Description { get; set; }
    }
}
